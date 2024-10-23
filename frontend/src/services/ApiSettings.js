import axios from 'axios';
import apiSettings from '../../apiSettings.json';

const api = axios.create({
    baseURL: apiSettings.baseURL,
});

export default api;
