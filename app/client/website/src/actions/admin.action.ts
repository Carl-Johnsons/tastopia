"use server";

import { withErrorProcessor, withSuccessfulResponse } from "@/utils/errorHanlder";
import { PaginatedQueryParams, Response } from "@/types/common";
import { protectedAxiosInstance } from "@/constants/host";
import { IPaginatedAdminActivityLogListResponse } from "@/generated/interfaces/tracking.interface";
import {
  IAdminDetailResponse,
  IPaginatedAdminListResponse
} from "@/generated/interfaces/user.interface";
import { AxiosError } from "axios";

export async function getAdmins(
  options?: PaginatedQueryParams
): Promise<Response<IPaginatedAdminListResponse>> {
  const url = "/api/admin/user";
  const {
    limit = 10,
    skip = 0,
    sortBy = "createdAt",
    sortOrder = "DESC",
    lang = "en",
    keyword = ""
  } = options || {};

  try {
    const { data } = await protectedAxiosInstance.get<IPaginatedAdminListResponse>(url, {
      params: {
        limit,
        skip,
        sortBy,
        sortOrder,
        lang,
        keyword: keyword.trim()
      }
    });

    return withSuccessfulResponse(data);
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export async function getAdminById(id: string): Promise<Response<IAdminDetailResponse>> {
  const url = "/api/admin/user/detail";

  try {
    const { data } = await protectedAxiosInstance.get<IAdminDetailResponse>(url, {
      params: {
        id
      }
    });

    return withSuccessfulResponse(data);
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export async function getCurrentAdminDetail(): Promise<Response<IAdminDetailResponse>> {
  const url = "/api/admin/user/current";

  try {
    const { data } = await protectedAxiosInstance.get<IAdminDetailResponse>(url);
    return withSuccessfulResponse(data);
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export const disableAdmin = async (id: string): Promise<Response<void>> => {
  const url = "/api/admin/user/toggle-admin-active";

  try {
    await protectedAxiosInstance.post<undefined>(url, { accountId: id });
    return withSuccessfulResponse(undefined);
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
};

export const restoreAdmin = async (id: string) => {
  return disableAdmin(id);
};

export async function createAdmin(formData: FormData): Promise<Response<undefined>> {
  const url = "/api/admin/account";

  try {
    await protectedAxiosInstance.post<undefined>(url, formData, {
      headers: {
        "Content-Type": "multipart/form-data"
      }
    });

    return withSuccessfulResponse(undefined);
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export async function updateAdmin(
  formData: FormData,
  isSelf?: boolean
): Promise<Response<undefined>> {
  const url = isSelf ? "/api/admin/account/current" : "/api/admin/account";

  try {
    await protectedAxiosInstance.patch<undefined>(url, formData, {
      headers: {
        "Content-Type": "multipart/form-data"
      }
    });

    return withSuccessfulResponse(undefined);
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export async function getAdminActivies(
  accountId: string,
  options?: Pick<PaginatedQueryParams, "skip" | "limit" | "lang">
): Promise<Response<IPaginatedAdminActivityLogListResponse>> {
  const url = "/api/admin/tracking";
  const { limit = 10, skip = 0, lang = "en" } = options || {};

  try {
    const { data } =
      await protectedAxiosInstance.get<IPaginatedAdminActivityLogListResponse>(url, {
        params: {
          accountId,
          limit,
          skip,
          lang
        }
      });

    return withSuccessfulResponse(data);
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export async function getCurrentAdminActivies(
  options?: Pick<PaginatedQueryParams, "skip" | "limit" | "lang">
): Promise<Response<IPaginatedAdminActivityLogListResponse>> {
  const url = "/api/admin/tracking/current";
  const { limit = 10, skip = 0, lang = "en" } = options || {};

  try {
    const { data } =
      await protectedAxiosInstance.get<IPaginatedAdminActivityLogListResponse>(url, {
        params: {
          limit,
          skip,
          lang
        }
      });

    return withSuccessfulResponse(data);
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}
