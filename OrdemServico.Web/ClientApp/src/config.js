// src/config.js
const env = process.env.NODE_ENV || 'development';

const config = {
    development: {
        baseApiUrl: 'http://api:8080/api' 
    },
    production: {
        baseApiUrl: `${window.location.origin}/api`
    }
};

export default config[env];