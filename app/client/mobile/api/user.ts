import { loginSchema, registerSchema } from "@/lib/validation/auth";
import { API_HOST, axiosInstance, protectedAxiosInstance } from "@/constants/host";
import { useMutation, useQuery } from "react-query";
import { InferType } from "yup";
import { CLIENT_ID, SCOPE } from "@/constants/api";
import { AxiosError } from "axios";
import { stringify } from "@/utils/debug";
import { selectAccessToken } from "@/slices/auth.slice";
import { UserState } from "@/slices/user.slice";

export type LoginParams = InferType<typeof loginSchema>;

export type User = {
  _id: string;
  name: string;
  email: string;
  username: string;
  bio: string;
  profilePic: string;
  followers: string[];
  following: string[];
};

type LoginResponse = {
  access_token: string;
  refresh_token: string;
};

type ErrorResponseDTO = {
  status: number;
  code: string;
  message: string;
};

export enum IDENTIFIER_TYPE {
  EMAIL,
  PHONE_NUMBER
}

export const useLogin = () => {
  return useMutation<LoginResponse, Error, LoginParams>({
    mutationKey: ["login"],
    mutationFn: async (inputs: LoginParams) => {
      const body = new URLSearchParams({
        client_id: CLIENT_ID,
        scope: SCOPE,
        grant_type: "password",
        username: inputs.identifier,
        password: inputs.password
      }).toString();

      try {
        const { data } = await axiosInstance.post<LoginResponse>(
          `${API_HOST}/connect/token`,
          body,
          { headers: { "Content-Type": "application/x-www-form-urlencoded" } }
        );

        return data;
      } catch (error) {
        console.debug("useLogin", stringify(error));

        if (error instanceof AxiosError && error.status === 400) {
          throw new Error("Wrong email, phone number or password.");
        }

        throw new Error("An error has occurred.");
      }
    }
  });
};

export type GetUserDetailsResponse = UserState;

export const useGetUserDetails = () => {
  const accessToken = selectAccessToken();

  return useQuery<GetUserDetailsResponse, Error>({
    queryKey: "getUserDetails",
    enabled: !!accessToken,
    queryFn: async () => {
      const url = `${API_HOST}/api/user/get-current-user-details`;
      console.debug("useGetUserDetails: Fetching data with access token", accessToken);

      try {
        const { data } = await protectedAxiosInstance.get(url);
        return data;
      } catch (error) {
        console.debug("useGetUserDetails", JSON.stringify(error));

        if (error instanceof AxiosError) {
          const data = error.response?.data as ErrorResponseDTO;
          throw new Error(data.message);
        }

        throw new Error("An error has occurred.");
      }
    }
  });
};

export type SignUpParams = InferType<typeof registerSchema>;

type SignUpResponse = LoginResponse;

export const useRegister = () => {
  return useMutation<
    SignUpResponse,
    Error,
    { data: SignUpParams; type: IDENTIFIER_TYPE }
  >({
    mutationKey: ["register"],
    mutationFn: async ({ data, type }) => {
      const ENDPOINT_TYPE = type === IDENTIFIER_TYPE.EMAIL ? "email" : "phone";
      const url = `${API_HOST}/api/account/register/${ENDPOINT_TYPE}`;

      try {
        const { data: response } = await axiosInstance.post<SignUpResponse>(url, data);
        return response;
      } catch (error) {
        console.debug("useRegister", stringify(error));

        if (error instanceof AxiosError) {
          const data = error.response?.data as ErrorResponseDTO;
          throw new Error(data.message);
        }

        throw new Error("An error has occurred.");
      }
    }
  });
};

export type VerifyParams = { OTP: string; type: IDENTIFIER_TYPE };
export type VerifyResponse = VerifyResponseSuccess | VerifyResponseError;
type VerifyResponseSuccess = 0;
type VerifyResponseError = {
  Status: number;
  Code: string;
  Message: string;
};

export const useVerify = () => {
  return useMutation<VerifyResponse, Error, VerifyParams>({
    mutationKey: ["verify"],
    mutationFn: async ({ OTP, type }) => {
      const ENDPOINT_TYPE = type === IDENTIFIER_TYPE.EMAIL ? "email" : "phone";
      const url = `${API_HOST}/api/account/verify/${ENDPOINT_TYPE}`;

      try {
        const { data } = await protectedAxiosInstance.post(url, { OTP });
        return data;
      } catch (error) {
        console.debug("useVerify", stringify(error));

        if (error instanceof AxiosError) {
          const data = error.response?.data as ErrorResponseDTO;
          throw new Error(data.message);
        }

        throw new Error("An error has occurred.");
      }
    }
  });
};

type ResendVerifyCodeResponseSuccess = 0;
type ResendVerifyCodeResponseError = {
  Status: number;
  Code: string;
  Message: string;
};

type ResendVerifyCode = ResendVerifyCodeResponseSuccess | ResendVerifyCodeResponseError;

export const useResendVerifyCode = () => {
  return useMutation<ResendVerifyCode, Error, { type: IDENTIFIER_TYPE }>({
    mutationKey: ["resendVerifyCode"],
    mutationFn: async ({ type }) => {
      const ENDPOINT_TYPE = type === IDENTIFIER_TYPE.EMAIL ? "email" : "phone";
      const url = `${API_HOST}/api/account/resend/${ENDPOINT_TYPE}`;

      try {
        const { data } = await protectedAxiosInstance.post(url);
        return data;
      } catch (error) {
        console.debug("resendVerifyCode", stringify(error));

        if (error instanceof AxiosError) {
          const data = error.response?.data as ErrorResponseDTO;
          throw new Error(data.message);
        }

        throw new Error("An error has occurred.");
      }
    }
  });
};

export const refreshAccessToken = async (refreshToken: string) => {
  const body = new URLSearchParams({
    client_id: CLIENT_ID,
    scope: SCOPE,
    grant_type: "refresh_token",
    refresh_token: refreshToken
  }).toString();

  try {
    const { data } = await axiosInstance.post<LoginResponse>(
      `${API_HOST}/connect/token`,
      body,
      {
        headers: {
          "Content-Type": "application/x-www-form-urlencoded"
        }
      }
    );

    return data;
  } catch (error) {
    console.debug("refreshAccessToken", stringify(error));

    if (error instanceof AxiosError) {
      const data = error.response?.data as ErrorResponseDTO;
      throw new Error(data.message);
    }

    throw new Error("An error has occurred.");
  }
};
