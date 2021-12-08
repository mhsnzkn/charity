import React from 'react';
import CollapsibleMenuItem from './CollapsibleMenuItem';
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

                <CollapsibleMenuItem title="Diger Islemler">
                    <li><Link to="Denen">deneme</Link></li>
                    <li><Link to="Denen">deneme1</Link></li>
                    <li><Link to="Denen">deneme2</Link></li>
                </CollapsibleMenuItem>

                <CollapsibleMenuItem title="Diger Islemler2" >
                    <li><Link to="Denen">deneme</Link></li>
                </CollapsibleMenuItem>

                <li>
                    <Link to={RoutePaths.Logout}>Log Out</Link>
                </li>
            </ul>
        </nav>
    );
}
