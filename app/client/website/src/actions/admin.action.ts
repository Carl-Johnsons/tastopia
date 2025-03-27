"use server";

import { withErrorProcessor } from "@/utils/errorHanlder";
import { PaginatedQueryParams } from "@/types/common";
import { adminDetailData, adminListData } from "@/constants/admin";
import { protectedAxiosInstance } from "@/constants/host";
import { IAdminListResponse } from "@/types/admin";

export async function getAdmins(options?: PaginatedQueryParams) {
  const url = "/api/admin/recipe/get-recipes";
  const {
    limit = 10,
    skip = 0,
    sortBy = "createdAt",
    sortOrder = "DESC",
    lang = "en",
    keyword = ""
  } = options || {};

  try {
    // const { data } =
    // await protectedAxiosInstance.get<IPaginatedAdminListResponse>(url, {
    // params: {
    // limit,
    // skip,
    // sortBy,
    // sortOrder,
    // lang,
    // keyword: encodeURIComponent(keyword)
    // }
    // });

    return adminListData;
  } catch (error) {
    withErrorProcessor(error);
    throw error;
  }
}

export async function getAdminById(id: string) {
  const url = "/api/admin/recipe/get-recipe-report-detail";

  try {
    // const { data } = await protectedAxiosInstance.get<IAdminReportRecipeDetailResponse>(
    // url,
    // {
    // params: {
    // recipeId,
    // lang
    // }
    // }
    // );

    return adminDetailData;
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
