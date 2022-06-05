
export const getToken = ()=>{
    const session = localStorage.getItem('session');
    return JSON.parse(session).token;
}
export const getBearerToken = ()=>{
    return 'Bearer '+getToken();
}
export const getHttpHeader = () =>{
    return {headers:{'Authorization': getBearerToken()}};
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