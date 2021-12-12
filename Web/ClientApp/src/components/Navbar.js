import React from 'react';

export default function Navbar({toggleMenu}) {

    return (
        <nav className="navbar navbar-expand-lg navbar-dark">
            <div className="container-fluid">

                <button type="button" id="sidebarCollapse" className="btn btn-danger" >
                    <i className="fa fa-align-left"></i>
                    <span> Menu</span>
                </button>
                
            </div>
        </nav>
    );
}
