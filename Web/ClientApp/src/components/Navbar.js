import React from 'react';

export default function Navbar({toggleMenu}) {

    return (
        <nav className="navbar navbar-expand-lg navbar-dark bg-custom">
            <div className="container-fluid">

                <button type="button" id="sidebarCollapse" className="btn btn-danger" onClick={toggleMenu}>
                    <i className="fas fa-align-left"></i>
                    <span>Menu</span>
                </button>
                <button className="btn btn-dark d-inline-block d-lg-none ml-auto" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <i className="fas fa-align-justify"></i>
                </button>

                <div className="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul className="nav navbar-nav ml-auto">
                        <li className="nav-item">
                            <a href="/Identity/Account/Manage/ChangePassword" className="nav-link" title="Hesabım"><i className="fas fa-user-circle fa-2x"></i></a>
                        </li>
                        <li className="nav-item">
                            <a href="/Identity/Account/Logout" className="nav-link" title="Çıkış"><i className="fas fa-sign-out-alt fa-2x"></i></a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    );
}
