// src/services/authService.ts
import axios from 'axios';
import { API_BASE_URL } from '../constants/api';

export interface User {
  usuario: string;
  nombreCompleto: string;
  empresa: string;
  requiereCambioPassword: boolean;
}

export const verificarCedula = async (cedula: string): Promise<{ status: string, message: string }> => {
  try {
    const res = await axios.post(`${API_BASE_URL}/register/generate-verification`, {
      NumeroIdentificacion: cedula,
    });
    return { status: 'OK', message: res.data.message || 'Código generado y enviado' };
  } catch (error: any) {
    if (error.response?.data?.status) {
      return error.response.data;
    }
    if (typeof error.response?.data === 'string') {
      return { status: 'ERROR', message: error.response.data };
    }
    return { status: 'ERROR', message: 'Error desconocido' };
  }
};

export const verificarCodigo = async (
  cedula: string,
  codigo: string
): Promise<{ status: string, message: string }> => {
  try {
    const res = await axios.post(`${API_BASE_URL}/register/verify-code`, {
      NumeroIdentificacion: cedula,
      Codigo: codigo,
    });
    return { status: 'OK', message: res.data.message || 'Registro completado' };
  } catch (error: any) {
    if (error.response?.data?.status) {
      return error.response.data;
    }
    if (typeof error.response?.data === 'string') {
      return { status: 'ERROR', message: error.response.data };
    }
    return { status: 'ERROR', message: 'Error desconocido' };
  }
};

export const login = async (
  usuario: string,
  password: string
): Promise<User | null> => {
  try {
    const response = await axios.post(`${API_BASE_URL}/auth/login`, {
      usuario,
      password,
    });

    // ✅ Si existe nombreCompleto, damos por válido el login
    if (response.status === 200 && response.data?.nombreCompleto) {
      localStorage.setItem('user', JSON.stringify(response.data));
      return response.data; // <- devuelve el usuario
    }

    return null;
  } catch (error) {
    console.error('Error en login:', error);
    return null;
  }
};

export const sendCorreo = (Email: string) => {
  return axios.post(`${API_BASE_URL}/auth/request-reset`, { Email })
}

export const verifyCode = (Code: string) => {
  return axios.post(`${API_BASE_URL}/auth/verify-code`, {Code })
}

export const resetPassword = (Code: string, NewPassword: string) => {
  // console.log(NewPassword);
  return axios.post(`${API_BASE_URL}/auth/reset-password`, { Code, NewPassword })
}