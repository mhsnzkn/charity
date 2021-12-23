import React from 'react';
import { Link } from 'react-router-dom';
import PasswordChange from '../components/PasswordChange';

export default function PasswordChangePage() {
    
    return (
        <>
        <Link to="/Users" className='btn btn-dark m-1'><i className='fa fa-undo'></i> Back</Link>
            <PasswordChange/>
        </>
    );
}
