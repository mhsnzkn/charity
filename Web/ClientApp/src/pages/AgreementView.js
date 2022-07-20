import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import Loader from '../components/Loader';
import axios from 'axios';

export default function AgreementView() {
    const params = useParams();
    const [values, setValues] = useState({});
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    const url = '/api/agreement'


    useEffect(() => {
        if (params.id) {
            setLoading(true);
            axios.get(url + "/" + params.id).then(res => {
                setValues(res.data)
            })
                .finally(() => setLoading(false))
        }
    }, [params.id])


    return (
        <>
            <button onClick={()=>navigate(-1)} className='btn btn-dark m-1'><i className='fas fa-undo'></i> Back</button>
            {loading ?
                <Loader />
                :
                <>
                    <h5>Details</h5>
                    <div className="form-row">
                        <div className="form-group col-md-6">
                            <label htmlFor="title">Title</label>
                            <input className="form-control"
                                disabled
                                value={values?.title}
                            />
                        </div>
                        <div className="form-group col-md-6">
                            <label htmlFor="order">Order</label>
                            <input type="number" className="form-control"
                                disabled
                                value={values?.order}
                            />
                        </div>
                        <div className="form-check mb-4">
                            <input type="checkbox" className="form-check-input" 
                                disabled
                                value={values?.isActive}
                                checked={values?.isActive}
                            />
                            <label className="form-check-label" htmlFor="isActive">
                                Status
                            </label>
                        </div>
                        <div className="form-group col-md-12">
                            <label htmlFor="contenttext">Content</label>
                            <div className='border p-1 bg-light' dangerouslySetInnerHTML={{ __html: values?.content }}></div>
                        </div>
                    </div>

                </>

            }
        </>
    );
}
