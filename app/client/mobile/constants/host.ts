import axios, { AxiosInstance, InternalAxiosRequestConfig } from "axios";
import { store } from "@/store";
import { ROLE, saveAuthData } from "@/slices/auth.slice";
import { stringify } from "@/utils/debug";
import { refreshAccessToken } from "@/api/tokens";
import { transformPlatformURI } from "@/utils/functions";
import { setupTelemetryHeaders, isCi, getTestName } from "@/utils/telemetry";
import { clearSession } from "@/utils/session";

const API_GATEWAY_SCHEME = process.env.EXPO_PUBLIC_API_GATEWAY_SCHEME;
const API_GATEWAY_HOST = process.env.EXPO_PUBLIC_API_GATEWAY_HOST;
const API_GATEWAY_PORT = process.env.EXPO_PUBLIC_API_GATEWAY_PORT;
const IDENTITY_DISCOVERY_URL = transformPlatformURI(
  process.env.EXPO_PUBLIC_IDENTITY_DISCOVERY_URL ?? `http://${API_GATEWAY_HOST}:5001`
);
const API_URI = transformPlatformURI(
  `${API_GATEWAY_SCHEME}://${API_GATEWAY_HOST}:${API_GATEWAY_PORT}`
);

const defaultHeaders = {
  "Content-Type": "application/json"
};

const axiosInstance = axios.create({
  baseURL: API_URI,
  withCredentials: true,
  headers: defaultHeaders,
  timeout: 10000
});

const protectedAxiosInstance = axios.create({
  baseURL: API_URI,
  withCredentials: true,
  headers: defaultHeaders,
  timeout: 50000,
  maxContentLength: Infinity,
  maxBodyLength: Infinity
});

function setUpTelemetryHeader(config: InternalAxiosRequestConfig<any>) {
  const { traceId } = setupTelemetryHeaders(config.headers);

  if (__DEV__ || isCi()) {
    const test = getTestName() ?? "unknown";
    console.log(`[OTEL_TRACE] ${test ?? `test=${test}`} traceId=${traceId}`);
  }
}

async function doRefreshToken(refreshToken: string, axiosInstance: AxiosInstance) {
  const data = await refreshAccessToken(refreshToken, axiosInstance);

  store.dispatch(
    saveAuthData({
      accessToken: data.access_token,
      refreshToken: data.refresh_token
    })
  );

  return data.access_token;
}

axiosInstance.interceptors.request.use(
  config => {
    if (config.method === "get") {
      config.paramsSerializer = {
        indexes: true
      };
    }

    setUpTelemetryHeader(config);
    return config;
  },
  error => {
    return Promise.reject(error);
  }
);

protectedAxiosInstance.interceptors.request.use(
  config => {
    const state = store.getState();
    const accessToken = state.auth.accessToken;

    if (accessToken) {
      config.headers.Authorization = `Bearer ${accessToken}`;
    }

    if (config.method === "get") {
      config.paramsSerializer = {
        indexes: true
      };
    }

    setUpTelemetryHeader(config);
    return config;
  },
  error => {
    return Promise.reject(error);
  }
);

let refreshPromise: Promise<string> | null = null;

protectedAxiosInstance.interceptors.response.use(
  res => res,
  async error => {
    console.debug("Error", stringify(error));

    const originalRequest = error.config;
    const status = error.response?.status ?? error.status;

    if (status === 401) {
      const { refreshToken, role } = store.getState().auth;

      // Guest users or unauthenticated sessions do not have tokens and must not be logged out on 401
      if (role === ROLE.GUEST || role == null) {
        return Promise.reject(error);
      }

      if (originalRequest?._retry || !refreshToken) {
        await clearSession();
        return Promise.reject(error);
      }

      if (originalRequest) {
        originalRequest._retry = true;
      }

      try {
        if (!refreshPromise) {
          refreshPromise = doRefreshToken(refreshToken, axiosInstance).finally(() => {
            refreshPromise = null;
          });
        }

        const newAccessToken = await refreshPromise;

        if (originalRequest) {
          originalRequest.headers = originalRequest.headers ?? {};
          originalRequest.headers.Authorization = `Bearer ${newAccessToken}`;
          return protectedAxiosInstance.request(originalRequest);
        }
      } catch (refreshError) {
        await clearSession();
        return Promise.reject(refreshError);
      }
    }

    return Promise.reject(error);
  }
);

export {
  API_GATEWAY_SCHEME,
  API_GATEWAY_HOST,
  API_URI,
  IDENTITY_DISCOVERY_URL,
  axiosInstance,
  protectedAxiosInstance
};
