import React from 'react';
import { Outlet } from 'react-router-dom';

export default function PublicLayout() {

    return (
        <>
            <nav className="navbar navbar-light bg-light">
                <div className="navbar-brand" >
                    <img className='img-fluid' src="/logo.png" style={{ maxHeight: '200px' }} alt="logo" />
                </div>
            </nav>
            <div className='container layout-div-height'>

                <Outlet />


            </div>
        </>
    );
}
