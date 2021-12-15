import './bootstrap.min.css';
import Footer from './components/Footer';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './style/heart_theme.css'
import './style/fontawesome/css/font-awesome.min.css'
import VolunteerFormPage from './pages/VolunteerFormPage';
import PublicLayout from './layouts/PublicLayout';
import NotFound from './pages/NotFound';
import AdminLayout from './layouts/AdminLayout';
import Volunteers from './pages/Volunteers';
import Users from './pages/Users';
import MyAccount from './pages/MyAccount';
import UserEdit from './pages/UserEdit';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import VolunteerDocumentForm from './components/VolunteerDocumentForm';

function App() {



  return (
    <>
      <Router>

        <Routes>
          <Route path="forms" element={<PublicLayout />} >
            <Route path="volunteerForm" element={<VolunteerFormPage />} />
            <Route path="volunteerDocument" element={<VolunteerDocumentForm />} />
          </Route>



          <Route path="/" element={<AdminLayout />} >
            <Route path="volunteers" element={<Volunteers />} />
            <Route path="users" element={<Users />} />
            <Route path="users/:id" element={<UserEdit />} />
            <Route path="myAccount" element={<MyAccount />} />

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
