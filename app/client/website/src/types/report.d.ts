import { InteractiveButtonProps } from "@/components/shared/common/Button";

type ReportActionButtonsProps = {
  targetId: string;
  reportId: string;
  status: ReportStatus;
};

type CommentReportActionButtonsProps = ReportActionButtonsProps & {
  recipeId: string;
};

interface IPaginatedAdminReportCommentListResponse {
  paginatedData: IAdminReportCommentResponse[];
  metadata?: INumberedPaginatedMetadata;
}

interface IAdminReportCommentResponse {
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
  "noTruncateText" | "noText"
> & {
  title: string;
  recipeId?: string;
  targetId: string;
  /** Callback to override the component's internal click hanlder. */
  onClick?: () => void;
  onSuccess?: () => void;
  onFailure?: () => void;
  className?: string;
};

type CommentDataTableButtonProps = DataTableButtonProps & {
  recipeId: string;
};

type GetReportsParams = {
  limit?: number;
  skip?: number;
  sortBy?: string;
  sortOrder?: string;
  lang?: string;
  keyword?: string;
};

type GetReportDetailOptions = {
  options?: {
    lang?: string;
  };
};

type GetCommentReportDetailParams = GetReportDetailOptions & {
  recipeId: string;
  commentId: string;
};
