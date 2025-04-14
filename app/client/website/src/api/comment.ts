import {
  disableComment,
  getCommentReportById,
  getCommentReports,
  MarkAllCommentReportsParams,
  markAllReports,
  markReportAsCompleted,
  reopenReport,
  restoreComment
} from "@/actions/comment.action";
import { IReportDTO } from "@/generated/interfaces/recipe.interface";
import { useErrorHandler } from "@/hooks/error/useErrorHanler";
import { ApiError } from "@/lib/error/errorHanler";
import { ChangeCommentStateDTO } from "@/types/comment";
import { GetCommentReportDetailParams, GetReportsParams } from "@/types/report";
import { useMutation, useQuery } from "@tanstack/react-query";

export const useGetCommentReports = ({
  limit,
  skip,
  sortBy,
  sortOrder,
  lang,
  keyword
}: GetReportsParams) => {
  const { withErrorProcessor } = useErrorHandler();

  return useQuery({
    queryKey: ["commentReports", skip, sortBy, sortOrder, lang, keyword, limit],
    queryFn: () =>
      withErrorProcessor(() =>
        getCommentReports({
          limit,
          skip,
          sortBy,
          sortOrder,
          lang,
          keyword
        })
      )
  });
};

export const useReopenReport = () => {
  const { withErrorProcessor } = useErrorHandler();

  return useMutation<void, ApiError, IReportDTO>({
    mutationFn: params => withErrorProcessor(() => reopenReport(params))
  });
};

export const useMarkReportAsCompleted = () => {
  const { withErrorProcessor } = useErrorHandler();

  return useMutation<void, ApiError, IReportDTO>({
    mutationFn: params => withErrorProcessor(() => markReportAsCompleted(params))
  });
};

export const useDisableComment = () => {
  const { withErrorProcessor } = useErrorHandler();

  return useMutation<void, ApiError, ChangeCommentStateDTO>({
    mutationFn: params => withErrorProcessor(() => disableComment(params))
  });
};

export const useRestoreComment = () => {
  const { withErrorProcessor } = useErrorHandler();

  return useMutation<void, ApiError, ChangeCommentStateDTO>({
    mutationFn: params => withErrorProcessor(() => restoreComment(params))
  });
};

export const useGetCommentReport = (params: GetCommentReportDetailParams) => {
  const { withErrorProcessor } = useErrorHandler();
  const { options } = params;
  const { lang } = options || {};

  return useQuery({
    queryKey: ["commentReport", params.commentId, lang],
    queryFn: () => withErrorProcessor(() => getCommentReportById(params))
  });
};

export const useMarkAllReport = () => {
  const { withErrorProcessor } = useErrorHandler();

  return useMutation<void, ApiError, MarkAllCommentReportsParams>({
    mutationFn: params => withErrorProcessor(() => markAllReports(params))
  });
};
