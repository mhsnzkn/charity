import React from 'react';
import { useParams } from 'react-router-dom';
import { toast } from 'react-toastify';

export default function UserEdit() {
    let params = useParams();
    
    return (
        <>
            <h4>User add/edit</h4><hr />
            <h1>{params.id}</h1>
            <button className='btn btn-primary' onClick={() => toast("Wow so easy !")}>Button</button>
            <button className='btn btn-primary' onClick={() => toast.success("Wow so easy !")}>Button</button>
            <button className='btn btn-primary' onClick={() => toast.warn("Wow so easy !")}>Button</button>
            <button className='btn btn-primary' onClick={() => toast.warning("Wow so easy !")}>Button</button>
            <button className='btn btn-primary' onClick={() => toast.error("Wow so easy !")}>Button</button>
            <button className='btn btn-primary' onClick={() => toast.info("Wow so easy !")}>Button</button>
        </>
    );
}
