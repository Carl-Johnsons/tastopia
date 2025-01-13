import axios from "axios";
import { store } from "@/store";
import { saveAuthData } from "@/slices/auth.slice";
import { stringify } from "@/utils/debug";
import Constants from "expo-constants";
import { refreshAccessToken } from "@/api/tokens";

const {expoConfig} = Constants;
const HOST = expoConfig?.hostUri?.split(":")[0];
const API_HOST = `http://${HOST}:5000`;

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

protectedAxiosInstance.interceptors.response.use(
  res => res,
  async error => {
    console.debug("Error", stringify(error));

    if (error.status === 401) {
      const { refreshToken } = store.getState().auth;
      console.log("Atempt to refresh access token", refreshToken);

      if (refreshToken) {
        const { access_token, refresh_token } = await refreshAccessToken(refreshToken);

        store.dispatch(
          saveAuthData({
            accessToken: access_token,
            refreshToken: refresh_token
          })
        );

        console.log("Refresh token successfully.", access_token);
        const config = error.config;

        protectedAxiosInstance.request(config);
      }
    }

    return Promise.reject(error);
  }
);

export { API_HOST, axiosInstance, protectedAxiosInstance };
