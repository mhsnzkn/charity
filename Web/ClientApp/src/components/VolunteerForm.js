import alertify from 'alertifyjs';
import axios from 'axios';
import { ErrorMessage, Field, FieldArray, Form, Formik } from 'formik';
import React, { useState } from 'react';
import * as yup from 'yup';
import LoaderButton from './LoaderButton';

export default function VolunteerForm({ isSubmit }) {
    const [btnLoading, setBtnLoading] = useState(false);

    const initialValues = {
        firstName: '',
        lastName: '',
        email: '',
        address: '',
        postCode: '',
        mobileNumber: '',
        homeNumber: '',
        reason: '',
        organisations: [{
            organisation: '',
            role: ''
        }],
        skills: [{
            skill: '',
            experience: ''
        }],
        dbscheck: false
    }

    const ValidationSchema = yup.object().shape({
        firstName: yup.string().required("Required"),
        lastName: yup.string().required("Required"),
        address: yup.string().required("Required"),
        mobileNumber: yup.string().required("Required"),
        postCode: yup.string().required("Required").max(8, 'Cannot exceed 8 characters'),
        email: yup.string()
            .email("Invalid email address format")
            .required("Required"),
        reason: yup.string().required('Required').max(100, 'Cannot exceed 1000 characters'),
        organisations: yup.array().of(yup.object().shape({
            organisation: yup.string().required("Required"),
            role: yup.string().required("Required"),
        })),
        skills: yup.array().of(yup.object().shape({
            skill: yup.string().required("Required"),
            experience: yup.string().required("Required"),
        })),
        dbscheck: yup.boolean().oneOf([true], "You must confirm DBS check to submit")
    });

    const submitHandler = (values) => {
        setBtnLoading(true);
        axios.post('/api/volunteer', values)
        .then((res)=>{
            if(res.data.error){
                alertify.error(res.data.message)
            }else{
                isSubmit();
            }
            setBtnLoading(false);
        })
        .catch(err => {
            setBtnLoading(false);
        })
        
    }

    return (
        <div className='p-2 mb-2'>
            <h3>Volunteer Application Form</h3>
            <hr />
            <Formik
                initialValues={initialValues}
                onSubmit={submitHandler}
                validationSchema={ValidationSchema}
            >
                {({ values }) => (
                    <Form>
                        <h5>Personal Details</h5>
                        <div className="form-row">
                            <div className="form-group col-md-4">
                                <label htmlFor="firstName">First Name</label>
                                <Field id="firstName" name="firstName" className="form-control" />
                                <ErrorMessage component="span" name="firstName" className="text-danger" />
                            </div>
                            <div className="form-group col-md-4">
                                <label htmlFor="lastName">Last Name</label>
                                <Field id="lastName" name="lastName" className="form-control" />
                                <ErrorMessage component="span" name="lastName" className="text-danger" />
                            </div>
                            <div className="form-group col-md-4">
                                <label htmlFor="email">Email</label>
                                <Field id="email" name="email" type="email" className="form-control" />
                                <ErrorMessage component="span" name="email" className="text-danger" />
                            </div>
                            <div className="form-group col-md-4">
                                <label htmlFor="mobileNumber">Mobile Number</label>
                                <Field id="mobileNumber" name="mobileNumber" className="form-control" />
                                <ErrorMessage component="span" name="mobileNumber" className="text-danger" />
                            </div>
                            <div className="form-group col-md-4">
                                <label htmlFor="homeNumber">Home Number</label>
                                <Field id="homeNumber" name="homeNumber" className="form-control" />
                                <ErrorMessage component="span" name="homeNumber" className="text-danger" />
                            </div>
                            <div className="form-group col-md-4">
                                <label htmlFor="postCode">Post Code</label>
                                <Field id="postCode" name="postCode" className="form-control text-uppercase" />
                                <ErrorMessage component="span" name="postCode" className="text-danger" />
                            </div>
                        </div>
                        <div className="form-group">
                            <label htmlFor="address">Address</label>
                            <Field id="address" name="address" className="form-control" />
                            <ErrorMessage component="span" name="address" className="text-danger" />
                        </div>
                        <h5>Previous or present employment and voluntary work</h5>

                        <FieldArray name="organisations">
                            {({ insert, remove, push }) => (
                                <div className="form-row">
                                    {values.organisations.map((item, index) => (
                                        <React.Fragment key={index}>
                                            <div className="form-group col-md-5">
                                                <label htmlFor={`organisations[${index}].organisation`}>Organisation</label>
                                                <Field name={`organisations[${index}].organisation`} className="form-control" />
                                                <ErrorMessage component="span" name={`organisations[${index}].organisation`} className="text-danger" />
                                            </div>
                                            <div className="form-group col-md-5">
                                                <label htmlFor={`organisations[${index}].role`}>Roles and Responsibilities</label>
                                                <Field name={`organisations[${index}].role`} className="form-control" />
                                                <ErrorMessage component="span" name={`organisations[${index}].role`} className="text-danger" />
                                            </div>
                                            <div className="form-group col-md-2 d-flex justify-content-between">
                                                <div className='align-self-center'>
                                                    <br />
                                                    {index > 0 && <button onClick={() => remove((index))} className='btn btn-sm btn-danger m-2' style={{ maxHeight: '30px' }}><i className='fas fa-trash'></i></button>}
                                                    {index + 1 === values.organisations.length && <button onClick={() => push({ organisation: '', role: '' })} className='btn btn-sm btn-primary m-2' style={{ maxHeight: '30px' }}><i className='fas fa-plus'></i></button>}

                                                </div>
                                            </div>
                                        </React.Fragment>

                                    ))}

                                </div>
                            )}
                        </FieldArray>

                        <h5>Skills and Experience</h5>
                        <FieldArray name="skills">
                            {({ insert, remove, push }) => (
                                <div className="form-row">
                                    {values.skills.map((item, index) => (
                                        <React.Fragment key={index}>
                                            <div className="form-group col-md-5">
                                                <label htmlFor={`skills[${index}].skill`}>Skill</label>
                                                <Field name={`skills[${index}].skill`} className="form-control" />
                                                <ErrorMessage component="span" name={`skills[${index}].skill`} className="text-danger" />
                                            </div>
                                            <div className="form-group col-md-5">
                                                <label htmlFor={`skills[${index}].experience`}>Experience</label>
                                                <Field name={`skills[${index}].experience`} className="form-control" />
                                                <ErrorMessage component="span" name={`skills[${index}].experience`} className="text-danger" />
                                            </div>
                                            <div className="form-group col-md-2 d-flex justify-content-between">
                                                <div className='align-self-center'>
                                                    <br />
                                                    {index > 0 && <button onClick={() => remove((index))} className='btn btn-sm btn-danger m-2' style={{ maxHeight: '30px' }}><i className='fas fa-trash'></i></button>}
                                                    {index + 1 === values.skills.length && <button onClick={() => push({ skill: '', experience: '' })} className='btn btn-sm btn-primary m-2' style={{ maxHeight: '30px' }}><i className='fas fa-plus'></i></button>}

                                                </div>
                                            </div>
                                        </React.Fragment>

                                    ))}

                                </div>
                            )}
                        </FieldArray>

                        <h5>Volunteering with Heart4Refugees</h5>
                        <div className="form-group">
                            <label htmlFor="reason">Why do you want to volunteer with Heart4Refugees?</label>
                            <Field as="textarea" className="form-control" id="reason" name="reason" />
                            <ErrorMessage component="span" name="reason" className="text-danger" />
                        </div>
                        <h5>DBS</h5>
                        <div className="form-group">
                            <div className="form-check">
                                <Field className="form-check-input" type="checkbox" id="dbscheck" name="dbscheck" />
                                <label className="form-check-label" htmlFor="dbscheck">
                                    I understand that I would have to get a DBS check through Heart4Refugees if I was to volunteer
                                </label>
                            </div>
                            <ErrorMessage component="span" name="dbscheck" className="text-danger" />
                        </div>

                        <div className='d-flex justify-content-end'>
                            <LoaderButton isLoading={btnLoading} type="submit" className="btn btn-primary">Submit</LoaderButton>
                        </div>
                    </Form>

                )}
            </Formik>
        </div>
    );
}
