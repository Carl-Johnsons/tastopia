import { API_HOST } from "@/constants/host";
import { selectAccessToken } from "@/slices/auth.slice";
import { useAppSelector } from "@/store/hooks";
import axios, { AxiosInstance } from "axios";
import { createContext, useContext, useEffect, useMemo, useState } from "react";

interface AxiosContextType {
  axiosInstance: AxiosInstance;
  protectedAxiosInstance: AxiosInstance;
}
interface Props {
  children: React.ReactNode;
}

const AxiosContext = createContext<AxiosContextType | null>(null);

const AxiosProvider = ({ children }: Props) => {
  const accessToken = useAppSelector(selectAccessToken);

  const [axiosInstance] = useState(() =>
    axios.create({
      baseURL: API_HOST,
      withCredentials: true
    })
  );

  const [protectedAxiosInstance] = useState(() =>
    axios.create({
      baseURL: API_HOST,
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${accessToken}`
      },
      withCredentials: true
    })
  );

  useEffect(() => {
    const requestInterceptor = protectedAxiosInstance.interceptors.request.use(
      async config => {
        try {
          console.log("Request interceptor");
          const token = accessToken;

          if (token) {
            config.headers.Authorization = `Bearer ${token}`;
          }
          return config;
        } catch (err) {
          return Promise.reject(err);
        }
      },
      err => {
        return Promise.reject(err);
      }
    );
    return () => {
      protectedAxiosInstance.interceptors.request.eject(requestInterceptor);
    };
  }, [protectedAxiosInstance.interceptors.request, accessToken]);

  const contextValue = useMemo(() => {
    return { axiosInstance, protectedAxiosInstance };
  }, [axiosInstance, protectedAxiosInstance]);

  return <AxiosContext.Provider value={contextValue}>{children}</AxiosContext.Provider>;
};

const useAxios = () => {
  const context = useContext(AxiosContext);
  if (!context) {
    throw new Error("useAxios must be used within a AxiosProvider");
  }
  return context;
};

export { AxiosProvider, AxiosContext, useAxios };
