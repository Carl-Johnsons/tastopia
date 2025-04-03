import { loginSchema, registerSchema } from "@/lib/validation/auth";
import { axiosInstance, protectedAxiosInstance } from "@/constants/host";
import { useInfiniteQuery, useMutation, useQuery } from "react-query";
import { InferType } from "yup";
import { CLIENT_ID, SCOPE } from "@/constants/api";
import { AxiosError } from "axios";
import { stringify } from "@/utils/debug";
import { selectAccessToken } from "@/slices/auth.slice";
import { UserState } from "@/slices/user.slice";
import { SETTING_KEY, SETTING_VALUE } from "@/constants/settings";
import {
  IAccountIdentifierDTO,
  IChangePasswordDTO,
  ICheckForgotPasswordDTO,
  IRegisterAccountDTO
} from "@/generated/interfaces/identity.interface";
import {
  IGetUserDetailsResponse,
  ISimpleUserResponse,
  IUpdateUserDTO,
  IUserReportUserDTO,
  IUserReportUserResponse
} from "@/generated/interfaces/user.interface";
import {
  IAdvancePaginatedMetadata,
  IErrorResponseDTO
} from "@/generated/interfaces/common.interface";
import { ForgotPasswordFormFields } from "@/components/screen/forgot/ForgotPasswordForm";
import {
  LoginResponse,
  ResendVerifyCode,
  SignUpResponse,
  VerifyResponse
} from "@/types/api/auth";

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

      const { data } = await axiosInstance.post<LoginResponse>("/connect/token", body, {
        headers: { "Content-Type": "application/x-www-form-urlencoded" }
      });

      return data;
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
        console.debug("useGetUserDetails", stringify(error));

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
    { data: IRegisterAccountDTO; type: IDENTIFIER_TYPE }
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

export type RequestUpdateIdentifierParams = {
  type: IDENTIFIER_TYPE;
  data: IAccountIdentifierDTO;
};

export type VerifyUpdateIdentifierParams = {
  type: IDENTIFIER_TYPE;
  data: IAccountIdentifierDTO & { OTP: string };
};

export type AddIdentifierParams = Pick<VerifyUpdateIdentifierParams, "type">;
export type ModifyIdentifierParams = Pick<VerifyUpdateIdentifierParams, "type">;

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

export type UnlinkIdentifierParams = {
  type: IDENTIFIER_TYPE;
};

export const useUnlink = () => {
  return useMutation<void, Error, UnlinkIdentifierParams>({
    mutationKey: ["unlink"],
    mutationFn: async ({ type }) => {
      const ENDPOINT_TYPE = type === IDENTIFIER_TYPE.EMAIL ? "email" : "phone";
      const url = `/api/account/unlink/${ENDPOINT_TYPE}`;
      await protectedAxiosInstance.post(url);
    }
  });
};

export const useRequestUpdateIdentifier = () => {
  return useMutation<void, AxiosError, RequestUpdateIdentifierParams>({
    mutationKey: ["requestUpdateIdentifier"],
    mutationFn: async ({ type, data }) => {
      const ENDPOINT_TYPE = type === IDENTIFIER_TYPE.EMAIL ? "email" : "phone";
      const url = `/api/account/request-update-identifier/${ENDPOINT_TYPE}`;
      await protectedAxiosInstance.post(url, data);
    }
  });
};

export const useVerifyUpdateIdentifierOTP = () => {
  return useMutation<void, AxiosError, VerifyUpdateIdentifierParams>({
    mutationKey: ["verifyUpdateIdentifierOTP"],
    mutationFn: async ({ type, data }) => {
      const ENDPOINT_TYPE = type === IDENTIFIER_TYPE.EMAIL ? "email" : "phone";
      const url = `/api/account/check-update-identifier/${ENDPOINT_TYPE}`;
      await protectedAxiosInstance.post<undefined>(url, data);
    }
  });
};

export const useUpdateIdentifier = () => {
  return useMutation<void, AxiosError, VerifyUpdateIdentifierParams>({
    mutationKey: ["verifyUpdateIdentifier"],
    mutationFn: async ({ type, data }) => {
      const ENDPOINT_TYPE = type === IDENTIFIER_TYPE.EMAIL ? "email" : "phone";
      const url = `/api/account/verify-update-identifier/${ENDPOINT_TYPE}`;
      await protectedAxiosInstance.post(url, data);
    }
  });
};

