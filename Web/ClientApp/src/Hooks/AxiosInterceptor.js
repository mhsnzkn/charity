import alertify from 'alertifyjs';
import axios from 'axios';
import React from 'react'
import { getBearerToken } from '../helpers/helpers';
import { useAuth } from './Auth';

const AxiosInterceptor = () => {
    const auth = useAuth();

    // For GET requests
    axios.interceptors.request.use(
        (config) => {
            config.headers.Authorization = getBearerToken();
            return config;
        },
        (err) => {
            if (err.response) {
                if(err.response.status=== 401 || err.response.status===403){
                    auth.signout();
                }else{
                    alertify.error('Connection error!');
                }
            } else if (err.request) {
            // client never received a response, or request never left
            alertify.error('Request Timed Out!');
            } else {
            // anything else
            alertify.error('Something went wrong!');
            }
            return Promise.reject(err);
        }
    );

    // For POST requests
    axios.interceptors.response.use(
        (config) => {
            // Add configurations here
            config.headers.Authorization = getBearerToken();
            return config;
        },
        (err) => {
            if (err.response) {
                if(err.response.status=== 401 || err.response.status===403){
                    auth.signout();
                }else{
                    alertify.error('Connection error!');
                }
            } else if (err.request) {
            // client never received a response, or request never left
            alertify.error('Request Timed Out!');
            } else {
            // anything else
            alertify.error('Something went wrong!');
            }
            return Promise.reject(err);
        }
    );

    return <></>
}

export default AxiosInterceptor;