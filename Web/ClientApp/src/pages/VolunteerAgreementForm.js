import React, { useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import Loader from '../components/Loader';
import { useAxiosGet } from '../Hooks/HttpRequests';
import NotFound from './NotFound';
import alertify from 'alertifyjs';
import axios from 'axios';
import LoaderButton from '../components/LoaderButton';

export default function VolunteerAgreementForm() {
    const params = useParams()
    const navigate = useNavigate()
    const response = useAxiosGet("/api/Agreement/Volunteer/" + params.key);
    const [order, setOrder] = useState(0)
    const [values, setValues] = useState([]);
    const [check, setCheck] = useState(false);
    const [btnLoading, setBtnLoading] = useState(false)
    let content;

    const addAgreement = (id) => {
        if (check) {
            let list = []
            if (values)
                list = values
            list.push(id)
            setOrder(order + 1);
            setValues(list)
            setCheck(false)
        } else {
            alertify.warning("You need to check the checkbox to continue")
        }
    }
    const submitForm = () => {
        let model = {};
        model.agreementIds = values;
        model.key = params.key;
        axios.post("/api/Agreement/Volunteer", model)
            .then((res) => {
                if (res.data.error) {
                    alertify.error(res.data.message)
                } else {
                    navigate("/Forms/Completed");
                }
            })
            .finally(() => {
                setBtnLoading(false);
            })
    }

    if (response.loading) {
        content = <Loader />
    }
    if (response.error) {
        content = <NotFound />
    }
    if (response.data) {
        content = response.data[order] ?
            <div key={response.data[order].id}>
                <h5>{response.data[order].title}</h5>
                <div className='border p-1 bg-light' dangerouslySetInnerHTML={{ __html: response.data[order].content }}></div>
                <div className='d-flex justify-content-center'>
                    <div className="form-check">
                        <input type="checkbox" id={response.data[order].id} className="form-check-input" onChange={(e) => { setCheck(e.target.checked) }} />
                        <label className="form-check-label" htmlFor={response.data[order].id}>I have read &amp; accept</label>
                        {!check && <><br /><small className="text-danger">*Required</small></>}
                    </div>
                </div>
                <div className="d-flex justify-content-end">
                    <button onClick={() => addAgreement(response.data[order].id)} className="btn btn-primary">Next</button>
                </div>
            </div>
            :
            <><p>I have read and understand all of the terms stated above, and have read and agreed each line to signify my compliance with each and every agreement. Heart4Refugees reserves the right to modify, update, or add to this agreement at any time. I understand that I will have the opportunity to review any changes.</p>
                <div className="d-flex justify-content-center">
                    <LoaderButton isLoading={btnLoading} onClick={() => submitForm()} className="btn btn-primary">Submit</LoaderButton>
                </div>
            </>


    }

    return (
        <>
            <div className='p-2 mb-2'>
                <h3>Volunteer Agreement</h3>
                <hr />

                {content}

            </div>
        </>
    );
}
