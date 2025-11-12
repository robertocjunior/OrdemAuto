// src/config.js
const env = process.env.NODE_ENV || 'development';

const config = {
    development: {
        // Correção: Apontando para a URL do seu launchSettings.json + /api
        baseApiUrl: 'http://localhost:5084/api' 
    },
    production: {
        // Mantido para quando você publicar o projeto
        baseApiUrl: `${window.location.origin}/api`
    }
};

export default config[env];