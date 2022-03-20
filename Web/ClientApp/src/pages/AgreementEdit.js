import { ErrorMessage, Field, Form, Formik } from 'formik';
import React, { useState, useEffect } from 'react';
import { Link, useNavigate, useParams } from 'react-router-dom';
import LoaderButton from '../components/LoaderButton';
import * as yup from 'yup'
import { getHttpHeader } from '../helpers/helpers';
import axios from 'axios';
import alertify from 'alertifyjs';

export default function AgreementEdit() {
    const params = useParams();
    const navigate = useNavigate();
    const [btnLoading, setBtnLoading] = useState(false);
    const [values, setValues] = useState({
        title: '',
        order: 1,
        content: '',
        isActive: true
    });
    const url = '/api/agreement'

    useEffect(()=>{
        if (params.id) {
            axios.get(url + "/" + params.id).then(res => {
                setValues(res.data)
            })
        }
    },[params.id])
    

    // const initialValues = {
    //     title : values?.title ?? '',
    //     order : values?.order ?? 1,
    //     content : values?.content ?? '',
    //     status :  values?.status ?? "Active"
    // };
    const validationSchema = yup.object().shape({
        title: yup.string().required('Required'),
        order: yup.number().required('Required'),
        content: yup.string().required('Required'),
        isActive: yup.bool()
    });

    const submitHandler = values => {
        setBtnLoading(true);
        values.id = params.id;
        axios.post(url, values, getHttpHeader())
            .then((res) => {
                if (res.data.error) {
                    alertify.error(res.data.message)
                } else {
                    navigate("/Agreements")
                }
                //setBtnLoading(false);
            })
            .finally(()=> setBtnLoading(false))
    }

    return (
        <>
            <h4>Agreement</h4>
            <hr />
            <Link to="/Agreements" className='btn btn-dark m-1'><i className='fas fa-undo'></i> Back</Link>
            <Formik
                initialValues={values}
                onSubmit={submitHandler}
                validationSchema={validationSchema}
                enableReinitialize={true}
            >
                {({ formValues }) => (
                    <Form>
                        <h5>Details</h5>
                        <div className="form-row">
                            <div className="form-group col-md-6">
                                <label htmlFor="title">Title</label>
                                <Field id="title" name="title" className="form-control" />
                                <ErrorMessage component="span" name="title" className="text-danger" />
                            </div>
                            <div className="form-group col-md-6">
                                <label htmlFor="order">Order</label>
                                <Field type="number" id="order" name="order" className="form-control" />
                                <ErrorMessage component="span" name="order" className="text-danger" />
                            </div>
                            <div className="form-group col-md-12">
                                <label htmlFor="contenttext">Content</label>
                                <Field as="textarea" id="contenttext" name="content" className="form-control" />
                                <ErrorMessage component="span" name="content" className="text-danger" />
                            </div>
                            <div className="form-check">
                                <Field type="checkbox" id="isActive" name="isActive" className="form-check-input" />
                                <label className="form-check-label" htmlFor="isActive">
                                    Status
                                </label>
                            </div>
                        </div>

                        <div className='d-flex justify-content-end'>
                            <LoaderButton isLoading={btnLoading} type="submit" className="btn btn-primary">Save</LoaderButton>
                        </div>
                    </Form>

                )}
            </Formik>
        </>
    );
}
