import './bootstrap.min.css';
import './style/heart_theme.css'
import './style/fontawesome/css/all.min.css'
import 'alertifyjs/build/css/alertify.css';
import 'alertifyjs/build/css/themes/semantic.min.css';
import Footer from './components/Footer';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import AgreementEdit from './pages/AgreementEdit';
import PublicLayout from './layouts/PublicLayout';
import NotFound from './pages/NotFound';
import AdminLayout from './layouts/AdminLayout';
import Volunteers from './pages/Volunteers';
import Users from './pages/Users';
import MyAccount from './pages/MyAccount';
import UserAdd from './pages/UserAdd';
import VolunteerDetail from './pages/VolunteerDetail';
import UserEdit from './pages/UserEdit';
import alertify from 'alertifyjs';
import PasswordChangePage from './pages/PasswordChangePage';
import ScrollTop from './Hooks/ScrollTop';
import VolunteerDocumentForm from './pages/VolunteerDocumentForm';
import Agreements from './pages/Agreements';
import SubmitSuccess from './pages/SubmitSuccess';
import VolunteerForm from './pages/VolunteerForm';
import VolunteerAgreementForm from './pages/VolunteerAgreementForm';
import AgreementView from './pages/AgreementView';
import Expenses from './pages/Expenses';
import ExpenseEdit from './pages/ExpenseEdit';

function App() {

  alertify.defaults.transition = "zoom";
  alertify.defaults.theme.ok = "ui positive button";
  alertify.defaults.theme.cancel = "ui black button";
  alertify.defaults.notifier.position = "top-right";


  return (
    <>
      <Router>
        <ScrollTop />

        <Routes>
          <Route path="Forms" element={<PublicLayout />} >
            <Route path="VolunteerForm" element={<VolunteerForm />} />
            <Route path="VolunteerDocument/:key" element={<VolunteerDocumentForm />} />
            <Route path="VolunteerAgreement/:key" element={<VolunteerAgreementForm />} />
            <Route path="Completed" element={<SubmitSuccess />} />
          </Route>



          <Route path="/" element={<AdminLayout />} >
            <Route path="VolunteerApplications" element={<Volunteers />} />
            <Route path="VolunteerApplications/Detail/:id" element={<VolunteerDetail />} />
            <Route path="VolunteerExpenses" element={<Expenses />} />
            <Route path="VolunteerExpenses/Add" element={<ExpenseEdit />} />
            <Route path="VolunteerExpenses/Edit/:id" element={<ExpenseEdit />} />
            <Route path="Users" element={<Users />} />
            <Route path="Users/Add" element={<UserAdd />} />
            <Route path="Users/Edit/:id" element={<UserEdit />} />
            <Route path="Users/PassChange/:id" element={<PasswordChangePage />} />
            <Route path="MyAccount" element={<MyAccount />} />
            <Route path="Agreements" element={<Agreements />} />
            <Route path="Agreements/Add" element={<AgreementEdit />} />
            <Route path="Agreements/Edit/:id" element={<AgreementEdit />} />
            <Route path="Agreements/View/:id" element={<AgreementView />} />
          </Route>


          <Route path="*" element={<NotFound />} />

        </Routes>


      </Router>
      <Footer />
    </>
  );
}

export default App;
