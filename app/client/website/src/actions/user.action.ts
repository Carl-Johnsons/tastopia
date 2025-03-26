"use server";

import { protectedAxiosInstance } from "@/constants/host";
import { SETTING_KEY, SETTING_VALUE } from "@/constants/settings";
import { AxiosError } from "axios";
import { UserState } from "@/slices/user.slice";
import { IErrorResponseDTO } from "@/generated/interfaces/common.interface";
import { stringify } from "@/utils/debug";
import {
  IAdminGetUserDetailResponse,
  IGetUserDetailsResponse,
  IPaginatedAdminGetUserListResponse,
  IPaginatedAdminUserReportListResponse
} from "@/generated/interfaces/user.interface";
import { IInfiniteAdminUserReportListResponse } from "@/types/user";
import { withErrorProcessor } from "@/utils/errorHanlder";

export async function getUserById(id: string) {
  try {
    const url = "/api/admin/user/get-user-detail";
    const { data } = await protectedAxiosInstance.post<IAdminGetUserDetailResponse>(url, {
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
    const url = "/api/admin/recipe/get-user-activities";
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

export const getCurrentUserDetails = async () => {
  const url = "/api/user/get-current-user-details";

  try {
    const { data } = await protectedAxiosInstance.get<IGetUserDetailsResponse>(url);
    return data as GetUserDetailsResponse;
  } catch (error) {
    withErrorProcessor(error);
    throw error;
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
    const url = `/api/admin/user/get-users?Skip=${skip}&SortBy=${sortBy}&SortOrder=${sortOrder}&limit=${limit}&keyword=${encodeURIComponent(keyword)}`;

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

export async function markUserReport(reportId: string) {
  try {
    const url = "/api/admin/user/mark-report";
    const { data } = await protectedAxiosInstance.post(url, {
      reportId
    });

    return data;
  } catch (error) {
    console.log(error);
    throw error;
  }
}

export async function getUserReports(
  skip = 0,
  sortBy = "",
  sortOrder = "asc",
  keyword = "",
  limit = 6,
  language = "en"
) {
  try {
    const url = `/api/admin/user/get-user-reports?Skip=${skip}&SortBy=${sortBy}&SortOrder=${sortOrder}&limit=${limit}&language=${language}&keyword=${encodeURIComponent(keyword)}`;

    const { data } = await protectedAxiosInstance.get(url);

    return data as IPaginatedAdminUserReportListResponse;
  } catch (error) {
    console.log(error);
    throw error;
  }
}

export async function getUserDetailReports(
  accountId = "",
  language = "en",
  pageParam = 0
) {
  try {
    const url = "/api/admin/user/get-user-report-by-account-id";
    const { data } = await protectedAxiosInstance.post(url, {
      accountId,
      language,
      skip: pageParam.toString()
    });

    return data as IInfiniteAdminUserReportListResponse;
  } catch (error) {
    console.log(error);
    throw error;
  }
}
