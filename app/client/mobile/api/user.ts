import {
  loginWithEmailSchema,
  registerWithEmailSchema,
  verifySchema
} from "@/lib/validation/auth";
import { z } from "zod";
import Constants from "expo-constants";

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

const host: string =
  process.env.API_HOST || Constants.expoConfig?.hostUri?.split(":")[0] || "10.0.2.2";

export enum IDENTIFIER_TYPE {
  EMAIL,
  PHONE_NUMBER
}

export const login = async (inputs: LoginParams): Promise<LoginResponse> => {
  const body = new URLSearchParams({
    client_id: "react.native",
    scope: "openid profile phone email offline_access IdentityServerApi",
    grant_type: "password",
    username: inputs.identifier,
    password: inputs.password
  }).toString();

  const res = await fetch(`http://${host}:5001/connect/token`, {
    method: "POST",
    headers: {
      "Content-Type": "application/x-www-form-urlencoded"
    },
    body
  });

  const data = await res.json();
  if (data.error) throw new Error(data.error);

  return data;
};

export type SignUpParams = z.infer<typeof registerWithEmailSchema>;

type SignUpResponse = {} & LoginResponse;

export const register = async (
  inputs: SignUpParams,
  type: IDENTIFIER_TYPE
): Promise<SignUpResponse> => {
  const REGISTER_TYPE = type == IDENTIFIER_TYPE.EMAIL ? "email" : "phone";

  const res = await fetch(`http://${host}:5001/api/account/register/${REGISTER_TYPE}`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(inputs)
  });

  const data = await res.json();
  if (data.error) throw new Error(data.error);
  if (!res.ok && data.Message) throw new Error(data.Message);

  return data;
};

export type VerifyParams = z.infer<typeof verifySchema>;
export type VerifyResponse = VerifyResponseSuccess | VerifyResponseError;
type VerifyResponseSuccess = 0;
type VerifyResponseError = {
  Status: number;
  Code: string;
  Message: string;
};

export const verify = async (inputs: VerifyParams): Promise<VerifyResponse> => {
  const parsedInputs = verifySchema.parse(inputs);
  const { OTP, accessToken } = parsedInputs;

  const res = await fetch(`http://${host}:5001/api/account/verify/email`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${accessToken}`
    },
    body: JSON.stringify({ OTP })
  });

  if (!res.ok) {
    const data = (await res.json()) as VerifyResponseError;
    throw new Error(data.Message);
  }

  return 0;
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
  const res = await fetch(`http://${host}:5001/api/account/resend/email`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${accessToken}`
    }
  });

  if (!res.ok) {
    const data = (await res.json()) as VerifyResponseError;
    throw new Error(data.Message);
  }

  return 0;
};
