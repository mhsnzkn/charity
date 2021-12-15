import { useFormik } from 'formik';
import React, { useState } from 'react';
import { toast } from 'react-toastify';
import { useAuth } from '../Hooks/Auth';
import * as Yup from 'yup'

export default function Login() {
	let auth = useAuth();

    const schema = Yup.object().shape({
        email: Yup.string()
        .email()
        .required('Required'),
        password: Yup.string().required('Required'),
      });

    const formik = useFormik({
        initialValues: {
          email: '',
          password:''
        },
        validationSchema: schema,
        onSubmit: values => {
          submitForm();
        },
      });

	const submitForm = () => {
		auth.signin(formik.values.email, formik.values.password);
	}

    const handleKeyDown = (event) => {
		if (event.key === 'Enter') {
			event.preventDefault();
			submitForm();
		}
	}

    return (
        <div className='d-flex justify-content-center' style={{ height: "92vh", backgroundImage: 'url(/wood-hearts-4k.jpg)' }}>
            <div className='align-self-center p-4 rounded' style={{backgroundColor:'rgb(149 154 192 / 95%)'}}>
                <img className='img-fluid' src='/logo.png' alt='heart4refugees' style={{maxWidth:'300px'}}/>
                <form onSubmit={formik.handleSubmit}>
                    <div className="form-group">
                        <label htmlFor='email'>Email address</label>
                        <input type="email" className="form-control" placeholder="Enter email"
                        id="email"
                        name="email"
                        onChange={formik.handleChange}
                        onKeyDown={handleKeyDown} 
                        onBlur={formik.handleBlur}
                        />
                        {formik.errors.email && formik.touched.email && <span className='text-danger'>{formik.errors.email}</span>}
                    </div>
                    <div className="form-group">
                        <label htmlFor='password'>Password</label>
                        <input type="password" className="form-control" placeholder="Password"
                        id="password"
                        name="password"
                        onChange={formik.handleChange}
                        onKeyDown={handleKeyDown}
                        onBlur={formik.handleBlur}  />
                        {formik.errors.password && formik.touched.password && <span className='text-danger'>{formik.errors.password}</span>}
                    </div>
                    <div className='d-flex justify-content-end'>
                        <button type="submit" className="btn btn-primary">Log in</button>
                    </div>
                </form>
            </div>
        </div>
    );
}
