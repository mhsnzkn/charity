import React, { useEffect, useState } from 'react';
import EmailChange from '../components/EmailChange';
import JobChange from '../components/JobChange';
import PasswordChange from '../components/PasswordChange';
import {useAxiosGet} from '../Hooks/HttpRequests'

export default function MyAccount() {
const [userInfo, setUserInfo] = useState({})

const response = useAxiosGet('/api/user/info');

useEffect(()=>{
    if(response.data){
        setUserInfo(response.data);
    }
},[response.data])

const updateInfo = (key, value)=>{
    setUserInfo({...userInfo, [key]:value})
}



    return (
        <>
            <h4>My Account</h4><hr />
            <EmailChange currentEmail={userInfo.email} onChange={(value)=> updateInfo("email",value)}/>
            <hr />
            <JobChange currentJob={userInfo.job} onChange={(value)=> updateInfo("job",value)}/>
            <hr />
            <PasswordChange />

        </>
    );
}
