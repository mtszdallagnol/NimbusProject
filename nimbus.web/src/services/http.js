import axios from 'axios';

const axiosInstance = axios.create({
    baseURL: 'http://localhost:5290/api/',
    headers: {
        'Contnet-type': 'application/json'
    }
});

export default axiosInstance