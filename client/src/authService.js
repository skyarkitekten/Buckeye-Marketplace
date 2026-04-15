import axios from 'axios';

const API_URL = 'http://localhost:5000/api/auth/'; // Double check your port!

export const register = (email, password) => {
    return axios.post(API_URL + 'register', { email, password });
};

export const login = async (email, password) => {
    const response = await axios.post(API_URL + 'login', { email, password });
    if (response.data.token) {
        // This is the "Security Requirement" for keeping the user logged in
        localStorage.setItem('userToken', response.data.token);
    }
    return response.data;
};

export const logout = () => {
    localStorage.removeItem('userToken');
};