import './bootstrap.min.css';
import Footer from './components/Footer';
import {BrowserRouter as Router, Route, Routes} from 'react-router-dom';
import { RoutePaths } from './Routes';
import Home from './pages/Home';
import Login from './pages/Login';
import './heart_theme.css'

function App() {

  return (
  <>
  <Router>


    
    <Routes>
        <Route exact path={RoutePaths.Login} element={<Login/>} />
        <Route exact path={RoutePaths.Home} element={<Home/>} />
    </Routes>
    
    

  </Router>
    <Footer/>
	</>
  );
}

export default App;
