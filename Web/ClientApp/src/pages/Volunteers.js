import React, { useState } from 'react';
import { useAxiosGet } from '../Hooks/HttpRequests';
import axios from 'axios';
import { getHttpHeader, getLengthUrl, getPageIndex } from '../helpers/helpers';
import { useAuth } from '../Hooks/Auth';
import alertify from 'alertifyjs';
import ApiSelect from '../components/ApiSelect';
import Paginator from '../components/Paginator';
import { Link } from 'react-router-dom';
import Loader from '../components/Loader';
import { Cancelled, Completed } from '../constants/volunteerStatus';

export default function Volunteers() {
    const auth = useAuth();
    const baseUrl = "/api/volunteer"
    const [url, setUrl] = useState(baseUrl + "?start=0&length=10&searchString=&status=0");
    const [update, setUpdate] = useState(0);

    let response = useAxiosGet(url, update);

    const paramChangeHandler = (key, value) => {
        let paramUrl = url.split('?')[1];
        let params = new URLSearchParams(paramUrl);
        if (key === "page") {
            if (value < 1) return;
            let length = getLengthUrl(url);
            key = 'start';
            value = (value - 1) * length;
        } else {
            params.set('start', 0);
        }

        params.set(key, value);
        setUrl(baseUrl + "?" + params.toString());
    }

    const approve = (id) => {
        alertify.confirm("Approve", "Do you confirm to approve?",
            function () {
                let model = { id: id, action: "approve", cancellationReason: "" };
                axios.post(baseUrl + "/actions", model, getHttpHeader())
                    .then(res => {
                        if (res.data.error) {
                            return alertify.error(res.data.message);
                        }
                        alertify.success(res.data.message);
                        setUpdate(update + 1);
                    })
                    .catch(err => {
                        if (err.response) {
                            if (err.response.status === 401 || err.response.status === 403) {
                                auth.signout();
                            }
                        } else if (err.request) {
                            // client never received a response, or request never left
                        } else {
                            // anything else
                        }
                        alertify.error('Connection error!');
                    })
            }, null);

    }
    const cancel = (id) => {
        alertify.prompt('Reject Volunteer Application', 'Reject Reason:', '',
            function (evt, value) {
                let model = { id: id, action: "cancel", cancellationReason: value };
                axios.post(baseUrl + "/actions", model, getHttpHeader())
                    .then(res => {
                        if (res.data.error) {
                            return alertify.error(res.data.message);
                        }
                        alertify.success(res.data.message);
                        setUpdate(update + 1);
                    })
                    .catch(err => {
                        if (err.response) {
                            if (err.response.status === 401 || err.response.status === 403) {
                                auth.signout();
                            }
                        } else if (err.request) {
                            // client never received a response, or request never left
                        } else {
                            // anything else
                        }
                        alertify.error('Connection error!');
                    })
            }, null);


    }

    const showReason = (reason) => {
        alertify.alert('Cancellation Reason', reason);
    }

    let tableRows;
    if (response.data) {
        if (response.data.records) {

            tableRows = response.data.records.map(item => {
                return <tr key={item.id}>
                    <td>{item.name}</td>
                    <td>{item.email}</td>
                    <td>{item.mobileNumber}</td>
                    <td>{new Date(item.crtDate).toLocaleDateString('uk')}</td>
                    <td>{item.status === Cancelled ?
                    <span className="badge badge-danger">{item.status}</span>
                :
                item.status === Completed ?
                <span className="badge badge-success">{item.status}</span>
            :
            <span className="badge badge-light text-dark">{item.status}</span>}</td>
                    <td>
                        {item.status !== Completed && item.status !== Cancelled &&
                            <button className='btn btn-sm btn-success m-1'
                                onClick={() => approve(item.id)} title='Approve'>
                                <i className='fa fa-check'></i>
                            </button>}

                        <Link className='btn btn-sm btn-info m-1' to={`/VolunteerApplications/detail/${item.id}`} title='Details'>
                            <i className='fa fa-list'></i>
                        </Link>
                        {item.status === Cancelled ?
                            <button className='btn btn-sm btn-danger m-1' onClick={() => showReason(item.cancellationReason)} title='Show Reason'>
                                <i className='fa fa-list'></i>
                            </button>
                            :
                            <button className='btn btn-sm btn-danger m-1' onClick={() => cancel(item.id)} title='Reject'>
                                <i className='fa fa-times'></i>
                            </button>
                        }
                    </td>
                </tr>
            })
        } else {
            tableRows = <tr><td colSpan="5">No Data</td></tr>
        }
    }
if(response.loading){
    tableRows = <tr><td colSpan="6" className='text-center'><Loader/></td></tr>
}
if(response.error){
    tableRows = <tr><td colSpan="6" className='text-danger'>Connection Error!</td></tr>
}

console.log("index:"+getPageIndex(url));
    return (
        <>
            <h4>Volunteers</h4><hr />

            <div className='m-3 p-3'>
                <div className="form-group row">
                    <label className="col-sm-2 col-form-label">Status</label>
                    <div className="col-sm-4">
                        <ApiSelect
                            className="form-select"
                            url="/api/volunteer/GetVolunteerStatus"
                            onChange={value => paramChangeHandler('status', value)}
                        />
                    </div>
                </div>
            </div>

            <div className='d-flex justify-content-between'>
                <div className='col-md-6 row text-center'>
                    <label className="col-md-8">Rows per page: </label>
                    <select className='form-select col-md-4' onChange={e => paramChangeHandler("length", e.target.value)}>
                        <option value="10"> 10 </option>
                        <option value="50"> 50 </option>
                        <option value="100"> 100 </option>
                        <option value="0"> All </option>
                    </select>
                </div>
                <div className='col-md-6 row'>
                    <label className="col-md-4">Search: </label>
                    <input className='form-control col-md-8' placeholder='Search' onChange={e => paramChangeHandler("searchString", e.target.value)}></input>
                </div>
            </div>

            <table className="table table-striped table-border table-responsive-lg">
                <thead className="thead-light">
                    <tr>
                        <th scope="col">Name</th>
                        <th scope="col">Email</th>
                        <th scope="col">Mobile No</th>
                        <th scope="col">Application Date</th>
                        <th scope="col">Status</th>
                        <th scope="col">Action</th>
                    </tr>
                </thead>
                <tbody>

                    {tableRows}

                </tbody>
            </table>
            <Paginator
                totalItems={response.data?.totalItems ?? 0}
                pageIndex={getPageIndex(url)}
                pageSize={getLengthUrl(url)}
                pageChange={(e) => paramChangeHandler("page", e)}
            />

        </>
    );
}
