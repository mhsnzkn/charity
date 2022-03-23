import alertify from 'alertifyjs';
import axios from 'axios';
import React, {useState} from 'react';
import { Link } from 'react-router-dom';
import Loader from '../components/Loader';
import Paginator from '../components/Paginator';
import { getLengthUrl, getPageIndex } from '../helpers/helpers';
import { useAxiosGet } from '../Hooks/HttpRequests';

export default function Agreement() {
    const baseUrl = "/api/agreement"
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
    const remove = (id) => {
        alertify.confirm('Delete Agreement', 'Agreement will be deleted or disabled', 
        function(){
            axios.delete(baseUrl+"/"+id).then( res =>{
                if (res.data.error) {
                    return alertify.error(res.data.message);
                }
                alertify.success(res.data.message);
                setUpdate(update + 1);
            })
        },null)
    }

    let tableRows;
    if (response.data) {
        if (response.data.records) {

            tableRows = response.data.records.map(item => {
                return <tr key={item.id}>
                    <td>{item.title}</td>
                    <td>{new Date(item.date).toLocaleDateString()}</td>
                    <td>{item.isActive ?
                        <span className="badge badge-success">Active</span>
                        :
                        <span className="badge badge-danger">Passive</span>}
                    </td>
                    <td>
                        <Link className='btn btn-sm btn-info m-1' to={`/Agreements/edit/${item.id}`} title='Edit Agreement'>
                            <i className='fas fa-edit'></i>
                        </Link>
                        {item.isActive &&
                            <>
                                <button className='btn btn-sm btn-danger m-1' onClick={() => remove(item.id)} title='Delete/Passive'>
                                    <i className='fas fa-trash'></i>
                                </button>
                            </>
                        }
                    </td>
                </tr>
            })
        } else {
            tableRows = <tr><td colSpan="4">No Data</td></tr>
        }
    }
    if(response.loading){
        tableRows = <tr><td colSpan="4" className='text-center'><Loader /></td></tr>
    }
    if(response.error){
        tableRows = <tr><td colSpan="4" className='text-danger'>Connection Error!</td></tr>
    }


    return (
        <>
            <h4>Agreements</h4><hr />
            <div className='m-1 p-1'>
                <Link to="/Agreements/Add" className='btn btn-primary'><i className='fas fa-plus'></i> Add</Link>
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
                        <th scope="col">Title</th>
                        <th scope="col">Date</th>
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
