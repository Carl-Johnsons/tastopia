import { InteractiveButtonProps } from "@/components/shared/common/Button";
import { ReportStatus } from "@/constants/reports";
import { INumberedPaginatedMetadata } from "@/generated/interfaces/common.interface";
import { IAdminReportCommentDetailResponse } from "@/generated/interfaces/recipe.interface";

type ReportActionButtonsProps = {
  targetId: string;
  reportId: string;
  status: ReportStatus;
};

export type CommentReportActionButtonsProps = ReportActionButtonsProps & {
  recipeId: string;
};

export interface IPaginatedAdminReportCommentListResponse {
  paginatedData: IAdminReportCommentDetailResponse[];
  metadata?: INumberedPaginatedMetadata;
}

export interface IAdminReportCommentResponse {
  reportId: string;
  commentId: string;
  commentOwnerUsername: string;
  commentContent: string;
  isCommentActive: boolean;
  recipeId: string;
  recipeTitle: string;
  recipeImageURL: string;
  reporterUsername: string;
  reportReason: string;
  createdAt: string;
  status: string;
}

export type DataTableButtonProps = Pick<
  InteractiveButtonProps,
  "noTruncateText" | "noText" | "toolTip" | "disabled"
> & {
  title: string;
  recipeId?: string;
  commentId?: string;
  targetId: string;
  /** Callback to override the component's internal click hanlder. */
  onClick?: () => void;
  onSuccess?: () => void;
  onFailure?: () => void;
  className?: string;
};

export type CommentDataTableButtonProps = Omit<DataTableButtonProps, "title"> & {
  recipeId: string;
};

export type GetReportsParams = {
  limit?: number;
  skip?: number;
  sortBy?: string;
  sortOrder?: string;
  lang?: string;
  keyword?: string;
};

export type GetReportDetailOptions = {
  options?: {
    lang?: string;
  };
};

export type GetCommentReportDetailParams = GetReportDetailOptions & {
  recipeId: string;
  commentId: string;
};
