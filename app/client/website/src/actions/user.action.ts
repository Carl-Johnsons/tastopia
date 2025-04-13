"use server";

import { protectedAxiosInstance } from "@/constants/host";
import { SETTING_KEY, SETTING_VALUE } from "@/constants/settings";
import { AxiosError } from "axios";
import { IErrorResponseDTO } from "@/generated/interfaces/common.interface";
import {
  IAdminGetUserDetailResponse,
  IPaginatedAdminGetUserListResponse,
  IPaginatedAdminUserReportListResponse
} from "@/generated/interfaces/user.interface";
import { IInfiniteAdminUserReportListResponse } from "@/types/user";
import { StatisticDateItem } from "@/types/statistic";
import { Response } from "@/types/common";
import { withErrorProcessor, withSuccessfulResponse } from "@/utils/errorHanlder";

export async function getUserById(
  id: string
): Promise<Response<IAdminGetUserDetailResponse>> {
  try {
    const url = "/api/admin/user/get-user-detail";
    const { data } = await protectedAxiosInstance.post<IAdminGetUserDetailResponse>(url, {
      accountId: id
    });

    return withSuccessfulResponse(data);
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
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

    return withSuccessfulResponse(data);
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
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

export const useUpdateSettings = async (
  data: UpdateSettingParams
): Promise<Response<UpdateSettingResponse>> => {
  const url = "/api/setting";

  try {
    const { data: response } = await protectedAxiosInstance.put<UpdateSettingResponse>(
      url,
      data
    );

    return withSuccessfulResponse(response);
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
};

export async function getAdminUsers(
  skip = 0,
  sortBy = "",
  sortOrder = "asc",
  keyword = "",
  limit = 6
): Promise<Response<IPaginatedAdminGetUserListResponse>> {
  try {
    const url = `/api/admin/user/get-users?Skip=${skip}&SortBy=${sortBy}&SortOrder=${sortOrder}&limit=${limit}&keyword=${encodeURIComponent(keyword.trim())}`;

    const { data } =
      await protectedAxiosInstance.get<IPaginatedAdminGetUserListResponse>(url);

    return withSuccessfulResponse(data);
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export async function adminBanUser(accountId: string): Promise<Response<any>> {
  try {
    const url = "/api/admin/user/ban-user";
    const { data } = await protectedAxiosInstance.post(url, {
      accountId
    });

    return withSuccessfulResponse(data);
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export async function markUserReport(reportId: string): Promise<Response<any>> {
  try {
    const url = "/api/admin/user/mark-report";
    const { data } = await protectedAxiosInstance.post(url, {
      reportId
    });

    return withSuccessfulResponse(data);
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export async function getUserReports(
  skip = 0,
  sortBy = "",
  sortOrder = "asc",
  keyword = "",
  limit = 6,
  language = "en"
): Promise<Response<IPaginatedAdminUserReportListResponse>> {
  try {
    const url = `/api/admin/user/get-user-reports?Skip=${skip}&SortBy=${sortBy}&SortOrder=${sortOrder}&limit=${limit}&language=${language}&keyword=${encodeURIComponent(keyword.trim())}`;

    const { data } =
      await protectedAxiosInstance.get<IPaginatedAdminUserReportListResponse>(url);

    return withSuccessfulResponse(data);
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export async function getUserDetailReports(
  accountId = "",
  language = "en",
  pageParam = 0
): Promise<Response<IInfiniteAdminUserReportListResponse>> {
  try {
    const url = "/api/admin/user/get-user-report-by-account-id";
    const { data } =
      await protectedAxiosInstance.post<IInfiniteAdminUserReportListResponse>(url, {
        accountId,
        language,
        skip: pageParam.toString()
      });

    return withSuccessfulResponse(data);
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export async function getTotalUsers(): Promise<Response<number>> {
  const url = "/api/admin/user/statistic/get-total-user";
  try {
    const { data } = await protectedAxiosInstance.get<number>(url);
    return withSuccessfulResponse(data);
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export async function getAccountStatistic(): Promise<Response<StatisticDateItem[]>> {
  const url = "/api/admin/account/statistic/get-account-statistic";

  try {
    const { data } = await protectedAxiosInstance.get<StatisticDateItem[]>(url);
    return withSuccessfulResponse(data);
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}
