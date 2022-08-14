import alertify from 'alertifyjs';
import axios from 'axios';
import { ErrorMessage, Field, Form, Formik } from 'formik';
import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import * as yup from 'yup'
import LoaderButton from '../components/LoaderButton'
import { Admin, Volunteer } from '../constants/userRoles';
import { getHttpHeader } from '../helpers/helpers';
import { useAxiosGet } from '../Hooks/HttpRequests';

export default function UserAdd() {
    const [btnLoading, setBtnLoading] = useState(false);
    const navigate = useNavigate();
    const volunteerResponse = useAxiosGet("/api/Volunteer/VolunteersForDropDown");
    let volunteerOptions;
    if(volunteerResponse.data){
        volunteerOptions = volunteerResponse.data.map(item => {return <option key={item.id} value={item.id}>{item.name}</option>})
    }

    const initialValues = {
        name:'',
        email:'',
        password:'',
        passwordConfirm:'',
        job:'',
        role:'',
        status: 'Active',
        volunteerId: ''
    };
    const validationSchema = yup.object().shape({
        name: yup.string().required('Required'),
        email: yup.string().email("Invalid email").required('Required'),
        password: yup.string().min(5, "Must be at least 5 characters").required('Required'),
        passwordConfirm: yup.string().oneOf([yup.ref('password'), null], "Passwords don't match!").required('Required'),
        role: yup.string().required('Required'),
        status: yup.string().oneOf(['Active','Passive']).required('Required'),
        volunteerId: yup.string().required('Required'),
    });

    const submitHandler = values =>{
        setBtnLoading(true);
        axios.post("/api/user", values, getHttpHeader())
        .then((res)=>{
            if(res.data.error){
                alertify.error(res.data.message)
            }else{
                navigate("/Users")
            }
            setBtnLoading(false);
        })
        .catch(err => {
            alertify.error("Connection Error!")
            setBtnLoading(false);
        })
    }
    
    return (
        <>
            <h4>User Add</h4>
            <hr />
            <Link to="/Users" className='btn btn-dark m-1'><i className='fas fa-undo'></i> Back</Link>
            <Formik
                initialValues={initialValues}
                onSubmit={submitHandler}
                validationSchema={validationSchema}
            >
                {({ values }) => (
                    <Form>
                        <h5>Personal Details</h5>
                        <div className="form-row">
                            <div className="form-group col-md-6">
                                <label htmlFor="name">Name</label>
                                <Field id="name" name="name" className="form-control" />
                                <ErrorMessage component="span" name="name" className="text-danger" />
                            </div>
                            <div className="form-group col-md-6">
                                <label htmlFor="email">Email</label>
                                <Field id="email" name="email" className="form-control" />
                                <ErrorMessage component="span" name="email" className="text-danger" />
                            </div>
                            <div className="form-group col-md-6">
                                <label htmlFor="password">Password</label>
                                <Field id="password" name="password" type="password" className="form-control" />
                                <ErrorMessage component="span" name="password" className="text-danger" />
                            </div>
                            <div className="form-group col-md-6">
                                <label htmlFor="passwordConfirm">Password Confirm</label>
                                <Field id="passwordConfirm" name="passwordConfirm" type="password" className="form-control" />
                                <ErrorMessage component="span" name="passwordConfirm" className="text-danger" />
                            </div>
                            <div className="form-group col-md-6">
                                <label htmlFor="job">Job</label>
                                <Field id="job" name="job" className="form-control" />
                                <ErrorMessage component="span" name="job" className="text-danger" />
                            </div>
                            <div className="form-group col-md-6">
                                <label htmlFor="role">Role</label>
                                <Field as="select" id="role" name="role" className="form-select" >
                                    <option value="">--Choose--</option>
                                    <option value={Volunteer}>Volunteer</option>
                                    <option value={Admin}>Admin</option>
                                </Field>
                                <ErrorMessage component="span" name="role" className="text-danger" />
                            </div>
                            <div className="form-group col-md-6">
                                <label htmlFor="status">Status</label>
                                <Field as="select" id="status" name="status" className="form-select" >
                                    <option value="Passive">Passive</option>
                                    <option value="Active">Active</option>
                                </Field>
                                <ErrorMessage component="span" name="status" className="text-danger" />
                            </div>
                            <div className="form-group col-md-6">
                                <label htmlFor="volunteerId">Volunteer</label>
                                <Field as="select" id="volunteerId" name="volunteerId" className="form-select" >
                                    <option value="">--Choose--</option>
                                    {volunteerOptions}
                                </Field>
                                <ErrorMessage component="span" name="volunteerId" className="text-danger" />
                            </div>
                        </div>

                        <div className='d-flex justify-content-end'>
                            <LoaderButton isLoading={btnLoading} type="submit" className="btn btn-primary">Submit</LoaderButton>
                        </div>
                    </Form>

                )}
            </Formik>

        </>
    );
}
