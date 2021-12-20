
export const getToken = ()=>{
    return localStorage.getItem('token');
}

export const getHttpHeader = () =>{
    return {headers:{'Authorization':'Bearer '+getToken()}};
}

export const getUrlSearchString = (page = 0, pageSize = 10, searchString = "") =>{
    return `?start=${page * pageSize}&length=${pageSize}&searchString=${searchString}`
}