import { IDENTIFIER_TYPE } from "@/api/user";
import { getVerifyIdentifierUpdateSchema } from "@/lib/validation/auth";
import { InferType } from "yup";

type LoginResponse = {
  access_token: string;
  refresh_token: string;
};

type SignUpResponse = LoginResponse;

type VerifyResponseSuccess = 0;
type VerifyResponseError = {
  Status: number;
  Code: string;
  Message: string;
};

type VerifyResponse = VerifyResponseSuccess | VerifyResponseError;

type ResendVerifyCodeResponseSuccess = 0;
type ResendVerifyCode = ResendVerifyCodeResponseSuccess | ErrorResponseDTO;

export const verifyIdentifierUpdateSchema = getVerifyIdentifierUpdateSchema(IDENTIFIER_TYPE.EMAIL);
export type VerifyIdentifierUpdateFormFields = InferType<typeof verifyIdentifierUpdateSchema>;
export type VerifyIdentifierUpdateSchema = typeof verifyIdentifierUpdateSchema; 
