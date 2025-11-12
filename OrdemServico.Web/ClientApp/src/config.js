// src/config.js
const env = process.env.NODE_ENV || 'development';

const config = {
    development: {
        // Esta URL é para quando você roda 'npm start' localmente (sem Docker)
        baseApiUrl: 'http://localhost:5084/api' 
    },
    production: {
        // Esta URL virá do docker-compose.yml quando rodando em produção
        // O React (Create React App) injeta a variável de ambiente aqui
        baseApiUrl: process.env.REACT_APP_API_BASE_URL
    }
};

// Fallback para garantir que 'production' nunca seja nulo
if (env === 'production' && !config.production.baseApiUrl) {
    console.warn("Atenção: Variável de ambiente REACT_APP_API_BASE_URL não foi definida. Usando URL de desenvolvimento como fallback.");
    // Se a variável não for injetada, usamos a de 'development'
    // Isso evita o erro 'http://localhost/api'
    config.production.baseApiUrl = config.development.baseApiUrl;
}


export default config[env];