export const useFindAccount = () => {
  return useMutation<
    ISimpleUserResponse,
    AxiosError,
    { data: ForgotPasswordFormFields; type: IDENTIFIER_TYPE }
  >({
    mutationFn: async ({ data, type }) => {
      const ENDPOINT_TYPE = type === IDENTIFIER_TYPE.EMAIL ? "email" : "phone";
      const url = `/api/account/find-account/${ENDPOINT_TYPE}`;

      const { data: response } = await axiosInstance.post<ISimpleUserResponse>(url, data);
      return response;
    }
  });
};

export const useRequestChangePassword = () => {
  return useMutation<
    undefined,
    AxiosError,
    { data: IAccountIdentifierDTO; type: IDENTIFIER_TYPE }
  >({
    mutationFn: async ({ data, type }) => {
      const ENDPOINT_TYPE = type === IDENTIFIER_TYPE.EMAIL ? "email" : "phone";
      const url = `/api/account/change-password/${ENDPOINT_TYPE}/request`;

      await axiosInstance.post(url, data);
    }
  });
};

export const useCheckForgotPasswordOTP = () => {
  return useMutation<
    undefined,
    AxiosError,
    { data: ICheckForgotPasswordDTO; type: IDENTIFIER_TYPE }
  >({
    mutationFn: async ({ data, type }) => {
      const ENDPOINT_TYPE = type === IDENTIFIER_TYPE.EMAIL ? "email" : "phone";
      const url = `/api/account/forgot-password/${ENDPOINT_TYPE}/check`;

      await axiosInstance.post(url, data);
    }
  });
};

export const useChangePassword = () => {
  return useMutation<
    undefined,
    AxiosError,
    { data: IChangePasswordDTO; type: IDENTIFIER_TYPE }
  >({
    mutationFn: async ({ data, type }) => {
      const ENDPOINT_TYPE = type === IDENTIFIER_TYPE.EMAIL ? "email" : "phone";
      const url = `/api/account/change-password/${ENDPOINT_TYPE}`;

      await axiosInstance.post(url, data);
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

export const useReportUser = () => {
  return useMutation<IUserReportUserResponse, Error, IUserReportUserDTO>({
    mutationFn: async ({ accountId, reasonCodes, additionalDetails }) => {
      const { data } = await protectedAxiosInstance.post("/api/user/user-report-user", {
        accountId,
        reasonCodes,
        additionalDetails
      });
      return data;
    }
  });
};

type IGetUserFollowersResponse = {
  paginatedData: SearchUserResultType[];
  metadata: IAdvancePaginatedMetadata;
};

export const useGetUserFollowers = (keyword: string) => {
  const accessToken = selectAccessToken();

  return useInfiniteQuery<IGetUserFollowersResponse, Error>({
    queryKey: ["getUserFollowers", keyword],
    enabled: !!accessToken,
    queryFn: async ({ pageParam = 0 }) => {
      try {
        const { data } = await protectedAxiosInstance.post<IGetUserFollowersResponse>(
          "/api/user/get-user-follower",
          {
            skip: pageParam,
            keyword
          }
        );
        return data;
      } catch (error) {
        if (error instanceof AxiosError) {
          const data = error.response?.data as IErrorResponseDTO;
          throw new Error(data.message || "An error has occurred.");
        }

        throw new Error("An error has occurred.");
      }
    },
    getNextPageParam: (lastPage, allPages) => {
      if (!lastPage.metadata?.hasNextPage) return undefined;
      return allPages.length;
    }
  });
};

export const useGetUserFollowings = (keyword: string = "") => {
  const accessToken = selectAccessToken();

  return useInfiniteQuery<IGetUserFollowersResponse, Error>({
    queryKey: ["getUserFollowings", keyword],
    enabled: !!accessToken,
    queryFn: async ({ pageParam = 0 }) => {
      try {
        const { data } = await protectedAxiosInstance.post<IGetUserFollowersResponse>(
          "/api/user/get-user-following",
          {
            skip: pageParam,
            keyword
          }
        );
        return data;
      } catch (error) {
        if (error instanceof AxiosError) {
          const data = error.response?.data as IErrorResponseDTO;
          throw new Error(data.message || "An error has occurred.");
        }

        throw new Error("An error has occurred.");
      }
    },
    getNextPageParam: (lastPage, allPages) => {
      if (!lastPage.metadata?.hasNextPage) return undefined;
      return allPages.length;
    }
  });
};
