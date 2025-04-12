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
import { StatisticDateItem, StatisticItem } from "@/types/statistic";
import { AxiosError } from "axios";

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
          keyword: keyword.trim()
        }
      }
    );

    return data;
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
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
          keyword: keyword.trim()
        }
      });

    return data;
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
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
    return withErrorProcessor(error as AxiosError);
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
    return withErrorProcessor(error as AxiosError);
  }
}

export const markReportAsCompleted = async ({ reportId, reportType }: IReportDTO) => {
  const url = "/api/admin/recipe/mark-report-complete";

  try {
    await protectedAxiosInstance.post<undefined>(url, { reportId, reportType });
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
};

export const markAllReportsAsCompleted = async ({ reportId, reportType }: IReportDTO) => {
  const url = "/api/admin/recipe/mark-report-complete";

  try {
    // await protectedAxiosInstance.post<undefined>(url, { reportId, reportType });
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
};

export const reopenReport = async ({ reportId, reportType }: IReportDTO) => {
  const url = "/api/admin/recipe/reopen-report";

  try {
    await protectedAxiosInstance.post<undefined>(url, { reportId, reportType });
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
};

export const reopenAllReports = async ({ reportId, reportType }: IReportDTO) => {
  const url = "/api/admin/recipe/reopen-report";

  try {
    // await protectedAxiosInstance.post<undefined>(url, { reportId, reportType });
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
};

export const disableRecipe = async (id: string) => {
  const url = "/api/admin/recipe";

  try {
    await protectedAxiosInstance.delete<undefined>(url, { params: { id } });
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
};

export const restoreRecipe = async (id: string) => {
  const url = "/api/admin/recipe/restore";

  try {
    await protectedAxiosInstance.put<undefined>(url, undefined, { params: { id } });
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
};

export async function getTotalRecipes() {
  const url = "/api/admin/recipe/statistic/get-total-recipe";
  try {
    const { data } = await protectedAxiosInstance.get<number>(url);
    return data;
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export async function getRecipeRanking() {
  const url = "/api/admin/recipe/statistic/get-recipe-ranking-by-views";
  try {
    const { data } = await protectedAxiosInstance.get<StatisticItem[]>(url);
    return data;
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export async function getRecipeStatistic() {
  const url = "/api/admin/recipe/statistic/get-recipe-statistic";
  try {
    const { data } = await protectedAxiosInstance.get<StatisticDateItem[]>(url);
    return data;
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export async function markAllRecipeReport({
  recipeId,
  isReopened
}: {
  recipeId: string;
  isReopened: boolean;
}) {
  const url = "/api/admin/recipe/mark-all-recipe-report";
  try {
    const { data } = await protectedAxiosInstance.post(url, { recipeId, isReopened });
    return data;
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}
