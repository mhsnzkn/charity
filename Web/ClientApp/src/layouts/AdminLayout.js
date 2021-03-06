import React, { useEffect, useState } from 'react';
import { Outlet, useLocation } from 'react-router-dom';
import Navbar from '../components/Navbar';
import SideMenu from '../components/SideMenu';
import { ProvideAuth } from '../Hooks/Auth';
import AxiosInterceptor from '../Hooks/AxiosInterceptor';
import { PrivateRoute } from '../Wrappers/PrivateRoute';

export default function AdminLayout() {
    const [menuActive, setMenuActive] = useState(false);
    const location = useLocation();
    const toggleMenu = () => {
        setMenuActive(!menuActive);
    }
    useEffect(()=>{
        if(window.innerWidth < 780){
            setMenuActive(false);
        }
    },[location])



    return (
        <ProvideAuth>
            <PrivateRoute>
                <AxiosInterceptor/>
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
