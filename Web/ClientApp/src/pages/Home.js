import React, { useState } from 'react';
import Navbar from '../components/Navbar';
import SideMenu from '../components/SideMenu';

export default function Home() {
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

                @RenderBody()


            </div>
        </div>
    </div>
    );
}
