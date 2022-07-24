import React, { useState } from 'react';
import { useAxiosGet } from '../Hooks/HttpRequests';
import axios from 'axios';
import { getLengthUrl, getPageIndex } from '../helpers/helpers';
import alertify from 'alertifyjs';
import ApiSelect from '../components/ApiSelect';
import Paginator from '../components/Paginator';
import { Link } from 'react-router-dom';
import Loader from '../components/Loader';
import { Cancelled, Accepted, Paid } from '../constants/expenseStatus';

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
        alertify.confirm("Accept", "Do you confirm to accept?",
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
        alertify.prompt('Reject Expense Payment', 'Reject Reason:', '',
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
    const pay = (id) => {
        alertify.prompt('Expense Payment', 'Paid Date:', '',
            function (evt, value) {
                if (value) {
                    let model = { id: id, action: "pay", date: value };
                    axios.post(baseUrl + "/actions", model)
                        .then(res => {
                            if (res.data.error) {
                                return alertify.error(res.data.message);
                            }
                            alertify.success(res.data.message);
                            setUpdate(update + 1);
                        })
                }else{
                    alertify.alert("Payment is not done", "Date is required")
                }
            }, null).set('type', 'date');
    }

    const showReason = (reason) => {
        alertify.alert('Cancellation Reason', reason);
    }

    let tableRows;
    if (response.data) {
        if (response.data.records) {

            tableRows = response.data.records.map(item => {
                return <tr key={item.id}>
                    <td>{item.userName}</td>
                    <td>{new Date(item.date).toLocaleDateString('uk')}</td>
                    <td>{item.amount}</td>
                    <td>{item.status === Cancelled ?
                        <span className="badge badge-danger">{item.status}</span>
                        :
                        item.status === Paid ?
                            <span className="badge badge-success">{item.status}</span>
                            :
                            item.status === Accepted ?
                                <span className="badge badge-info ">{item.status}</span>
                                :
                                <span className="badge badge-light text-dark">{item.status}</span>}
                    </td>
                    <td>{item.payDate && new Date(item.payDate).toLocaleDateString('uk')}</td>
                    <td>
                        <Link className='btn btn-sm btn-info m-1' to={`/VolunteerExpenses/Edit/${item.id}`} title="Edit">
                            <i className='fas fa-edit'></i>
                        </Link>
                        {item.status === Accepted ?
                            <button className='btn btn-sm btn-success m-1' onClick={() => pay(item.id)} title="Pay">
                                <i className="fas fas fa-money-bill"></i>
                            </button>
                            :
                            <button className='btn btn-sm btn-success m-1' onClick={() => approve(item.id)} title="Accept">
                                <i className='fas fa-check'></i>
                            </button>
                        }


                        {item.status === Cancelled ?
                            <button className='btn btn-sm btn-danger m-1' onClick={() => showReason(item.description)} title="Show Reason">
                                <i className="fas fa-comment-slash"></i>
                            </button>
                            :
                            <button className='btn btn-sm btn-danger m-1' onClick={() => cancel(item.id)} title="Cancel">
                                <i className="far fa-times-circle"></i>
                            </button>
                        }

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
                        <th scope="col">Date</th>
                        <th scope="col">Amount</th>
                        <th scope="col">Status</th>
                        <th scope="col">Date Paid</th>
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
