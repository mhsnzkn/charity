import React, { useEffect, useState } from 'react';
import ReactDatatable from '@mkikets/react-datatable';
import { useAxiosGet } from '../Hooks/HttpRequests';
import axios from 'axios';
import { getHttpHeader } from '../helpers/helpers';
import { useAuth } from '../Hooks/Auth';
import { Link } from 'react-router-dom';
import alertify from 'alertifyjs';

export default function Volunteers() {
    const auth = useAuth();
    const url = '/api/volunteer';
    const [tableData, setTableData] = useState({ total: 0, records: [] })
    const [updatePage, setUpdatePage] = useState(0);

    let response = useAxiosGet(url, updatePage);

    useEffect(() => {
        if (response.data) {
            setTableData({ total: response.data.total, records: response.data.records })
        }
    }, [response.data])



    const columns = [
        {
            key: "name",
            text: "Name",
            align: 'center'
        },
        {
            key: "email",
            text: "Email",
            className: 'text-center',
            align: 'center'
        },
        {
            key: "mobileNumber",
            text: "Mobile Number",
            align: 'center'
        },
        {
            key: "statusName",
            text: "Status",
            className: 'text-center',
            align: 'center'
        },
        {
            key: "id",
            text: "Action",
            align: 'center',
            className: 'text-center',
            cell: (record, index) => {
                return (
                    <>
                        {record.status < 6 &&
                            <button className='btn btn-sm btn-success m-1'
                                onClick={() => approve(record.id, record.status)}>
                                <i className='fa fa-check'></i>
                            </button>}

                        <Link className='btn btn-sm btn-info m-1' to={`/VolunteerApplications/detail/${record.id}`}>
                            <i className='fa fa-list'></i>
                        </Link>
                        {record.status === 0 ?
                            <button className='btn btn-sm btn-danger m-1' onClick={() => showReason(record.cancellationReason)}>
                                <i className='fa fa-times'></i>
                            </button>
                            :
                            <button className='btn btn-sm btn-danger m-1' onClick={() => cancel(record.id)}>
                                <i className='fa fa-times'></i>
                            </button>
                        }

                    </>
                )
            }
        }
    ];
    const config = {
        page_size: 10,
        length_menu: [10, 20, 50],
        show_filter: true,
        show_pagination: true,
        pagination: 'advance',
        filename: "Volunteers",
        button: {
            excel: true,
            print: true
        }
    }
    const approve = (id, status) => {
        alertify.confirm("Approve", "Do you confirm to approve?",
            function () {
                let model = { id: id, action: "approve", cancellationReason: "" };
                axios.put(url, model, getHttpHeader())
                    .then(res => {
                        if (res.data.error) {
                            return alertify.error(res.data.message);
                        }
                        alertify.success(res.data.message);
                        setUpdatePage(updatePage + 1);
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
                axios.put(url, model, getHttpHeader())
                    .then(res => {
                        if (res.data.error) {
                            return alertify.error(res.data.message);
                        }
                        alertify.success(res.data.message);
                        setUpdatePage(updatePage + 1);
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


    const tableChangeHandler = data => {
        let queryString = Object.keys(data).map((key) => {
            if (key === "sort_order" && data[key]) {
                return encodeURIComponent("sort_order") + '=' + encodeURIComponent(data[key].order) + '&' + encodeURIComponent("sort_column") + '=' + encodeURIComponent(data[key].column)
            } else {
                return encodeURIComponent(key) + '=' + encodeURIComponent(data[key])
            }

        }).join('&');

        //this.getData(queryString);
        console.log(queryString);
    }

    const getData = (queryString = "") => {
        let url = "http://tradekisan.in/react-datatable/sampleAPI/?" + queryString
        fetch(url, {
            headers: {
                "Accept": "application/json"
            }
        })
            .then(res => res.json())
            .then(res => {
                this.setState({
                    total: res.total,
                    records: res.result
                })
            })

    }


    return (
        <>
            <h4>Volunteers</h4><hr />

            <div className='m-3 p-3'>
                <div class="form-group row">
                    <label for="inputPassword" class="col-sm-2 col-form-label">Status</label>
                    <div class="col-sm-4">
                        <select className="form-control">
                            <option>1</option>
                            <option>2</option>
                            <option>3</option>
                        </select>
                    </div>
                </div>
            </div>


            <ReactDatatable
                config={config}
                records={tableData.records}
                columns={columns}
                dynamic={true}
                total_record={tableData.total}
                onChange={tableChangeHandler} />

        </>


    );
}
