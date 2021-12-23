
export const getToken = ()=>{
    return localStorage.getItem('token');
}

export const getHttpHeader = () =>{
    return {headers:{'Authorization':'Bearer '+getToken()}};
}

export const getLengthUrl = (url) => {
    let paramUrl = new URLSearchParams(url);
    return paramUrl.get("length");
}
export const getPageIndex = (url) => {
    let paramUrl = new URLSearchParams(url.split("?")[1]);
    let length = paramUrl.get("length");
    let start = paramUrl.get("start");
    return (start/length)+1;
}