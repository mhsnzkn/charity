import React from 'react';

export default function LoaderButton({ children, isLoading, ...rest }) {

    return (
        <button
            {...rest}
            disabled={isLoading}
        >
            {isLoading ?
                <div className="d-flex justify-content-center">
                    <i className="fa fa-circle-o-notch fa-spin "></i>
                </div>
                :
                children}
        </button>
    );
}
