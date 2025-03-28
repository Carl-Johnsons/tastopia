"use server";

import { withErrorProcessor } from "@/utils/errorHanlder";
import { PaginatedQueryParams } from "@/types/common";
import { protectedAxiosInstance } from "@/constants/host";
import { IPaginatedAdminActivityLogListResponse } from "@/generated/interfaces/tracking.interface";
import {
  IAdminDetailResponse,
  IPaginatedAdminListResponse
} from "@/generated/interfaces/user.interface";

export async function getAdmins(options?: PaginatedQueryParams) {
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
        keyword: encodeURIComponent(keyword)
      }
    });

    return data;
  } catch (error) {
    withErrorProcessor(error);
    throw error;
  }
}

export async function getAdminById(id: string) {
  const url = "/api/admin/user/detail";

  try {
    const { data } = await protectedAxiosInstance.get<IAdminDetailResponse>(url, {
      params: {
        id
      }
    });

    return data;
  } catch (error) {
    withErrorProcessor(error);
    throw error;
  }
}

export const disableAdmin = async (id: string) => {
  const url = "/api/admin/recipe";

  try {
    // await protectedAxiosInstance.delete<undefined>(url, { params: { id } });
  } catch (error) {
    withErrorProcessor(error);
    throw error;
  }
};

export const restoreAdmin = async (id: string) => {
  const url = "/api/admin/recipe/restore";

  try {
    // await protectedAxiosInstance.put<undefined>(url, undefined, { params: { id } });
  } catch (error) {
    withErrorProcessor(error);
    throw error;
  }
};

export async function createAdmin(formData: FormData) {
  const url = "/api/admin/admin/create-admin";

  try {
    // const { data } = await protectedAxiosInstance.post<IAdminListResponse>(
    // url,
    // formData,
    // {
    // headers: {
    // "Content-Type": "multipart/form-data"
    // }
    // }
    // );
  } catch (error) {
    withErrorProcessor(error);
    throw error;
  }
}

export async function getAdminActivies(
  accountId: string,
  options?: Pick<PaginatedQueryParams, "skip" | "limit" | "lang">
) {
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

    return data;
  } catch (error) {
    withErrorProcessor(error);
    throw error;
  }
}
