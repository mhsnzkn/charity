import './bootstrap.min.css';
import Footer from './components/Footer';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import { RoutePaths } from './Routes';
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

function App() {

  return (
    <>
      <Router>

        <Routes>
          <Route path={RoutePaths.Forms} element={<PublicLayout />} >
            <Route path={RoutePaths.VolunteerForm} element={<VolunteerFormPage/>} />
          </Route>
          <Route exact path={RoutePaths.Login} element={<Login />} />





          <Route  path={RoutePaths.Home} element={<AdminLayout />} >
            <Route path={RoutePaths.Volunteers} element={<Volunteers/>} />
            <Route path={RoutePaths.Users} element={<Users/>} />
            <Route path={RoutePaths.MyAccount} element={<MyAccount/>} />
            <Route path={RoutePaths.Logout} element={<Logout/>} />
            
          </Route>

          
          <Route path="*" element={<NotFound/>}/>
        </Routes>


      </Router>
      <Footer />
    </>
  );
}

export default App;
