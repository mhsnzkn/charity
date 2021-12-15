import React, { useState } from 'react';
import { Outlet } from 'react-router-dom';
import Navbar from '../components/Navbar';
import SideMenu from '../components/SideMenu';
import { ProvideAuth } from '../Hooks/Auth';
import { PrivateRoute } from '../Wrappers/PrivateRoute';

export default function AdminLayout() {
    const [menuActive, setMenuActive] = useState(false);
    const toggleMenu = () => {
        setMenuActive(!menuActive);
    }

    return (
        <ProvideAuth>
            <PrivateRoute>
                <div className="wrapper layout-div-height">
                    <SideMenu menuActive={menuActive} />

                    <div id="content">
                        <div>
                            <Navbar toggleMenu={toggleMenu} />

                            <Outlet />


                        </div>
                    </div>
                </div>
            </PrivateRoute>
        </ProvideAuth>
    );
}
