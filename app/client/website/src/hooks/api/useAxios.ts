import "server-only";

import axios from "axios";
import { auth } from "@/auth";

/**
 * Create axios instances for making API requests.
 */
const useAxios = async () => {
  const session = await auth();
  const accessToken = session?.accessToken;

  console.log("abc accessToken", accessToken);

  const API_GATEWAY_SCHEME = "http";
  const API_GATEWAY_HOST = "localhost";
  const API_GATEWAY_PORT = "5000";

  const API_URI = `${API_GATEWAY_SCHEME}://${API_GATEWAY_HOST}:${API_GATEWAY_PORT}`;

  const defaultHeaders = {
    "Content-Type": "application/json",
  };

  const defaultConfig = {
    baseURL: API_URI,
    withCredentials: true,
    headers: defaultHeaders,
    timeout: 10000,
  };

  const axiosInstance = axios.create(defaultConfig);
  const protectedAxiosInstance = axios.create(defaultConfig);

  protectedAxiosInstance.interceptors.request.use(async (config) => {
    if (accessToken) {
      config.headers.Authorization = `Bearer ${accessToken}`;
    }

    return config;
  });

  // TODO: Add refresh token logic on access token expiration

  return { axiosInstance, protectedAxiosInstance };
};

export default useAxios;
