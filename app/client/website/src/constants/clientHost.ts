import axios from "axios";
import { CLIENT_BASE_URL } from "./api";

const defaultHeaders = {
  "Content-Type": "application/json",
};

export const clientAxiosInstance = axios.create({
  baseURL: CLIENT_BASE_URL,
  withCredentials: true,
  headers: defaultHeaders,
  timeout: 10000,
});
