import React, { useState } from 'react';
import axios from 'axios';
import { ErrorMessage, Field, Form, Formik } from 'formik';
import { toast } from 'react-toastify';
import * as yup from 'yup';
import LoaderButton from './LoaderButton';

export default function VolunteerDocumentForm() {
    const [btnLoading, setBtnLoading] = useState(false);

    const initialValues = {
        firstName: '',
        lastName: '',
        email: '',
        address: '',
        postCode: '',
        mobileNumber: '',
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
        // firstName: yup.string().required("Required"),
        // lastName: yup.string().required("Required"),
        // address: yup.string().required("Required"),
        // mobileNumber: yup.string().required("Required"),
        // postCode: yup.string().required("Required").max(8),
        // email: yup.string()
        //     .email("Invalid email address format")
        //     .required("Required"),
        // reason: yup.string().required('Required'),
        // organisations: yup.array().of(yup.object().shape({
        //     organisation: yup.string().required("Required"),
        //     role: yup.string().required("Required"),
        // })),
        // skills: yup.array().of(yup.object().shape({
        //     skill: yup.string().required("Required"),
        //     experience: yup.string().required("Required"),
        // })),
        // dbscheck: yup.boolean().oneOf([true], "You must confirm DBS check to submit")
    });

    const submitHandler = (values) => {
        setBtnLoading(true);
        axios.post('/api/volunteer', values)
        .then((res)=>{
            if(res.data.error){
                toast.error(res.data.message)
            }else{
                //isSubmit();
            }
            setBtnLoading(false);
        })
        .catch(err => {
            setBtnLoading(false);
        })
        
    }
    
    return (
        <div className='p-2 mb-2'>
            <h3>Volunteer Documents Form</h3>
            <hr />
            <Formik
                initialValues={initialValues}
                onSubmit={submitHandler}
                validationSchema={ValidationSchema}
            >
                {({ values }) => (
                    <Form>
                        <div className="form-row">
                            <div className="form-group col-md-4">
                                <label htmlFor="firstName">Passport/BRP/Id</label>
                                <Field id="firstName" name="firstName" className="form-control" type='file' />
                                <ErrorMessage component="span" name="firstName" className="text-danger" />
                            </div>
                            <div className="form-group col-md-4">
                                <label htmlFor="lastName">Second Form</label>
                                <Field id="lastName" name="lastName" className="form-control" type='file' />
                                <ErrorMessage component="span" name="lastName" className="text-danger" />
                            </div>
                            <div className="form-group col-md-4">
                                <label htmlFor="email">Third Form</label>
                                <Field id="email" name="email" className="form-control" type='file' />
                                <ErrorMessage component="span" name="email" className="text-danger" />
                            </div>
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
