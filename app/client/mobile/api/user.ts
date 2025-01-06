import { loginSchema, registerSchema, verifySchema } from "@/lib/validation/auth";
import { API_HOST, axiosInstance, protectedAxiosInstance } from "@/constants/host";
import { useMutation } from "react-query";
import { InferType } from "yup";
import { AxiosError } from "axios";
import { CLIENT_ID, SCOPE } from "@/constants/api";

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
          {
            headers: {
              "Content-Type": "application/x-www-form-urlencoded"
            }
          }
        );
        return data;
      } catch (error) {
        if (error instanceof AxiosError) {
          throw new Error(error.message);
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
      const REGISTER_TYPE = type === IDENTIFIER_TYPE.EMAIL ? "email" : "phone";
      const url = `${API_HOST}/api/account/register/${REGISTER_TYPE}`;

      try {
        const { data: response } = await axiosInstance.post<SignUpResponse>(url, data, {
          headers: {
            "Content-Type": "application/json"
          }
        });

        return response;
      } catch (error) {
        if (error instanceof AxiosError) {
          throw new Error(error.message);
        }

        throw new Error("An error has occurred.");
      }
    }
  });
};

export type VerifyParams = InferType<typeof verifySchema>;
export type VerifyResponse = VerifyResponseSuccess | VerifyResponseError;
type VerifyResponseSuccess = 0;
type VerifyResponseError = {
  Status: number;
  Code: string;
  Message: string;
};

export const useVerify = () => {
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

export const useResendVerifyCode = () => {
  return useMutation<ResendVerifyCode, Error, unknown>({
    mutationKey: ["resendVerifyCode"],
    mutationFn: async () => {
      const url = `${API_HOST}/api/account/resend/email`;

      try {
        const { status, data } = await protectedAxiosInstance.post(url);

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
    console.log("Error", JSON.stringify(error, null, 2));
    
    if (error instanceof AxiosError) {
      throw new Error(error.message);
    }

    throw new Error("An error has occurred.");
  }
};
