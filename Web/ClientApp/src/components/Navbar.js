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
                
            <a href='https://mail.ionos.co.uk' target="_blank" rel='noreferrer' className='btn btn-small btn-dark float-right' title='Go to mail'><i className='fas fa-envelope'></i></a>
            </div>
        </nav>
    );
}
