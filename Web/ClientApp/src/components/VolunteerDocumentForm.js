import React, { useState } from 'react';
import axios from 'axios';
import LoaderButton from './LoaderButton';
import { useNavigate, useParams } from 'react-router-dom';
import alertify from 'alertifyjs';

export default function VolunteerDocumentForm() {
    const params = useParams();
    const navigate = useNavigate();
    const [btnLoading, setBtnLoading] = useState(false);
    const [values, setValues] = useState([])
    const [validation, setValidation] = useState(true);

    const submitHandler = () => {
        if (!(values.document1 && values.document2 && values.document3)) {
            setValidation(false);
            alertify.error("At least 3 documenst should be uploaded")
            return;
        }
        setBtnLoading(true);
        let formData = new FormData();
        formData.append('key', params.key);
        formData.append('files', values.document1);
        formData.append('files', values.document2);
        formData.append('files', values.document3);
        axios.post('/api/volunteer/documents', formData)
            .then((res) => {
                if (res.data.error) {
                    alertify.error(res.data.message)
                } else {
                    navigate("/Forms/Completed");
                }
                setBtnLoading(false);
            })
            .catch(err => {
                alertify.error("Submit unsuccessful!")
                setBtnLoading(false);
            })
    }

    const chg = (key, file) => {
        
        setValues({...values, [key]:file});

    }

    return (
        <div className='p-2 mb-2'>
            <h3>Volunteer Documents Form</h3>
            <hr />
            {!validation && <small className='text-danger'>There has to be 3 documents</small>}
            <div className="form-row">
                <div className="form-group col-md-4">
                    <label htmlFor="document1">Document 1</label>
                    <input id="document1" name="document1" className="form-control" type='file'
                        onChange={(e) => chg(e.target.name, e.target.files[0])}
                    />
                </div>
                <div className="form-group col-md-4">
                    <label htmlFor="document2">Document 2</label>
                    <input id="document2" name="document2" className="form-control" type='file'
                        onChange={(e) => chg(e.target.name, e.target.files[0])}
                    />
                </div>
                <div className="form-group col-md-4">
                    <label htmlFor="document3">Document 3</label>
                    <input id="document3" name="document3" className="form-control" type='file'
                        onChange={(e) => chg(e.target.name, e.target.files[0])}
                    />
                </div>
            </div>

            <div className='d-flex justify-content-end'>
                <LoaderButton isLoading={btnLoading} onClick={submitHandler} className="btn btn-primary">Submit</LoaderButton>
            </div>
        </div>
    );
}
