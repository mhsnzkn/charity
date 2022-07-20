import React from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../Hooks/Auth';

export default function SideMenu({ menuActive }) {
    const auth = useAuth();

    return (
        <nav id="sidebar" className={menuActive ? 'active' : ''}>
            <div className="sidebar-header">
                <Link to="/">
                    <img src="/logo_dark.png" className="w-100" alt="logo" />
                </Link>
            </div>

            <ul className="list-unstyled components">
                <li>
                    <a href="#volunteers" data-toggle="collapse" aria-expanded="false" className="dropdown-toggle collapsed">Volunteers</a>
                    <ul className="list-unstyled collapse" id="volunteers" >
                        <li><Link to="VolunteerApplications">Applications</Link></li>
                        <li><Link to="VolunteerExpenses">Expenses</Link></li>
                    </ul>
                </li>
                <li>
                    <Link to="Users">Users</Link>
                </li>
                <li>
                    <Link to="MyAccount">My Account</Link>
                </li>
                <li>
                    <a href="#settings" data-toggle="collapse" aria-expanded="false" className="dropdown-toggle collapsed">Settings</a>
                    <ul className="list-unstyled collapse" id="settings" >
                        <li>
                        <Link to="Agreements">Agreements</Link>
                        </li>
                    </ul>
                </li>
                <li>
                    <a href="/" onClick={() => auth.signout()}>Log Out</a>
                </li>
            </ul>
        </nav>
    );
}
