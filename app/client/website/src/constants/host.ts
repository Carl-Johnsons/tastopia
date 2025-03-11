import axios from "axios";
import { API_URI, CLIENT_BASE_URL } from "./api";
import { getCookie } from "cookies-next/client";

console.log("API gateways uri is " + API_URI);

const defaultHeaders = {
  "Content-Type": "application/json",
};

export const clientAxiosInstance = axios.create({
  baseURL: CLIENT_BASE_URL,
  withCredentials: true,
  headers: defaultHeaders,
  timeout: 10000,
});

export const axiosInstance = axios.create({
  baseURL: API_URI,
  withCredentials: true,
  headers: defaultHeaders,
  timeout: 10000,
});

export const protectedAxiosInstance = axios.create({
  baseURL: API_URI,
  withCredentials: true,
  headers: defaultHeaders,
  timeout: 10000,
});

protectedAxiosInstance.interceptors.request.use(
  async (config) => {
    const accessToken = await getCookie("accessToken");
    console.log("Access token in interceptors:", accessToken);

    if (accessToken) {
      config.headers.Authorization = `Bearer ${accessToken}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  },
);
