import {
  disableComment,
  getCommentReports,
  markReportAsCompleted,
  reopenReport,
  restoreComment
} from "@/actions/comment.action";
import {
  IPaginatedAdminReportCommentListResponse,
  IReportDTO
} from "@/generated/interfaces/recipe.interface";
import { ChangeCommentStateDTO } from "@/types/comment";
import { GetReportsParams } from "@/types/report";
import { useMutation, useQuery } from "@tanstack/react-query";

export const useGetCommentReports = ({
  limit,
  skip,
  sortBy,
  sortOrder,
  lang,
  keyword
}: GetReportsParams) => {
  return useQuery<IPaginatedAdminReportCommentListResponse>({
    queryKey: ["commentReports", skip, sortBy, sortOrder, lang, keyword, limit],
    queryFn: () =>
      getCommentReports({
        limit,
        skip,
        sortBy,
        sortOrder,
        lang,
        keyword
      })
  });
};

export const useReopenReport = () => {
  return useMutation<void, Error, IReportDTO>({
    mutationFn: params => reopenReport(params)
  });
};

export const useMarkReportAsCompleted = () => {
  return useMutation<void, Error, IReportDTO>({
    mutationFn: params => markReportAsCompleted(params)
  });
};

export const useDisableComment = () => {
  return useMutation<void, Error, ChangeCommentStateDTO>({
    mutationFn: params => disableComment(params)
  });
};

export const useRestoreComment = () => {
  return useMutation<void, Error, ChangeCommentStateDTO>({
    mutationFn: params => restoreComment(params)
  });
};
