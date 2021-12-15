import './bootstrap.min.css';
import Footer from './components/Footer';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Home from './pages/Home';
import Login from './pages/Login';
import './style/heart_theme.css'
import './style/fontawesome/css/font-awesome.min.css'
import VolunteerFormPage from './pages/VolunteerFormPage';
import PublicLayout from './layouts/PublicLayout';
import NotFound from './pages/NotFound';
import AdminLayout from './layouts/AdminLayout';
import Volunteers from './pages/Volunteers';
import Users from './pages/Users';
import MyAccount from './pages/MyAccount';
import Logout from './pages/Logout';
import UserEdit from './pages/UserEdit';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { ProvideAuth } from './Hooks/Auth';
import { PrivateRoute } from './Wrappers/PrivateRoute';

function App() {



  return (
    <>
      <ProvideAuth>
        <Router>

          <Routes>
            <Route path="forms" element={<PublicLayout />} >
              <Route path="volunteerForm" element={<VolunteerFormPage />} />
            </Route>
            <Route exact path="login" element={<Login />} />





              <Route path="/" element={ <AdminLayout />} >
                <Route path="volunteers" element={<Volunteers />} />
                <Route path="users" element={<Users />} />
                <Route path="users/:id" element={<UserEdit />} />
                <Route path="myAccount" element={<MyAccount />} />
                <Route path="logout" element={<Logout />} />

              </Route>


              <Route path="*" element={<NotFound />} />

          </Routes>


        </Router>
        <ToastContainer />
        <Footer />
      </ProvideAuth>
    </>
  );
}

export default App;
