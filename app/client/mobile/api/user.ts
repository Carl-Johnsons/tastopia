import { loginSchema, registerSchema } from "@/lib/validation/auth";
import { axiosInstance, protectedAxiosInstance } from "@/constants/host";
import { useMutation, useQuery } from "react-query";
import { InferType } from "yup";
import { CLIENT_ID, SCOPE } from "@/constants/api";
import { AxiosError } from "axios";
import { stringify } from "@/utils/debug";
import { selectAccessToken } from "@/slices/auth.slice";
import { UserState } from "@/slices/user.slice";
import { SETTING_KEY, SETTING_VALUE } from "@/constants/settings";

export type LoginParams = InferType<typeof loginSchema>;
export enum IDENTIFIER_TYPE {
  EMAIL,
  PHONE_NUMBER
}

export const useLogin = () => {
  return useMutation<LoginResponse, Error, LoginParams>({
    mutationKey: ["login"],
    mutationFn: async inputs => {
      const body = new URLSearchParams({
        client_id: CLIENT_ID,
        scope: SCOPE,
        grant_type: "password",
        username: inputs.identifier,
        password: inputs.password
      }).toString();

      try {
        const { data } = await axiosInstance.post<LoginResponse>("/connect/token", body, {
          headers: { "Content-Type": "application/x-www-form-urlencoded" }
        });

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

export const useGetUserSettings = () => {
  const accessToken = selectAccessToken();

  return useQuery<GetUserSettingsResponse, Error>({
    queryKey: "getUserSettings",
    enabled: !!accessToken,
    queryFn: async () => {
      const url = "/api/setting";

      try {
        const { data } = await protectedAxiosInstance.get(url);
        return data;
      } catch (error) {
        if (error instanceof AxiosError) {
          const data = error.response?.data as IErrorResponseDTO;
          throw new Error(data.message);
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
      const url = "/api/user/get-current-user-details";

      try {
        const { data } = await protectedAxiosInstance.get(url);
        return data;
      } catch (error) {
        console.debug("useGetUserDetails", JSON.stringify(error));

        if (error instanceof AxiosError) {
          const data = error.response?.data as IErrorResponseDTO;
          throw new Error(data.message);
        }

        throw new Error("An error has occurred.");
      }
    }
  });
};

export type SignUpParams = InferType<typeof registerSchema>;

export const useRegister = () => {
  return useMutation<
    SignUpResponse,
    Error,
    { data: SignUpParams; type: IDENTIFIER_TYPE }
  >({
    mutationKey: ["register"],
    mutationFn: async ({ data, type }) => {
      const ENDPOINT_TYPE = type === IDENTIFIER_TYPE.EMAIL ? "email" : "phone";
      const url = `/api/account/register/${ENDPOINT_TYPE}`;

      try {
        const { data: response } = await axiosInstance.post<SignUpResponse>(url, data);
        return response;
      } catch (error) {
        console.debug("useRegister", stringify(error));

        if (error instanceof AxiosError) {
          const data = error.response?.data as IErrorResponseDTO;
          throw new Error(data.message);
        }

        throw new Error("An error has occurred.");
      }
    }
  });
};

export type VerifyParams = { OTP: string; type: IDENTIFIER_TYPE };

export const useVerify = () => {
  return useMutation<VerifyResponse, Error, VerifyParams>({
    mutationKey: ["verify"],
    mutationFn: async ({ OTP, type }) => {
      const ENDPOINT_TYPE = type === IDENTIFIER_TYPE.EMAIL ? "email" : "phone";
      const url = `/api/account/verify/${ENDPOINT_TYPE}`;

      try {
        const { data } = await protectedAxiosInstance.post(url, { OTP });
        return data;
      } catch (error) {
        console.debug("useVerify", stringify(error));

        if (error instanceof AxiosError) {
          const data = error.response?.data as IErrorResponseDTO;
          throw new Error(data.message);
        }

        throw new Error("An error has occurred.");
      }
    }
  });
};

export const useResendVerifyCode = () => {
  return useMutation<ResendVerifyCode, Error, { type: IDENTIFIER_TYPE }>({
    mutationKey: ["resendVerifyCode"],
    mutationFn: async ({ type }) => {
      const ENDPOINT_TYPE = type === IDENTIFIER_TYPE.EMAIL ? "email" : "phone";
      const url = `/api/account/resend/${ENDPOINT_TYPE}`;

      try {
        const { data } = await protectedAxiosInstance.post(url);
        return data;
      } catch (error) {
        console.debug("resendVerifyCode", stringify(error));

        if (error instanceof AxiosError) {
          const data = error.response?.data as IErrorResponseDTO;
          throw new Error(data.message);
        }

        throw new Error("An error has occurred.");
      }
    }
  });
};

export type UpdateSettingResponseSuccess = 0;
export type UpdateSettingResponse = UpdateSettingResponseSuccess | IErrorResponseDTO;
export type UpdateSettingParams = {
  settings: Array<{
    key: SETTING_KEY;
    value: SETTING_VALUE.BOOLEAN | SETTING_VALUE.LANGUAGE;
  }>;
};

type UserSetting = {
  accountId: string;
  settingId: string;
  settingValue: SETTING_VALUE.LANGUAGE | SETTING_VALUE.BOOLEAN;
  user: null;
  setting: Setting;
};

type Setting = {
  code: SETTING_KEY;
  description: string;
  dataType: number;
  defaultValue: SETTING_VALUE.LANGUAGE | SETTING_VALUE.BOOLEAN;
  id: string;
};

type GetUserSettingsResponse = Array<UserSetting>;

export const useUpdateSetting = () => {
  return useMutation<UpdateSettingResponse, Error, UpdateSettingParams>({
    mutationKey: ["updateSetting"],
    mutationFn: async data => {
      const url = "/api/setting";

      try {
        const { data: response } =
          await protectedAxiosInstance.put<UpdateSettingResponse>(url, data);
        return response;
      } catch (error) {
        console.debug("useUpdateSetting", stringify(error));

        if (error instanceof AxiosError) {
          const data = error.response?.data as IErrorResponseDTO;
          throw new Error(data.message);
        }

        throw new Error("An error has occurred.");
      }
    }
  });
};

export type UpdateUserResponseSuccess = undefined;
export type UpdateUserResponse = UpdateUserResponseSuccess | IErrorResponseDTO;

export const useUpdateUser = () => {
  return useMutation<UpdateUserResponse, Error, IUpdateUserDTO>({
    mutationKey: ["updateUser"],
    mutationFn: async data => {
      const url = "/api/user";
      const body = new FormData();

      Object.entries(data).forEach(([key, value]) => {
        if (value) {
          body.append(key, value);
        }
      });

      try {
        const { data: response } = await protectedAxiosInstance.patch<UpdateUserResponse>(
          url,
          body,
          {
            headers: { "Content-Type": "multipart/form-data" }
          }
        );

        return response;
      } catch (error) {
        console.debug("useUpdateUser", stringify(error));

        if (error instanceof AxiosError) {
          const data = error.response?.data as IErrorResponseDTO;
          throw new Error(data.message);
        }

        throw new Error("An error has occurred.");
      }
    }
  });
};

type FollowUnfollowUserResponse = {
  followerId: string;
  followingId: string;
  isFollowing: boolean;
};

export const useFollowUnfollowUser = () => {
  return useMutation<FollowUnfollowUserResponse, Error, { accountId: string }>({
    mutationFn: async ({ accountId }) => {
      const { data } = await protectedAxiosInstance.post("/api/user/follow-user", {
        accountId: accountId
      });
      return data;
    }
  });
};

export const useGetUserByAccountId = (accountId: string) => {
  return useQuery<IGetUserDetailsResponse>({
    queryKey: ["user", accountId],
    queryFn: async () => {
      const { data } = await protectedAxiosInstance.post<IGetUserDetailsResponse>(
        "/api/user/get-user-detail-by-account-id",
        {
          accountId: accountId
        }
      );
      return data;
    },
    enabled: !!accountId
  });
};

export const useReportUserReason = (language: string) => {
  return useQuery<ReportRecipeCommentReasonResponse>({
    queryKey: ["reportUserReason", language],
    queryFn: async () => {
      const { data } =
        await protectedAxiosInstance.post<ReportRecipeCommentReasonResponse>(
          "/api/user/get-report-reasons",
          {
            language
          }
        );
      return data;
    },
    enabled: !!language
  });
};
