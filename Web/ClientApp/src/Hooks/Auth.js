import alertify from "alertifyjs";
import Axios from "axios";
import React, { useState, useEffect, useContext, createContext } from "react";

const authContext = createContext();


// Provider component that wraps your app and makes auth object ...
// ... available to any child component that calls useAuth().
export function ProvideAuth({ children }) {
  const auth = useProvideAuth();
  return <authContext.Provider value={auth}>{children}</authContext.Provider>;
}

// Hook for child components to get the auth object ...
// ... and re-render when it changes.
export const useAuth = () => {
  return useContext(authContext);
};

// Provider hook that creates auth object and handles state
function useProvideAuth() {
  const [user, setUser] = useState(null);
  
  // Wrap any methods we want to use making sure ...
  // ... to save the user to state.
  const signin = (email, password) => {
    return Axios.post("/api/user/Login" , {email:email, password:password})
    .then(res => {
        if(res.data.error){
          alertify.error(res.data.message);
          setUser(null);
        }else{
          localStorage.setItem('session', JSON.stringify(res.data.data))
          setUser(res.data.data);
        }
    })
    .catch((err) => {
      alertify.error("Connection error!");
      setUser(null);
    })
  };

  const signup = (email, username, password) => {
    Axios.post("/api/user/Add" , {email:email, username:username, password:password})
    .then(res => {
        if(res.data.error){
          alertify.error(res.data.message);
        }else{
          alertify.success("To activate your registry, admin should approve");
        }
    })
    .catch((err) => {
      alertify.error("Connection Error");
    })
  };

  const signout = () => {
    localStorage.clear();
    return setUser(null);
  };


//   // Subscribe to user on mount
//   // Because this sets state in the callback it will cause any ...
//   // ... component that utilizes this hook to re-render with the ...
//   // ... latest auth object.
  useEffect(() => {
    const session = localStorage.getItem('session');
    if(session){
      const token = JSON.parse(session).token;
      const unsubscribe = 
          Axios.post("/api/user/Validate" , null,{headers:{'Authorization':'Bearer '+token}})
          .then(res => {
              if(res.data.error===false){
                setUser(res.data);
              }else{
                setUser(false);
              }
          })
          .catch(() => {
            setUser(false);
          })

   // Cleanup subscription on unmount
   return () => unsubscribe();
  }
  return null;
 }, []);
  
  // Return the user object and auth methods
  return {
    user,
    signin,
    signup,
    signout,
  };
}