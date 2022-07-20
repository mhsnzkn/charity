import React, { useState } from 'react';
import { useAxiosGet } from '../Hooks/HttpRequests';
import axios from 'axios';
import { getLengthUrl, getPageIndex } from '../helpers/helpers';
import alertify from 'alertifyjs';
import ApiSelect from '../components/ApiSelect';
import Paginator from '../components/Paginator';
import { Link } from 'react-router-dom';
import Loader from '../components/Loader';
import { Cancelled, Completed, OnHold } from '../constants/volunteerStatus';

export default function Expenses() {
    const baseUrl = "/api/expense"
    const [url, setUrl] = useState(baseUrl + "?start=0&length=10");
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
                axios.post(baseUrl + "/actions", model)
                    .then(res => {
                        if (res.data.error) {
                            return alertify.error(res.data.message);
                        }
                        alertify.success(res.data.message);
                        setUpdate(update + 1);
                    })
            }, null);
    }
    const cancel = (id) => {
        alertify.prompt('Reject Volunteer Application', 'Reject Reason:', '',
            function (evt, value) {
                let model = { id: id, action: "cancel", cancellationReason: value };
                axios.post(baseUrl + "/actions", model)
                    .then(res => {
                        if (res.data.error) {
                            return alertify.error(res.data.message);
                        }
                        alertify.success(res.data.message);
                        setUpdate(update + 1);
                    })
            }, null);
    }
    const onHold = (id) => {
        alertify.confirm("On Hold", "Do you confirm to put the application on hold?",
            function () {
                let model = { id: id, action: "onhold" };
                axios.post(baseUrl + "/actions", model)
                    .then(res => {
                        if (res.data.error) {
                            return alertify.error(res.data.message);
                        }
                        alertify.success(res.data.message);
                        setUpdate(update + 1);
                    })
            }, null);
    }
    const sendMail = (id) => {
        alertify.confirm("Send Mail", "Do you confirm to send the last email?",
            function () {
                let model = { id: id };
                axios.post(baseUrl + "/sendMail", model)
                    .then(res => {
                        if (res.data.error) {
                            return alertify.error(res.data.message);
                        }
                        alertify.success(res.data.message);
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
                            item.status === OnHold ?
                                <span className="badge badge-secondary">{item.status}</span>
                                :
                                <span className="badge badge-light text-dark">{item.status}</span>}
                    </td>
                    <td>
                        <div className="btn-group dropleft">
                            <button type="button" className="btn btn-dark dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">

                            </button>
                            <div className="dropdown-menu">
                                <Link className='dropdown-item' to={`/VolunteerApplications/detail/${item.id}`}>
                                    <i className='fas fa-list'></i> Details
                                </Link>
                                <button className='dropdown-item'
                                    onClick={() => sendMail(item.id)}>
                                    <i className='fas fa-envelope'></i> Resend Mail
                                </button>

                                {item.status !== Completed && item.status !== Cancelled &&
                                    <>
                                        <button className='dropdown-item'
                                            onClick={() => approve(item.id)}>
                                            <i className='fas fa-check'></i> Approve
                                        </button>
                                        {item.status !== OnHold &&
                                            <button className='dropdown-item'
                                                onClick={() => onHold(item.id)}>
                                                <i className='fas fa-stop-circle'></i> On Hold
                                            </button>}
                                    </>
                                }

                                {item.status === Cancelled ?
                                    <button className='dropdown-item' onClick={() => showReason(item.cancellationReason)}>
                                        <i className="fas fa-comment-slash"></i> Show Reason
                                    </button>
                                    :
                                    <button className='dropdown-item' onClick={() => cancel(item.id)}>
                                        <i className="far fa-times-circle"></i> Cancel
                                    </button>
                                }
                            </div>
                        </div>
                    </td>
                </tr>
            })
        } else {
            tableRows = <tr><td colSpan="5">No Data</td></tr>
        }
    }
    if (response.loading) {
        tableRows = <tr><td colSpan="6" className='text-center'><Loader /></td></tr>
    }
    if (response.error) {
        tableRows = <tr><td colSpan="6" className='text-danger'>Connection Error!</td></tr>
    }

    return (
        <>
            <h4>Expenses</h4><hr />
            <div className='m-1 p-1'>
                <Link to="/VolunteerExpenses/Add" className='btn btn-primary'><i className='fas fa-plus'></i> Add</Link>
            </div>

            <div className='m-3 p-3'>
                <div className="form-group row">
                    <label className="col-sm-2 col-form-label">Status</label>
                    <div className="col-sm-4">
                        <ApiSelect
                            className="form-select"
                            url="/api/expense/GetExpenseStatus"
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
