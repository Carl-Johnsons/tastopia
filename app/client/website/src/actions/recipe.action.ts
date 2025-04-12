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
import { PaginatedQueryParams, Response } from "@/types/common";
import { StatisticDateItem, StatisticItem } from "@/types/statistic";
import { AxiosError } from "axios";

export async function getRecipes(
  options?: PaginatedQueryParams
): Promise<Response<IPaginatedAdminRecipeListResponse>> {
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

    return {
      ok: true,
      data
    };
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export async function getRecipeReports(
  options?: PaginatedQueryParams
): Promise<Response<IPaginatedAdminReportRecipeListResponse>> {
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

    return {
      ok: true,
      data
    };
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export type GetRecipeReportDetailParams = {
  recipeId: string;
  options?: {
    lang?: string;
  };
};

export async function getRecipeReportById({
  recipeId,
  options
}: GetRecipeReportDetailParams): Promise<Response<IAdminReportRecipeDetailResponse>> {
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

    return {
      ok: true,
      data
    };
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export type GetRecipeCommentsParams = {
  recipeId: string;
  options?: Omit<IGetRecipeCommentsDTO, "recipeId">;
};

export async function getRecipeComments({
  recipeId,
  options
}: GetRecipeCommentsParams): Promise<Response<IPaginatedRecipeCommentListResponse>> {
  const url = "/api/recipe/get-recipe-comments";

  try {
    const { data } =
      await protectedAxiosInstance.post<IPaginatedRecipeCommentListResponse>(url, {
        recipeId,
        ...options
      });

    return {
      ok: true,
      data
    };
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export const markReportAsCompleted = async ({
  reportId,
  reportType
}: IReportDTO): Promise<Response<undefined>> => {
  const url = "/api/admin/recipe/mark-report-complete";

  try {
    await protectedAxiosInstance.post<undefined>(url, { reportId, reportType });
    return {
      ok: true,
      data: undefined
    };
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
};

export const reopenReport = async ({
  reportId,
  reportType
}: IReportDTO): Promise<Response<undefined>> => {
  const url = "/api/admin/recipe/reopen-report";

  try {
    await protectedAxiosInstance.post<undefined>(url, { reportId, reportType });
    return {
      ok: true,
      data: undefined
    };
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
};

export type MarkAllRecipeReportParams = {
  recipeId: string;
  isReopened: boolean;
};

export const markAllReports = async ({
  recipeId,
  isReopened
}: MarkAllRecipeReportParams): Promise<Response<undefined>> => {
  const url = "/api/admin/recipe/mark-all-recipe-report";

  try {
    const { data } = await protectedAxiosInstance.post(url, { recipeId, isReopened });
    return {
      ok: true,
      data
    };
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
};

export const disableRecipe = async (id: string): Promise<Response<undefined>> => {
  const url = "/api/admin/recipe";

  try {
    await protectedAxiosInstance.delete<undefined>(url, { params: { id } });
    return {
      ok: true,
      data: undefined
    };
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
};

export const restoreRecipe = async (id: string): Promise<Response<undefined>> => {
  const url = "/api/admin/recipe/restore";

  try {
    await protectedAxiosInstance.put<undefined>(url, undefined, { params: { id } });
    return {
      ok: true,
      data: undefined
    };
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
};

export async function getTotalRecipes(): Promise<Response<number>> {
  const url = "/api/admin/recipe/statistic/get-total-recipe";
  try {
    const { data } = await protectedAxiosInstance.get<number>(url);
    return {
      ok: true,
      data
    };
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export async function getRecipeRanking(): Promise<Response<StatisticItem[]>> {
  const url = "/api/admin/recipe/statistic/get-recipe-ranking-by-views";
  try {
    const { data } = await protectedAxiosInstance.get<StatisticItem[]>(url);
    return {
      ok: true,
      data
    };
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}

export async function getRecipeStatistic(): Promise<Response<StatisticDateItem[]>> {
  const url = "/api/admin/recipe/statistic/get-recipe-statistic";
  try {
    const { data } = await protectedAxiosInstance.get<StatisticDateItem[]>(url);
    return {
      ok: true,
      data
    };
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
}
