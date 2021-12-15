import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';


export default function Logout() {
    let navigate = useNavigate();
    
    useEffect(()=>{
        navigate("/login");
    })
    return (
        <>
            <h1>Logout</h1>
        </>
    );
}
