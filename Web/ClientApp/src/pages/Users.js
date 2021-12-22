import React, { useState } from 'react';
import { Link, useParams } from 'react-router-dom';
import Paginator from '../components/Paginator';
import { getLengthUrl, getPageIndex } from '../helpers/helpers';
import { useAuth } from '../Hooks/Auth';
import { useAxiosGet } from '../Hooks/HttpRequests';

export default function Users() {
    const auth = useAuth();
    const baseUrl = "/api/user"
    const [url, setUrl] = useState(baseUrl + "?start=0&length=10&searchString=");
    const [update, setUpdate] = useState(0);
    const length = getLengthUrl(url);
    let response = useAxiosGet(url, update);

    const paramChangeHandler = (key, value) => {
        let paramUrl = url.split('?')[1];
        let params = new URLSearchParams(paramUrl);
        if (key === "page") {
            if (value < 1) return;
            key = 'start';
            value = (value - 1) * length;
        } else {
            params.set('start', 0);
        }

        params.set(key, value);
        setUrl(baseUrl + "?" + params.toString());
    }

    let tableRows;
    if (response.data) {
        if (response.data.records) {

            tableRows = response.data.records.map(item => {
                return <tr key={item.id}>
                    <td>{item.name}</td>
                    <td>{item.email}</td>
                    <td>{item.job}</td>
                    <td>{item.role}</td>
                    <td>{item.status === 0 ?
                        <span className="badge badge-danger">{item.statusName}</span>
                        :
                        <span className="badge badge-light text-dark">{item.statusName}</span>}
                    </td>
                    <td>
                        {/* {item.status < 7 &&
                            <button className='btn btn-sm btn-success m-1'
                                onClick={() => approve(item.id)} title='Approve'>
                                <i className='fa fa-check'></i>
                            </button>}

                        <Link className='btn btn-sm btn-info m-1' to={`/VolunteerApplications/detail/${item.id}`} title='Details'>
                            <i className='fa fa-list'></i>
                        </Link>
                        {item.status === 1 ?
                            <button className='btn btn-sm btn-danger m-1' onClick={() => showReason(item.cancellationReason)} title='Show Reason'>
                                <i className='fa fa-list'></i>
                            </button>
                            :
                            <button className='btn btn-sm btn-danger m-1' onClick={() => cancel(item.id)} title='Reject'>
                                <i className='fa fa-times'></i>
                            </button>
                        } */}
                    </td>
                </tr>
            })
        } else {
            tableRows = <tr><td colSpan="5">No Data</td></tr>
        }
    }


    return (
        <>
            <h4>Users</h4><hr />
            <div className='m-1 p-1'>
                <Link to="/Users/Add" className='btn btn-primary'><i className='fa fa-plus'></i> Add</Link>
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
                        <th scope="col">Job</th>
                        <th scope="col">Role</th>
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
                pageSize={length}
                pageChange={(e) => paramChangeHandler("page", e)}
            />

        </>
    );
}
