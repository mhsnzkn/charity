import { useFormik } from 'formik';
import React, { useState, useEffect } from 'react';
import { Link, useNavigate, useParams } from 'react-router-dom';
import LoaderButton from '../components/LoaderButton';
import Loader from '../components/Loader';
import * as yup from 'yup'
import axios from 'axios';
import alertify from 'alertifyjs';
import ApiSelect from '../components/ApiSelect';

export default function ExpenseEdit() {
    const params = useParams();
    const navigate = useNavigate();
    const [btnLoading, setBtnLoading] = useState(false);
    const [values, setValues] = useState({});
    const [file, setFile] = useState();
    const [loading, setLoading] = useState(false);
    const url = '/api/expense';


    useEffect(() => {
        if (params.id) {
            setLoading(true);
            axios.get(url + "/" + params.id).then(res => {
                setValues(res.data)
            })
                .finally(() => setLoading(false))
        }
    }, [params.id])

    const submitHandler = values => {
        setBtnLoading(true);
        const formData = new FormData();
        formData.append('id', params.id || 0);
        formData.append('details', values.details);
        formData.append('modeOfTransport', values.modeOfTransport);
        formData.append('claim', values.claim);
        formData.append('amount', values.amount);
        formData.append('totalMileage', values.totalMileage);
        formData.append('date', values.date);
        formData.append('volunteerId', values.volunteerId);
        formData.append('formFile', file);

        axios.post(url, formData)
            .then((res) => {
                if (res.data.error) {
                    alertify.error(res.data.message)
                } else {
                    navigate("/VolunteerExpenses")
                }
            })
            .finally(() => setBtnLoading(false))
    }
    
    const formik = useFormik({
        enableReinitialize: true,
        initialValues: {
            details: values.details || '',
            modeOfTransport: values.modeOfTransport || '',
            claim: values.claim || '',
            amount: values.amount || 0,
            totalMileage: values.totalMileage || 0,
            date: values.date ? new Date(values.date).toISOString().split('T')[0] : new Date().toISOString().split('T')[0],
            volunteerId: values.volunteerId || ''
        },
        onSubmit: submitHandler,
        validationSchema: yup.object().shape({
            details: yup.string().required('Required'),
            modeOfTransport: yup.string().required('Required'),
            claim: yup.string().required('Required'),
            amount: yup.number().required('Required'),
            totalMileage: yup.number().required('Required'),
            date: yup.date().required('Required'),
            volunteerId: yup.string().required('Required'),
        })
    });
    
    return (
        <>
            <Link to="/VolunteerExpenses" className='btn btn-dark m-1'><i className='fas fa-undo'></i> Back</Link>

            {loading ?
                <Loader />
                :
                <form onSubmit={formik.handleSubmit}>
                    <h5>Details</h5>
                    <div className="form-row">
                        <div className="form-group col-md-6">
                            <label htmlFor="date">Date</label>
                            <input type="date" id="date" name="date" className="form-control"
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                                value={formik.values.date}
                            />
                            {formik.touched.date && formik.errors.date && <small className='text-danger'>{formik.errors.date}</small>}
                        </div>
                        <div className="form-group col-md-6">
                            <label htmlFor="details">Details</label>
                            <input id="details" name="details" className="form-control"
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                                value={formik.values.details}
                            />
                            {formik.touched.details && formik.errors.details && <small className='text-danger'>{formik.errors.details}</small>}
                        </div>
                        <div className="form-group col-md-6">
                            <label htmlFor="modeOfTransport">Mode Of Transport</label>
                            <input id="modeOfTransport" name="modeOfTransport" className="form-control"
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                                value={formik.values.modeOfTransport}
                            />
                            {formik.touched.modeOfTransport && formik.errors.modeOfTransport && <small className='text-danger'>{formik.errors.modeOfTransport}</small>}
                        </div>
                        <div className="form-group col-md-6">
                            <label htmlFor="claim">Claim</label>
                            <input id="claim" name="claim" className="form-control"
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                                value={formik.values.claim}
                            />
                            {formik.touched.claim && formik.errors.claim && <small className='text-danger'>{formik.errors.claim}</small>}
                        </div>
                        <div className="form-group col-md-6">
                            <label htmlFor="totalMileage">Total Mileage</label>
                            <input type="number" id="totalMileage" name="totalMileage" className="form-control"
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                                value={formik.values.totalMileage}
                            />
                            {formik.touched.totalMileage && formik.errors.totalMileage && <small className='text-danger'>{formik.errors.totalMileage}</small>}
                        </div>
                        <div className="form-group col-md-6">
                            <label htmlFor="amount">Amount</label>
                            <input type="number" id="amount" name="amount" className="form-control"
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                                value={formik.values.amount}
                            />
                            {formik.touched.amount && formik.errors.amount && <small className='text-danger'>{formik.errors.amount}</small>}
                        </div>
                        <div className="form-group col-md-6">
                            <label htmlFor="formFile">New File</label>
                            <input type="file" id="formFile" name="formFile" className="form-control"
                                onChange={(e) => setFile(e.target.files[0])}
                            />
                        </div>
                        <div className="form-group col-md-6">
                            <label htmlFor="formFile">Volunteer</label>
                            <ApiSelect className="custom-select" 
                                onChange={(e)=> formik.setFieldValue('volunteerId', e)} 
                                url="/api/volunteer/VolunteersForDropDown"
                                defaultValue={{id:'', name:'--Choose--'}}
                                value={formik.values.volunteerId}
                                />
                            {formik.touched.volunteerId && formik.errors.volunteerId && <small className='text-danger'>{formik.errors.volunteerId}</small>}
                        </div>
                    </div>

                    <div className='d-flex justify-content-end'>
                        <LoaderButton isLoading={btnLoading} type="submit" className="btn btn-primary">Save</LoaderButton>
                    </div>
                    
                    {values.filePath && <div><img className='img-fluid' src={values.filePath} alt='expense'/></div>}
                    
                </form>

            }
        </>
    );
}
