import React from 'react';
import '../style/loader.css'

export default function Loader() {
    
    return (
        <div className="d-flex justify-content-center p-4">
            <div className="lds-roller"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div>
        </div>
    );
}
