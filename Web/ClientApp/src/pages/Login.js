import React from 'react';

export default function Login() {

    return (
        <div className='d-flex justify-content-center' style={{ height: "92vh", backgroundImage: 'url(/wood-hearts-4k.jpg)' }}>
            <div className='align-self-center p-4 rounded' style={{backgroundColor:'rgb(149 154 192 / 95%)'}}>
                <img className='img-fluid' src='/logo.png' alt='heart4refugees' style={{maxWidth:'300px'}}/>
                <form>
                    <div className="form-group">
                        <label>Email address</label>
                        <input type="email" className="form-control" placeholder="Enter email" />
                    </div>
                    <div className="form-group">
                        <label>Password</label>
                        <input type="password" className="form-control" placeholder="Password" />
                    </div>
                    <div className='d-flex justify-content-end'>
                        <button type="button" className="btn btn-primary">Log in</button>
                    </div>
                </form>
            </div>
        </div>
    );
}
