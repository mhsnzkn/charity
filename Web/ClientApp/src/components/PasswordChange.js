import alertify from 'alertifyjs';
import axios from 'axios';
import { useFormik } from 'formik';
import React, { useState } from 'react';
import { getHttpHeader } from '../helpers/helpers';
import { useAuth } from '../Hooks/Auth';
import LoaderButton from './LoaderButton';
import * as yup from 'yup'

export default function PasswordChange() {
    const auth = useAuth()
    const [btnLoading, setBtnLoading] = useState(false);

    const formik = useFormik({
        initialValues: { password: '', passwordconfirm: '' },
        validationSchema: yup.object().shape({
            password: yup.string()
                .min(5, "Must be at least 5 characters")
                .required("Required"),
            passwordconfirm: yup.string()
                .oneOf([yup.ref('password'), null], "Passwords don't match!")
                .required('Required')
        }),
        onSubmit: values => {
            values.action = "password";
            setBtnLoading(true);
            axios.post("/api/user/updateinfo", values, getHttpHeader())
                .then(res => {
                    if (res.data.error) {
                        alertify.error(res.data.message)
                    } else {
                        alertify.success(res.data.message)
                        formik.resetForm();
                    }
                    setBtnLoading(false);
                }).catch(err => {
                    if (err.response) {
                        if (err.response.status === 401 || err.response.status === 403) {
                            auth.signout();
                        } else {
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
    });


    return (

        <form onSubmit={formik.handleSubmit}>
            <div className="form-row">
                <div className="form-group col-md-4">
                    <label htmlFor='password'>Password</label>
                    <input id="password" name="password" type="password" className="form-control"
                        value={formik.values.password}
                        onChange={formik.handleChange}
                        onBlur={formik.handleBlur}
                    />
                    {formik.errors.password && formik.touched.password && <span className="text-danger" >{formik.errors.password}</span>}
                </div>
                <div className="form-group col-md-4">
                    <label htmlFor="passwordconfirm">Password Confirm</label>
                    <input id="passwordconfirm" name="passwordconfirm" type="password" className="form-control"
                        value={formik.values.passwordconfirm}
                        onChange={formik.handleChange}
                        onBlur={formik.handleBlur}
                    />
                    {formik.errors.passwordconfirm && formik.touched.passwordconfirm && <span className="text-danger" >{formik.errors.passwordconfirm}</span>}
                </div>
                <div className="form-group col-md-4">
                    <br />
                    <LoaderButton isLoading={btnLoading} type="submit" className="btn btn-primary">Change Password</LoaderButton>
                </div>
            </div>
        </form>
    );
}
