"use server";

import { protectedAxiosInstance } from "@/constants/host";
import { SETTING_KEY, SETTING_VALUE } from "@/constants/settings";
import { AxiosError } from "axios";
import { UserState } from "@/slices/user.slice";
import { IErrorResponseDTO } from "../../generated/interfaces/common.interface";
import { stringify } from "@/utils/debug";
import { IPaginatedAdminGetUserListResponse } from "@/generated/interfaces/user.interface";

export async function getUserById(id: string) {
  try {
    const url = "/api/user/admin-get-user-detail";
    const { data } = await protectedAxiosInstance.post(url, {
      accountId: id
    });

    return data;
  } catch (error) {
    console.log(error);
    throw error;
  }
}

export async function getUserActivitiesById(id: string, language: string) {
  try {
    const url = "/api/recipe/admin-get-user-activities";
    const { data } = await protectedAxiosInstance.post(url, {
      accountId: id,
      language,
      skip: 0
    });

    return data;
  } catch (error) {
    console.log(error);
    throw error;
  }
}

export type UpdateSettingResponseSuccess = 0;
export type UpdateSettingResponse = UpdateSettingResponseSuccess | IErrorResponseDTO;
export type UpdateSettingParams = {
  settings: Array<{
    key: SETTING_KEY;
    value: SETTING_VALUE.BOOLEAN | SETTING_VALUE.LANGUAGE;
  }>;
};

export const useUpdateSettings = async (data: UpdateSettingParams) => {
  const url = "/api/setting";

  try {
    const { data: response } = await protectedAxiosInstance.put<UpdateSettingResponse>(
      url,
      data
    );
    return response;
  } catch (error) {
    console.debug("useUpdateSettings", stringify(error));

    if (error instanceof AxiosError) {
      const data = error.response?.data as IErrorResponseDTO;
      throw new Error(data.message);
    }

    throw new Error("An error has occurred.");
  }
};

export type GetUserDetailsResponse = UserState;

export const getUserDetails = async () => {
  const url = "/api/user/get-current-user-details";

  try {
    const { data } = await protectedAxiosInstance.get(url);
    return data as GetUserDetailsResponse;
  } catch (error) {
    if (error instanceof AxiosError) {
      const data = error.response?.data as IErrorResponseDTO;
      throw new Error(data.message ? data.message : error.message);
    }

    throw new Error("An error has occurred.");
  }
};

export async function getAdminUsers(
  skip = 0,
  sortBy = "",
  sortOrder = "asc",
  keyword = "",
  limit = 6
) {
  try {
    const url = `/api/user/admin-get-users?Skip=${skip}&SortBy=${sortBy}&SortOrder=${sortOrder}&limit=${limit}&keyword=${encodeURIComponent(keyword)}`;

    const { data } = await protectedAxiosInstance.get(url);

    return data as IPaginatedAdminGetUserListResponse;
  } catch (error) {
    console.log(error);
    throw error;
  }
}

export async function adminBanUser(accountId: string) {
  try {
    const url = "/api/user/admin-ban-user";
    const { data } = await protectedAxiosInstance.post(url, {
      accountId
    });

    return data;
  } catch (error) {
    console.log(error);
    throw error;
  }
}
