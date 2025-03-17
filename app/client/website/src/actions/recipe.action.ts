"use server";

import { protectedAxiosInstance } from "@/constants/host";
import {
  IAdminReportRecipeDetailResponse,
  IPaginatedAdminReportRecipeListResponse
} from "@/generated/interfaces/recipe.interface";
import { stringify } from "@/utils/debug";

export type GetRecipeReportsParams = {
  limit?: number;
  skip?: number;
  sortBy?: string;
  sortOrder?: string;
  lang?: string;
};

export async function getRecipeReports(options?: GetRecipeReportsParams) {
  const url = "/api/recipe/admin-get-recipe-reports";
  const {
    limit = 10,
    skip = 0,
    sortBy = "createdAt",
    sortOrder = "DESC",
    lang = "vi"
  } = options || {};

  console.log("getRecipeReports", {
    limit,
    skip,
    sortBy,
    sortOrder,
    lang
  });

  try {
    const { data } =
      await protectedAxiosInstance.get<IPaginatedAdminReportRecipeListResponse>(url, {
        params: {
          limit,
          skip,
          sortBy,
          sortOrder,
          lang
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
  const url = "/api/recipe/admin-get-recipe-report-detail";
  const { lang = "vi" } = options || {};

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
    console.error("getRecipeReportById", stringify(error));
    throw error;
  }
}
