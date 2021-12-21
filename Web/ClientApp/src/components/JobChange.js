import alertify from 'alertifyjs';
import axios from 'axios';
import { ErrorMessage, Field, Form, Formik } from 'formik';
import React, { useState } from 'react';
import * as yup from 'yup'
import { getHttpHeader } from '../helpers/helpers';
import { useAuth } from '../Hooks/Auth';
import LoaderButton from './LoaderButton';

export default function JobChange({currentJob, onChange}) {
    const auth = useAuth()
    const [btnLoading, setBtnLoading] = useState(false);

    const schema = yup.object().shape({
        job: yup.string()
            .required("Required"),
    });

    const submitHandler = (values) => {
        values.action = "job";
        setBtnLoading(true);
        axios.post("/api/user/updateinfo", values, getHttpHeader())
            .then( res =>{
                if(res.data.error){
                    alertify.error(res.data.message)
                }else{
                    alertify.success(res.data.message)
                    onChange(values.job);
                }
                setBtnLoading(false);
            }).catch( err =>{
                if (err.response) {
                    if(err.response.status=== 401 || err.response.status===403){
                        auth.signout();
                    }else{
                        alertify.error('Connection error!');
                    }
                } else if (err.request) {
                // client never received a response, or request never left
                alertify.error('Connection error!');
                } else {
                // anything else
                alertify.error('Connection error!');
                }
                setBtnLoading(false);
            })

    }

    return (
        <Formik
            initialValues={{ job: '' }}
            validationSchema={schema}
            onSubmit={submitHandler}>
            <Form>
                <div className="form-row">
                    <div className="form-group col-md-4">
                        <label>Job</label>
                        <label className="form-control bg-light">{currentJob}</label>
                    </div>
                    <div className="form-group col-md-4">
                        <label htmlFor="job">New Job</label>
                        <Field id="job" name="job" className="form-control" />
                        <ErrorMessage component="span" name="job" className="text-danger" />
                    </div>
                    <div className="form-group col-md-4">
                        <br />
                        <LoaderButton isLoading={btnLoading} type="submit" className="btn btn-primary">Change Job</LoaderButton>

                    </div>
                </div>
            </Form>
        </Formik>
    );
}
