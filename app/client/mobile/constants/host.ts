import axios from "axios";
import { Platform } from "react-native";
import { store } from "@/store";

const IOS_API_HOST = "http://localhost:5000";
const ANDROID_API_HOST = "http://10.0.2.2:5000";

const API_HOST = Platform.select({
  ios: IOS_API_HOST,
  android: ANDROID_API_HOST,
  default: IOS_API_HOST
});

const defaultHeaders = {
  "Content-Type": "application/json"
};

const axiosInstance = axios.create({
  baseURL: API_HOST,
  withCredentials: true,
  headers: defaultHeaders,
  timeout: 10000
});

const protectedAxiosInstance = axios.create({
  baseURL: API_HOST,
  withCredentials: true,
  headers: defaultHeaders,
  timeout: 10000
});

protectedAxiosInstance.interceptors.request.use(
  config => {
    const state = store.getState();
    const accessToken = state.auth.accessToken;

    if (accessToken) {
      config.headers.Authorization = `Bearer ${accessToken}`;
    }
    return config;
  },
  error => {
    return Promise.reject(error);
  }
);

export { API_HOST, axiosInstance, protectedAxiosInstance };
