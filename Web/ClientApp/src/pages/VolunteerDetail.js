import React from 'react';
import { Link, useParams } from 'react-router-dom';
import Loader from '../components/Loader';
import { useAxiosGet } from '../Hooks/HttpRequests';
import ErrorPage from './ErrorPage'

export default function VolunteerDetail() {
    let params = useParams();
    let content;

    let response = useAxiosGet('/api/volunteer/' + params.id)

    if (response.error) {
        content = <ErrorPage />;
    }
    if (response.loading) {
        content = <Loader />
    }
    if (response.data) {
        content = <>
            <h5>Personal Details</h5>
            <div className="form-row">
                <div className="form-group col-md-4">
                    <label htmlFor="firstName">Application Date</label>
                    <input id="firstName" value={new Date(response.data.crtDate).toLocaleDateString('uk')} className="form-control" disabled />
                </div>
                <div className="form-group col-md-4">
                    <label htmlFor="firstName">First Name</label>
                    <input id="firstName" value={response.data.firstName} className="form-control" disabled />
                </div>
                <div className="form-group col-md-4">
                    <label htmlFor="lastName">Last Name</label>
                    <input id="lastName" value={response.data.lastName} className="form-control" disabled />
                </div>
                <div className="form-group col-md-4">
                    <label htmlFor="email">Email</label>
                    <input id="email" value={response.data.email} className="form-control" disabled />
                </div>
                <div className="form-group col-md-4">
                    <label htmlFor="mobileNumber">Mobile Number</label>
                    <input id="mobileNumber" value={response.data.mobileNumber} className="form-control" disabled />
                </div>
                <div className="form-group col-md-4">
                    <label htmlFor="homeNumber">Home Number</label>
                    <input value={response.data.homeNumber ?? ""} className="form-control" disabled />
                </div>
                <div className="form-group col-md-8">
                    <label htmlFor="address">Address</label>
                    <input id="address" value={response.data.address} className="form-control" disabled />
                </div>
                <div className="form-group col-md-4">
                    <label htmlFor="postCode">Post Code</label>
                    <input value={response.data.postCode} className="form-control" disabled />
                </div>
            </div>
            <h5>Previous or present employment and voluntary work</h5>
            <div className="form-row">
                {response.data.organisations?.map((item, index) => (
                    <React.Fragment key={index}>
                        <div className="form-group col-md-6">
                            <label htmlFor={`organisations[${index}].organisation`}>Organisation</label>
                            <input value={item.organisation} className="form-control" disabled />
                        </div>
                        <div className="form-group col-md-6">
                            <label htmlFor={`organisations[${index}].role`}>Roles and Responsibilities</label>
                            <input value={item.role} className="form-control" disabled />
                        </div>
                    </React.Fragment>

                ))}
            </div>
            <h5>Skills and Experience</h5>
            <div className="form-row">
                {response.data.skills?.map((item, index) => (
                    <React.Fragment key={index}>
                        <div className="form-group col-md-6">
                            <label htmlFor={`skills[${index}].skill`}>Skill</label>
                            <input value={item.skill} className="form-control" disabled />
                        </div>
                        <div className="form-group col-md-6">
                            <label htmlFor={`skills[${index}].experience`}>Experience</label>
                            <input value={item.experience} className="form-control" disabled />
                        </div>
                    </React.Fragment>
                ))}
            </div>
            <h5>Volunteering with Heart4Refugees</h5>
            <div className="form-group">
                <label htmlFor="reason">Why do you want to volunteer with Heart4Refugees?</label>
                <textarea className="form-control" id="reason" value={response.data.reason} disabled />
            </div>
            {response.data.files?.length > 0 &&
            <>
            <h5>Documents</h5>
            <div className='row'>
            {response.data.files.map( item => {
                return <div class="card col-md-4">
                <img src={item.path} class="card-img-top" alt="Document"/>
              </div>
            })}
            </div>
            </>
            }
        </>
    }

    return (
        <>
            <Link to="/VolunteerApplications" className='btn btn-dark m-1'><i className='fas fa-undo'></i> Back</Link>
            {content}
        </>
    );
}
