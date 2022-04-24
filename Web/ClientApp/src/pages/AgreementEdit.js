import { useFormik } from 'formik';
import React, { useState, useEffect } from 'react';
import { Link, useNavigate, useParams } from 'react-router-dom';
import LoaderButton from '../components/LoaderButton';
import Loader from '../components/Loader';
import * as yup from 'yup'
import { getHttpHeader } from '../helpers/helpers';
import axios from 'axios';
import alertify from 'alertifyjs';
import { CKEditor } from '@ckeditor/ckeditor5-react';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';

export default function AgreementEdit() {
    const params = useParams();
    const navigate = useNavigate();
    const [btnLoading, setBtnLoading] = useState(false);
    const [values, setValues] = useState({});
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
            })
            .finally(() => setBtnLoading(false))
    }
    const formik = useFormik({
        enableReinitialize: true,
        initialValues: {
            title: values.title || '',
            order: values.order || 1,
            content: values.content || '',
            isActive: values.isActive ?? true
        },
        onSubmit: submitHandler,
        validationSchema: yup.object().shape({
            title: yup.string().required('Required'),
            order: yup.number().required('Required'),
            content: yup.string().required('Required'),
            isActive: yup.bool()
        })
    });

    return (
        <>
            <Link to="/Agreements" className='btn btn-dark m-1'><i className='fas fa-undo'></i> Back</Link>
            {loading ?
                <Loader />
                :
                <form onSubmit={formik.handleSubmit}>
                    <h5>Details</h5>
                    <div className="form-row">
                        <div className="form-group col-md-6">
                            <label htmlFor="title">Title</label>
                            <input id="title" name="title" className="form-control"
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                                value={formik.values.title}
                            />
                            {formik.touched.title && formik.errors.title && <small className='text-danger'>{formik.errors.title}</small>}
                        </div>
                        <div className="form-group col-md-6">
                            <label htmlFor="order">Order</label>
                            <input type="number" id="order" name="order" className="form-control"
                                onChange={formik.handleChange}
                                onBlur={formik.handleBlur}
                                value={formik.values.order}
                            />
                            {formik.touched.order && formik.errors.order && <small className='text-danger'>{formik.errors.order}</small>}
                        </div>
                        <div className="form-check mb-4">
                            <input type="checkbox" id="isActive" name="isActive" className="form-check-input"
                                onChange={formik.handleChange}
                                value={formik.values.isActive}
                                checked={formik.values.isActive}
                            />
                            <label className="form-check-label" htmlFor="isActive">
                                Status
                            </label>
                        </div>
                        <div className="form-group col-md-12">
                            <label htmlFor="contenttext">Content</label>
                            <CKEditor
                                editor={ClassicEditor}
                                data={values?.content}
                                onChange={(event, editor) => {
                                    //const data = editor.getData();
                                    formik.setFieldValue('content', editor.getData())
                                }}
                            />
                            {formik.touched.content && formik.errors.content && <small className='text-danger'>{formik.errors.content}</small>}
                        </div>
                    </div>

                    <div className='d-flex justify-content-end'>
                        <LoaderButton isLoading={btnLoading} type="submit" className="btn btn-primary">Save</LoaderButton>
                    </div>
                </form>

            }
        </>
    );
}
