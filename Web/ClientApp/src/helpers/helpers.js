
export const getToken = ()=>{
    return localStorage.getItem('token');
}

export const getHttpHeader = () =>{
    return {headers:{'Authorization':'Bearer '+getToken()}};
}