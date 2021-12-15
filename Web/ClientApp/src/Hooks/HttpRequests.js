import {useEffect, useState} from 'react'
import axios from 'axios'
import { useAuth } from './Auth'


export function useAxiosGet(url){
    let auth = useAuth();
    let token = localStorage.getItem('token');
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
        axios.get(url, {headers:{'Authorization':'Bearer '+token}})
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
                    }
                } else if (err.request) {
                // client never received a response, or request never left
                } else {
                // anything else
                }
                
            })
    }, [url, auth, token])

    return request
}