import React from 'react';
import { useAuth } from '../Hooks/Auth';
import Login from '../pages/Login';

// A wrapper for <Route> that redirects to the login
// screen if you're not yet authenticated.
export function PrivateRoute({ children }) {
  const auth = useAuth();
  return (
    auth.user ? (
      children
    ) : (
      <Login />
    )
  );
}
