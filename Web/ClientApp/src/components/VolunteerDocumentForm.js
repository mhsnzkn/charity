import React, { useState } from 'react';
import axios from 'axios';
import { toast } from 'react-toastify';
import LoaderButton from './LoaderButton';
import { useParams } from 'react-router-dom';

export default function VolunteerDocumentForm({ submit }) {
    const params = useParams();
    const [btnLoading, setBtnLoading] = useState(false);
    const [values, setValues] = useState([])
    const [validation, setValidation] = useState(true);

    const submitHandler = () => {
        if (values.length < 3) {
            setValidation(false);
            return;
        }
        setBtnLoading(true);
        let formData = new FormData();
        values.forEach(item => formData.append('files', item));
        //formData.append('files', values);
        formData.append('key', params.key);
        axios.post('/api/volunteer/documents', formData)
            .then((res) => {
                if (res.data.error) {
                    toast.error(res.data.message)
                } else {
                    submit();
                }
                setBtnLoading(false);
            })
            .catch(err => {
                toast.error("Submit unsuccessful!")
                setBtnLoading(false);
            })
    }

    const chg = (e) => {
        let files = [];
        for (let i = 0; i < e.length; i++) {
            files.push(e[i]);

        }
        setValues(files);

        setValidation(files.length >= 3);
    }

    return (
        <div className='p-2 mb-2'>
            <h3>Volunteer Documents Form</h3>
            <hr />
            <div className="form-row">
                <div className="form-group col-md-12">
                    <label htmlFor="document1">Please upload at least 3 documents</label>
                    <input id="document1" name="document1" className="form-control" type='file' multiple
                        onChange={(e) => chg(e.target.files)}
                    />
                    {!validation && <small className='text-danger'>There has to be at least 3 documents</small>}
                </div>
            </div>

            <div className='d-flex justify-content-end'>
                <LoaderButton isLoading={btnLoading} onClick={submitHandler} className="btn btn-primary">Submit</LoaderButton>
            </div>
        </div>
    );
}
