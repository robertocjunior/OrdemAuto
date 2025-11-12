import React, { useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Layout from './components/layout/Layout';
import './styles/css/site.css';
import './styles/App.css';
import 'bootswatch/dist/lumen/bootstrap.min.css';
import OrdemPage from './pages/OrdemPage';
import VeiculoPage from './pages/VeiculoPage';
function App() {
    useEffect(() => {
        document.documentElement.lang = 'pt-BR';
        document.documentElement.charset = 'UTF-8';
    }, []);

    return (
        <Router>
            <Layout>
                <Routes>
                    <Route path="/ordem" element={<OrdemPage />} />
                    <Route path="/veiculos" element={<VeiculoPage />} />
                </Routes>
            </Layout>
        </Router>
    );
}

export default App;
