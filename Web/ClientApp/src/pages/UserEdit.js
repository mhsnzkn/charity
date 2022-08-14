import { ErrorMessage, Field, Form, Formik } from 'formik';
import React, { useState } from 'react';
import { Link, useNavigate, useParams } from 'react-router-dom';
import LoaderButton from '../components/LoaderButton';
import * as yup from 'yup'
import { getHttpHeader } from '../helpers/helpers';
import axios from 'axios';
import alertify from 'alertifyjs';
import { useAxiosGet } from '../Hooks/HttpRequests';
import { Admin, Volunteer } from '../constants/userRoles';

export default function UserEdit() {
    const params = useParams();
    const [btnLoading, setBtnLoading] = useState(false);
    const navigate = useNavigate();
    const url = '/api/user'
    const volunteerResponse = useAxiosGet("/api/Volunteer/VolunteersForDropDown");
    const response = useAxiosGet(url+"/"+params.id)
    
    let volunteerOptions;
    if(volunteerResponse.data){
        volunteerOptions = volunteerResponse.data.map(item => {return <option key={item.id} value={item.id}>{item.name}</option>})
    }

    const initialValues = {
        name : response.data?.name ?? '',
        email : response.data?.email ?? '',
        job : response.data?.job ?? '',
        role : response.data?.role ?? '',
        status :  response.data?.status ?? "Active",
        volunteerId: response.data?.volunteerId ?? ""
    };
    const validationSchema = yup.object().shape({
        name: yup.string().required('Required'),
        email: yup.string().email("Invalid email").required('Required'),
        role: yup.string().required('Required'),
        status: yup.string().oneOf(['Active','Passive']).required('Required')
    });

    const submitHandler = values =>{
        setBtnLoading(true);
        values.id = params.id;
        axios.post(url, values, getHttpHeader())
        .then((res)=>{
            if(res.data.error){
                alertify.error(res.data.message)
            }else{
                navigate("/Users")
            }
            //setBtnLoading(false);
        })
        .catch(err => {
            setBtnLoading(false);
            alertify.error("Connection Error!")
        })
    }
    
    return (
        <>
            <h4>User Edit</h4>
            <hr />
            <Link to="/Users" className='btn btn-dark m-1'><i className='fas fa-undo'></i> Back</Link>
            <Formik
                initialValues={initialValues}
                onSubmit={submitHandler}
                validationSchema={validationSchema}
                enableReinitialize={true}
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
