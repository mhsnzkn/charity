import React from 'react';
import CollapsibleDropdown from './CollapsibleDropdown';
import { RoutePaths } from '../Routes';
import { Link } from 'react-router-dom';

export default function SideMenu({menuActive}) {
    
    return (
        <nav id="sidebar" className={menuActive ? 'active' : ''}>
            <div className="sidebar-header">
                <Link to="/">
                    <img src="/logo_dark.png" className="w-100" alt="logo"/>
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

                    {/* <CollapsibleDropdown title="Diger Islemler" items={[{path:RoutePaths.Bir, name:"Biir"},{path:RoutePaths.Iki, name:"Ikki"}]}/> */}

                    <li>
                        <Link to={RoutePaths.Logout}>Log Out</Link>
                    </li>
            </ul>
        </nav>
    );
}
