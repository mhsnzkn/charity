import React from 'react';

export default function Login() {

    return (
        <div className='d-flex justify-content-center' style={{ height: "92vh", backgroundImage: 'url(/wood-hearts-4k.jpg)' }}>
            <div className='align-self-center p-4 rounded' style={{backgroundColor:'rgb(149 154 192 / 95%)'}}>
                <img className='img-fluid' src='/logo.png' alt='heart4refugees' style={{maxWidth:'300px'}}/>
                <form className=''>
                    <div class="form-group">
                        <label for="exampleInputEmail1">Email address</label>
                        <input type="email" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="Enter email" />
                    </div>
                    <div class="form-group">
                        <label for="exampleInputPassword1">Password</label>
                        <input type="password" class="form-control" id="exampleInputPassword1" placeholder="Password" />
                    </div>
                    <div className='d-flex justify-content-end'>
                        <button type="button" class="btn btn-primary">Submit</button>
                    </div>
                </form>
            </div>
        </div>
    );
}
