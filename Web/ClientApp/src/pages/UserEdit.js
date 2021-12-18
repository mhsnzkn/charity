import alertify from 'alertifyjs';
import React from 'react';
import { useParams } from 'react-router-dom';

export default function UserEdit() {
    let params = useParams();
    
    return (
        <>
            <h4>User add/edit</h4><hr />
            <h1>{params.id}</h1>
            <button className='btn btn-primary' onClick={() => alertify.success("Wow so easy !")}>Button</button>
            {/* <button className='btn btn-primary' onClick={() => alertify.warning("Wow so easy !")}>Button</button>
            <button className='btn btn-primary' onClick={() => alertify.error("Wow so easy !")}>Button</button>
            <button className='btn btn-primary' onClick={() => alertify.notify("Wow so easy !")}>Button</button> */}
        </>
    );
}
