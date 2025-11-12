import React, { useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom'; // Importe o Navigate
import Layout from './components/layout/Layout';
import './styles/css/site.css';
import './styles/App.css';
import 'bootswatch/dist/lumen/bootstrap.min.css';
import OrdemPage from './pages/OrdemPage';
import VeiculoPage from './pages/VeiculoPage';
import Dashboard from './pages/DashboadPage';

function App() {
    useEffect(() => {
        document.documentElement.lang = 'pt-BR';
        document.documentElement.charset = 'UTF-8';
    }, []);

    return (
        <Router>
            <Layout>
                <Routes>
                    {/* Adicione esta linha para redirecionar a rota raiz */}
                    <Route path="/" element={<Navigate replace to="/dashboard" />} />
                    
                    <Route path="/dashboard" element={<Dashboard />} />
                    <Route path="/ordem" element={<OrdemPage />} />
                    <Route path="/veiculos" element={<VeiculoPage />} />
                </Routes>
            </Layout>
        </Router>
    );
}

export default App;