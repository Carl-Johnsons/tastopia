"use server";

import { protectedAxiosInstance } from "@/constants/host";
import {
  IAdminReportCommentDetailResponse,
  IPaginatedAdminReportCommentListResponse,
  IReportDTO
} from "@/generated/interfaces/recipe.interface";
import { withErrorProcessor } from "@/utils/errorHanlder";
import { GetCommentReportDetailParams } from "@/types/report";
import { ChangeCommentStateDTO } from "@/types/comment";
import { PaginatedQueryParams } from "@/types/common";

export async function getCommentReports(options?: PaginatedQueryParams) {
  const url = "/api/admin/recipe/comment/reports/all";
  const {
    limit = 10,
    skip = 0,
    sortBy = "createdAt",
    sortOrder = "DESC",
    lang = "vi",
    keyword = ""
  } = options || {};

  try {
    const { data } =
      await protectedAxiosInstance.get<IPaginatedAdminReportCommentListResponse>(url, {
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

export async function getCommentReportById({
  recipeId,
  commentId,
  options
}: GetCommentReportDetailParams) {
  const url = "/api/admin/recipe/comment/reports";
  const { lang = "en" } = options || {};

  try {
    const { data } = await protectedAxiosInstance.get<IAdminReportCommentDetailResponse>(
      url,
      {
        params: {
          recipeId,
          commentId,
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

export const markReportAsCompleted = async ({ reportId, reportType }: IReportDTO) => {
  const url = "/api/admin/recipe/mark-report-complete";

  try {
    await protectedAxiosInstance.post<undefined>(url, { reportId, reportType });
  } catch (error) {
    withErrorProcessor(error);
  }
};

export const reopenReport = async ({ reportId, reportType }: IReportDTO) => {
  const url = "/api/admin/recipe/reopen-report";

  try {
    await protectedAxiosInstance.post<undefined>(url, { reportId, reportType });
  } catch (error) {
    withErrorProcessor(error);
  }
};

export const disableComment = async ({ recipeId, commentId }: ChangeCommentStateDTO) => {
  const url = "/api/admin/recipe/comment";

  try {
    await protectedAxiosInstance.delete<undefined>(url, {
      params: { recipeId, commentId }
    });
  } catch (error) {
    withErrorProcessor(error);
  }
};

export const restoreComment = async ({ recipeId, commentId }: ChangeCommentStateDTO) => {
  const url = "/api/admin/recipe/comment/restore";

  try {
    await protectedAxiosInstance.put<undefined>(url, undefined, {
      params: { recipeId, commentId }
    });
  } catch (error) {
    withErrorProcessor(error);
  }
};
