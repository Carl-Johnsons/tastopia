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

