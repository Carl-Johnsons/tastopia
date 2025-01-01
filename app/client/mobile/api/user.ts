import {
  loginWithEmailSchema,
  registerWithEmailSchema,
  verifySchema
} from "@/lib/validation/auth";
import { z } from "zod";
import axios from "axios";
import { API_HOST, axiosInstance } from "@/constants/host";
import { useMutation } from "react-query";

export type LoginParams = z.infer<typeof loginWithEmailSchema>;

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

export enum IDENTIFIER_TYPE {
  EMAIL,
  PHONE_NUMBER
}

const useLogin = () => {
  return useMutation<LoginResponse, Error, LoginParams>({
    mutationKey: ["login"],
    mutationFn: async (inputs: LoginParams) => {
      const body = new URLSearchParams({
        client_id: "react.native",
        scope: "openid profile phone email offline_access IdentityServerApi",
        grant_type: "password",
        username: inputs.identifier,
        password: inputs.password
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
      } catch (error: any) {
        if (error.response?.data?.error_description) {
          throw new Error(error.response.data.error_description);
        }
        if (error.message) {
          throw new Error(error.message);
        }
        throw new Error("An error has occurred.");
      }
    }
  });
};

export type SignUpParams = z.infer<typeof registerWithEmailSchema>;

type SignUpResponse = LoginResponse & {
  // TODO: check these types
  error: string;
  Message: string;
  identifier: string;
};

const useRegister = () => {
  return useMutation<
    SignUpResponse,
    Error,
    { data: SignUpParams; type: IDENTIFIER_TYPE }
  >({
    mutationKey: ["register"],
    mutationFn: async ({ data, type }) => {
      const REGISTER_TYPE = type === IDENTIFIER_TYPE.EMAIL ? "email" : "phone";
      const url = `${API_HOST}/api/account/register/${REGISTER_TYPE}`;

      try {
        const { data: response } = await axiosInstance.post<SignUpResponse>(url, data, {
          headers: {
            "Content-Type": "application/json"
          }
        });

        if (response.error) throw new Error(response.error);
        if (response.Message) throw new Error(response.Message);

        return response;
      } catch (error: any) {
        throw new Error(error.response?.data?.Message || "Registration failed");
      }
    }
  });
};

export type VerifyParams = z.infer<typeof verifySchema>;
export type VerifyResponse = VerifyResponseSuccess | VerifyResponseError;
type VerifyResponseSuccess = 0;
type VerifyResponseError = {
  Status: number;
  Code: string;
  Message: string;
};

const useVerify = () => {
  return useMutation<
    VerifyResponse,
    Error,
    {
      input: VerifyParams;
      accessToken: string;
    }
  >({
    mutationKey: ["verify"],
    mutationFn: async ({ input, accessToken }) => {
      const { OTP } = input;
      const url = `${API_HOST}/api/account/verify/email`;

      try {
        const { status, data } = await axiosInstance.post(
          url,
          { OTP },
          {
            headers: {
              Authorization: `Bearer ${accessToken}`
            }
          }
        );

        if (status !== 200) {
          throw new Error(data.Message || "Verification failed");
        }

        return 0;
      } catch (error: any) {
        throw new Error(error.response?.data?.Message || "Verification failed");
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

const useResendVerifyCode = () => {
  return useMutation<ResendVerifyCode, Error, string>({
    mutationKey: ["resendVerifyCode"],
    mutationFn: async accessToken => {
      const url = `${API_HOST}/api/account/resend/email`;

      try {
        const { status, data } = await axiosInstance.post(
          url,
          {},
          {
            headers: {
              Authorization: `Bearer ${accessToken}`
            }
          }
        );

        if (status !== 200) {
          throw new Error(data.Message || "Resend verification code failed");
        }

        return 0;
      } catch (error: any) {
        throw new Error(
          error.response?.data?.Message || "Resend verification code failed"
        );
      }
    }
  });
};

export { useLogin, useRegister, useVerify, useResendVerifyCode };
