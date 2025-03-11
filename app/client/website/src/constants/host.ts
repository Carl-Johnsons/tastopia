import axios from "axios";
import { API_URI, CLIENT_BASE_URL } from "./api";
import { getAuthCookie } from "@/utils/auth";
import { auth } from "@/auth";

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
    const session = await auth();
    const accessToken = session?.accessToken;
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
