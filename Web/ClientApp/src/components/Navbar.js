import React from 'react';
import { useAuth } from '../Hooks/Auth';

export default function Navbar({toggleMenu}) {
    const auth = useAuth();

    return (
        <nav className="navbar navbar-expand-lg navbar-dark">
            <div className="container-fluid">

                <button type="button" id="sidebarCollapse" onClick={toggleMenu} className="btn btn-danger" >
                    <i className="fas fa-align-left"></i>
                    <span> Menu</span>
                </button>
                
            <button className='btn btn-small btn-dark float-right ' onClick={() => auth.signout()}><i className='fas fa-sign-out-alt'></i></button>
            </div>
        </nav>
    );
}
