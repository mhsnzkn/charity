import React from 'react';
import { RoutePaths } from '../Routes';
import { Link } from 'react-router-dom';

export default function SideMenu({ menuActive }) {

    return (
        <nav id="sidebar" className={menuActive ? 'active' : ''}>
            <div className="sidebar-header">
                <Link to="/">
                    <img src="/logo_dark.png" className="w-100" alt="logo" />
                </Link>
            </div>

            <ul className="list-unstyled components">
                <li>
                    <Link to={RoutePaths.Volunteers}>Volunteers</Link>
                </li>
                <li>
                    <Link to={RoutePaths.Users}>Users</Link>
                </li>
                <li>
                    <Link to={RoutePaths.MyAccount}>My Account</Link>
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
                    <Link to={RoutePaths.Logout}>Log Out</Link>
                </li>
            </ul>
        </nav>
    );
}
