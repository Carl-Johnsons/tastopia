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
import { PaginatedQueryParams, Response } from "@/types/common";
import { AxiosError } from "axios";

export async function getCommentReports(
  options?: PaginatedQueryParams
): Promise<Response<IPaginatedAdminReportCommentListResponse>> {
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

export async function getCommentReportById({
  recipeId,
  commentId,
  options
}: GetCommentReportDetailParams): Promise<Response<IAdminReportCommentDetailResponse>> {
  const url = "/api/admin/recipe/comment/reports";
  const { lang } = options || {};

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

export const disableComment = async ({
  recipeId,
  commentId
}: ChangeCommentStateDTO): Promise<Response<undefined>> => {
  const url = "/api/admin/recipe/comment";

  try {
    await protectedAxiosInstance.delete<undefined>(url, {
      params: { recipeId, commentId }
    });
    return {
      ok: true,
      data: undefined
    };
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
};

export const restoreComment = async ({
  recipeId,
  commentId
}: ChangeCommentStateDTO): Promise<Response<undefined>> => {
  const url = "/api/admin/recipe/comment/restore";

  try {
    await protectedAxiosInstance.put<undefined>(url, undefined, {
      params: { recipeId, commentId }
    });
    return {
      ok: true,
      data: undefined
    };
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
};

export type MarkAllCommentReportsParams = {
  recipeId: string;
  commentId: string;
  isReopened: boolean;
};

export const markAllReports = async (
  params: MarkAllCommentReportsParams
): Promise<Response<undefined>> => {
  const url = "/api/admin/recipe/mark-all-comment-report";

  try {
    const { data } = await protectedAxiosInstance.post(url, params);
    return {
      ok: true,
      data
    };
  } catch (error) {
    return withErrorProcessor(error as AxiosError);
  }
};
