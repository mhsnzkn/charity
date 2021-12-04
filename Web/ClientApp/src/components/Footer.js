import React from 'react';

export default function Footer() {
    
    return (
        <footer className="border-top footer text-muted p-2  w-100 bg-light" style={{bottom:'0'}}>
        <div className="container text-center">
            &copy; 2021 - <img src='/logo.png' alt='hear4refugees' width='125'/> &emsp; <small>by <a rel="noopener noreferrer" target="_blank" href="https://muhsinozkan.com"><img src="/mylogo.png" width="40" alt='muhsinozkan.com' /></a></small>
        </div>
    </footer>
    );
}
