import {
  loginWithEmailSchema,
  registerWithEmailSchema,
  verifySchema
} from "@/lib/validation/auth";
import { z } from "zod";
import Constants from "expo-constants";
import axios from "axios";
import { API_HOST, axiosInstance } from "@/constants/host";
import { transformPlatformURI } from "@/utils/functions";
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

type SignUpResponse = {} & LoginResponse;

export const register = async (
  inputs: SignUpParams,
  type: IDENTIFIER_TYPE
): Promise<SignUpResponse> => {
  const REGISTER_TYPE = type === IDENTIFIER_TYPE.EMAIL ? "email" : "phone";
  const url = `${API_HOST}/api/account/register/${REGISTER_TYPE}`;

  console.log("url", url);
  console.log("data", JSON.stringify(inputs, null, 2));
  console.log("type", type);

  console.log("Sending request");

  try {
    const { data } = await axios.post(url, inputs, {
      headers: {
        "Content-Type": "application/json"
      }
    });

    console.log("Got response");

    if (data.error) throw new Error(data.error);
    if (data.Message) throw new Error(data.Message);

    console.log("Check data ok, return data");
    return data;
  } catch (error: any) {
    console.log("Error during registration:", error);
    throw new Error(error.response?.data?.Message || "Registration failed");
  }
};

export type VerifyParams = z.infer<typeof verifySchema>;
export type VerifyResponse = VerifyResponseSuccess | VerifyResponseError;
type VerifyResponseSuccess = 0;
type VerifyResponseError = {
  Status: number;
  Code: string;
  Message: string;
};

export const verify = async (
  input: VerifyParams,
  accessToken: string
): Promise<VerifyResponse> => {
  const { OTP } = input;
  const url = `${API_HOST}/api/account/verify/email`;

  try {
    const { status, data } = await axios.post(
      url,
      { OTP },
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${accessToken}`
        }
      }
    );

    if (status !== 200) {
      throw new Error(data.Message || "Verification failed");
    }

    return 0;
  } catch (error: any) {
    console.log("Error during verification:", error);
    throw new Error(error.response?.data?.Message || "Verification failed");
  }
};

type ResendVerifyCodeResponseSuccess = 0;
type ResendVerifyCodeResponseError = {
  Status: number;
  Code: string;
  Message: string;
};

type ResendVerifyCode = ResendVerifyCodeResponseSuccess | ResendVerifyCodeResponseError;

export const resendVerifyCode = async (
  accessToken: string
): Promise<ResendVerifyCode> => {
  const url = `${API_HOST}/api/account/resend/email`;

  try {
    const { status, data } = await axios.post(
      url,
      {},
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${accessToken}`
        }
      }
    );

    if (status !== 200) {
      throw new Error(data.Message || "Resend verification code failed");
    }

    return 0;
  } catch (error: any) {
    console.log("Error during resending verification code:", error);
    throw new Error(error.response?.data?.Message || "Resend verification code failed");
  }
};

export { useLogin };
