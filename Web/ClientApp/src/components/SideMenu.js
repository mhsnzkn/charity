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
                    <Link to="volunteers">Volunteers</Link>
                </li>
                <li>
                    <Link to="users">Users</Link>
                </li>
                <li>
                    <Link to="myaccount">My Account</Link>
                </li>
                {/* <li>
                    <a href="#pageSubmenu" data-toggle="collapse" aria-expanded="false" class="dropdown-toggle collapsed">Pages</a>
                    <ul class="list-unstyled collapse" id="pageSubmenu" >
                        <li>
                            <Link to="#">Page 1</Link>
                        </li>
                        <li>
                            <Link to="#">Page 2</Link>
                        </li>
                        <li>
                            <Link to="#">Page 3</Link>
                        </li>
                    </ul>
                </li> */}

                <li>
                    <a href="/#" onClick={() => auth.signout()}>Log Out</a>
                </li>
            </ul>
        </nav>
    );
}
