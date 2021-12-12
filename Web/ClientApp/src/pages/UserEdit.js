import React from 'react';
import { useParams } from 'react-router-dom';

export default function UserEdit() {
    let params = useParams();
    
    return (
        <>
            <h4>User add/edit</h4><hr />
            <h1>{params.id}</h1>
        </>
    );
}
