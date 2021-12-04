import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import {RoutePaths} from '../Routes';


export default function Logout() {
    let navigate = useNavigate();
    
    useEffect(()=>{
        navigate("/"+RoutePaths.Login);
    })
    return (
        <>
            <h1>Logout</h1>
        </>
    );
}
