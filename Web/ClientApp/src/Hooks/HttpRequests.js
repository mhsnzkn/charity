import {useEffect, useState} from 'react'
import axios from 'axios'
import { useAuth } from './Auth'
import { getHttpHeader } from '../helpers/helpers';
import alertify from 'alertifyjs';


export function useAxiosGet(url, state = null){
    let auth = useAuth();
    const [request, setRequest] = useState({
        loading: false,
        data: null,
        error: false
    })
    useEffect(() => {
        setRequest({
            loading: true,
            data: null,
            error: false
        })
        axios.get(url, getHttpHeader())
            .then(response => {
                setRequest({
                    loading: false,
                    data: response.data,
                    error: false
                })
            })
            .catch((err) => {
                setRequest({
                    loading: false,
                    data: null,
                    error: true
                })
                if (err.response) {
                    if(err.response.status=== 401 || err.response.status===403){
                        auth.signout();
                    }else{
                        alertify.error('Connection error!');
                    }
                } else if (err.request) {
                // client never received a response, or request never left
                alertify.error('Connection error!');
                } else {
                // anything else
                alertify.error('Connection error!');
                }
                
            })
    }, [url, auth, state])

    return request
}

