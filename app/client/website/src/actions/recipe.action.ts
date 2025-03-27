"use server";

import { protectedAxiosInstance } from "@/constants/host";
import {
  IAdminReportRecipeDetailResponse,
  IPaginatedAdminRecipeListResponse,
  IPaginatedAdminReportRecipeListResponse,
  IPaginatedRecipeCommentListResponse,
  IReportDTO
} from "@/generated/interfaces/recipe.interface";
import { IGetRecipeCommentsDTO } from "../../../mobile/generated/interfaces/recipe.interface";
import { withErrorProcessor } from "@/utils/errorHanlder";
import { PaginatedQueryParams } from "@/types/common";

export async function getRecipes(options?: PaginatedQueryParams) {
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
    const { data } = await protectedAxiosInstance.get<IPaginatedAdminRecipeListResponse>(
      url,
      {
        params: {
          limit,
          skip,
          sortBy,
          sortOrder,
          lang,
          keyword: encodeURIComponent(keyword)
        }
      }
    );

    return data;
  } catch (error) {
    console.log(error);
    throw error;
  }
}

export async function getRecipeReports(options?: PaginatedQueryParams) {
  const url = "/api/admin/recipe/get-recipe-reports";
  const {
    limit = 10,
    skip = 0,
    sortBy = "createdAt",
    sortOrder = "DESC",
    lang = "en",
    keyword = ""
  } = options || {};

  try {
    const { data } =
      await protectedAxiosInstance.get<IPaginatedAdminReportRecipeListResponse>(url, {
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
    console.log(error);
    throw error;
  }
}

type GetRecipeReportDetailParams = {
  recipeId: string;
  options?: {
    lang?: string;
  };
};

export async function getRecipeReportById({
  recipeId,
  options
}: GetRecipeReportDetailParams) {
  const url = "/api/admin/recipe/get-recipe-report-detail";
  const { lang = "en" } = options || {};

  try {
    const { data } = await protectedAxiosInstance.get<IAdminReportRecipeDetailResponse>(
      url,
      {
        params: {
          recipeId,
          lang
        }
      }
    );

    return data;
  } catch (error) {
    console.log(error);
    throw error;
  }
}

export type GetRecipeCommentsParams = {
  recipeId: string;
  options?: Omit<IGetRecipeCommentsDTO, "recipeId">;
};

export async function getRecipeComments({ recipeId, options }: GetRecipeCommentsParams) {
  const url = "/api/recipe/get-recipe-comments";

  try {
    const { data } =
      await protectedAxiosInstance.post<IPaginatedRecipeCommentListResponse>(url, {
        recipeId,
        ...options
      });

    return data;
  } catch (error) {
    console.log(error);
    throw error;
  }
}

export const markReportAsCompleted = async ({ reportId, reportType }: IReportDTO) => {
  const url = "/api/admin/recipe/mark-report-complete";

  try {
    await protectedAxiosInstance.post<undefined>(url, { reportId, reportType });
  } catch (error) {
    withErrorProcessor(error);
    throw error;
  }
};

export const reopenReport = async ({ reportId, reportType }: IReportDTO) => {
  const url = "/api/admin/recipe/reopen-report";

  try {
    await protectedAxiosInstance.post<undefined>(url, { reportId, reportType });
  } catch (error) {
    withErrorProcessor(error);
    throw error;
  }
};

export const disableRecipe = async (id: string) => {
  const url = "/api/admin/recipe";

  try {
    await protectedAxiosInstance.delete<undefined>(url, { params: { id } });
  } catch (error) {
    withErrorProcessor(error);
    throw error;
  }
};

export const restoreRecipe = async (id: string) => {
  const url = "/api/admin/recipe/restore";

  try {
    await protectedAxiosInstance.put<undefined>(url, undefined, { params: { id } });
  } catch (error) {
    withErrorProcessor(error);
    throw error;
  }
};
