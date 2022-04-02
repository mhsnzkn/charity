import {useEffect, useState} from 'react'
import axios from 'axios'
import { useAuth } from './Auth'


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
        axios.get(url)
            .then(response => {
                setRequest({
                    loading: false,
                    data: response.data,
                    error: false
                })
            })
            .catch( err =>{
                setRequest({
                    loading: false,
                    data: null,
                    error: true
                })
            })
    }, [url, auth, state])

    return request
}

