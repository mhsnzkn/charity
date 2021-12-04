import React, { useState } from 'react';
import { Outlet } from 'react-router-dom';
import Navbar from '../components/Navbar';
import SideMenu from '../components/SideMenu';

export default function AdminLayout() {
    const[menuActive, setMenuActive] = useState(false);
    const toggleMenu = ()=>{
        setMenuActive(!menuActive);
    }

    return (
        <div className="wrapper">
        <SideMenu menuActive={menuActive}/>

        <div id="content">
            <div>
                <Navbar toggleMenu={toggleMenu}/>

                <Outlet/>


            </div>
        </div>
    </div>
    );
}
