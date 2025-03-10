import axios from "axios";
import { useSelectAccessToken } from "@/slices/auth.slice";
import { useMemo } from "react";

const API_GATEWAY_SCHEME = "http";
const API_GATEWAY_HOST = "localhost";
const API_GATEWAY_PORT = "5000";

const API_URI = `${API_GATEWAY_SCHEME}://${API_GATEWAY_HOST}:${API_GATEWAY_PORT}`;
console.log("API gateways uri is " + API_URI);

const defaultHeaders = {
  "Content-Type": "application/json",
};

/**
 * Create axios instances for making API requests.
 */
const useAxios = () => {
  const accessToken = useSelectAccessToken();

  const axiosInstance = useMemo(
    () =>
      axios.create({
        baseURL: API_URI,
        withCredentials: true,
        headers: defaultHeaders,
        timeout: 10000,
      }),
    [API_URI],
  );

  const protectedAxiosInstance = useMemo(
    () =>
      axios.create({
        baseURL: API_URI,
        withCredentials: true,
        headers: defaultHeaders,
        timeout: 10000,
      }),
    [API_URI],
  );

  protectedAxiosInstance.interceptors.request.use(
    (config) => {
      if (accessToken) {
        config.headers.Authorization = `Bearer ${accessToken}`;
      }
      return config;
    },
    (error) => {
      return Promise.reject(error);
    },
  );

  // TODO: Add refresh token logic on access token expiration

  return { axiosInstance, protectedAxiosInstance };
};

export { API_GATEWAY_SCHEME, API_GATEWAY_HOST, API_URI, useAxios };
