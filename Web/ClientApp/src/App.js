import './bootstrap.min.css';
import './style/heart_theme.css'
import './style/fontawesome/css/all.min.css'
import 'react-toastify/dist/ReactToastify.css';
import 'alertifyjs/build/css/alertify.css';
import Footer from './components/Footer';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import VolunteerFormPage from './pages/VolunteerFormPage';
import PublicLayout from './layouts/PublicLayout';
import NotFound from './pages/NotFound';
import AdminLayout from './layouts/AdminLayout';
import Volunteers from './pages/Volunteers';
import Users from './pages/Users';
import MyAccount from './pages/MyAccount';
import UserAdd from './pages/UserAdd';
import { ToastContainer } from 'react-toastify';
import VolunteerDocumentForm from './components/VolunteerDocumentForm';
import VolunteerDetail from './pages/VolunteerDetail';
import UserEdit from './pages/UserEdit';
import alertify from 'alertifyjs';
import PasswordChangePage from './pages/PasswordChangePage';
import ScrollTop from './Hooks/ScrollTop';

function App() {

  alertify.defaults.transition = "zoom";
  alertify.defaults.theme.ok = "ui positive button";
  alertify.defaults.theme.cancel = "ui black button";


  return (
    <>
      <Router>
        <ScrollTop />

        <Routes>
          <Route path="forms" element={<PublicLayout />} >
            <Route path="volunteerForm" element={<VolunteerFormPage />} />
            <Route path="volunteerDocument" element={<VolunteerDocumentForm />} />
          </Route>



          <Route path="/" element={<AdminLayout />} >
            <Route path="VolunteerApplications" element={<Volunteers />} />
            <Route path="VolunteerApplications/Detail/:id" element={<VolunteerDetail />} />
            <Route path="Users" element={<Users />} />
            <Route path="Users/Add" element={<UserAdd />} />
            <Route path="Users/Edit/:id" element={<UserEdit />} />
            <Route path="Users/PassChange/:id" element={<PasswordChangePage />} />
            <Route path="MyAccount" element={<MyAccount />} />
          </Route>


          <Route path="*" element={<NotFound />} />

        </Routes>


      </Router>
      <Footer />
      <ToastContainer />
    </>
  );
}

export default App;
