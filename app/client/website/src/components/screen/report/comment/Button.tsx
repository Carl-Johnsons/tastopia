"use client";

import {
  useDisableComment,
  useMarkAllReport,
  useMarkReportAsCompleted,
  useReopenReport,
  useRestoreComment
} from "@/api/comment";
import InteractiveButton, { DataTableButton } from "@/components/shared/common/Button";
import { BanIcon } from "@/components/shared/icons";
import { ReportType } from "@/constants/reports";
import { useInvalidateAdmin } from "@/hooks/query";
import { Link, useRouter } from "@/i18n/navigation";
import { CommentDataTableButtonProps, DataTableButtonProps } from "@/types/report";
import { useQueryClient } from "@tanstack/react-query";
import { Check, RotateCw, Search } from "lucide-react";
import { useTranslations } from "next-intl";
import { useCallback, useMemo, useState } from "react";
import { toast } from "react-toastify";

export const ViewDetailButton = ({
  title,
  onSuccess,
  onFailure,
  recipeId,
  targetId,
  className
}: CommentDataTableButtonProps) => {
  const router = useRouter();
  const [isLoading, setIsLoading] = useState(false);
  const url = useMemo(
    () => `/reports/comments/detail/recipe/${recipeId}/comment/${targetId}`,
    [recipeId, targetId]
  );

  const handleClick = useCallback(() => {
    setIsLoading(true);

    try {
      router.push(url);
      onSuccess && onSuccess();
    } catch (error) {
      onFailure && onFailure();
      console.log(error);
    } finally {
      setIsLoading(false);
    }
  }, [onSuccess, onFailure, router, url]);

  return (
    <Link href={url}>
      <DataTableButton
        title={title}
        icon={<Search className='text-white_black' />}
        isLoading={isLoading}
        onClick={handleClick}
        className={className}
      />
    </Link>
  );
};

/**
 * Reopen a report.
 */
export const ReopenReportButton = ({
  title,
  targetId,
  commentId,
  onSuccess,
  onFailure,
  className,
  disabled,
  ...props
}: DataTableButtonProps) => {
  const { mutate, isPending } = useReopenReport();
  const queryClient = useQueryClient();
  const { invalidateCurrentAdminActivities } = useInvalidateAdmin();
  const t = useTranslations("report");

  const handleClick = useCallback(() => {
    mutate(
      {
        reportId: targetId,
        reportType: ReportType.COMMENT
      },
      {
        onSuccess: async () => {
          toast.success(t("reopenSuccess"));
          await queryClient.invalidateQueries({ queryKey: ["commentReport", commentId] });
          invalidateCurrentAdminActivities();
          onSuccess && onSuccess();
        },
        onError: ({ message }) => {
          toast.error(message);
          onFailure && onFailure();
        }
      }
    );
  }, [
    t,
    commentId,
    onSuccess,
    onFailure,
    targetId,
    mutate,
    queryClient,
    invalidateCurrentAdminActivities
  ]);

  return (
    <DataTableButton
      title={title}
      icon={<RotateCw className='text-white_black' />}
      isLoading={isPending}
      disabled={isPending || disabled}
      onClick={handleClick}
      className={`bg-green-400 hover:bg-green-500 ${className}`}
      {...props}
    />
  );
};

/**
 * Reopen all reports for a comment.
 */
export const ReopenAllReportsButton = ({
  title,
  targetId,
  recipeId,
  commentId,
  onSuccess,
  onFailure,
  className,
  disabled,
  ...props
}: DataTableButtonProps) => {
  const { mutate, isPending } = useMarkAllReport();
  const queryClient = useQueryClient();
  const { invalidateCurrentAdminActivities } = useInvalidateAdmin();
  const t = useTranslations("report");

  const handleClick = useCallback(async () => {
    mutate(
      { commentId: targetId, recipeId: recipeId as string, isReopened: true },
      {
        onSuccess: async () => {
          toast.success(t("reopenAllSuccess"));
          await queryClient.invalidateQueries({ queryKey: ["commentReport", commentId] });
          invalidateCurrentAdminActivities();
          onSuccess && onSuccess();
        },
        onError: ({ message }) => {
          toast.error(message);
          onFailure && onFailure();
        }
      }
    );
  }, [
    t,
    commentId,
    onSuccess,
    onFailure,
    targetId,
    recipeId,
    mutate,
    queryClient,
    invalidateCurrentAdminActivities
  ]);

  return (
    <DataTableButton
      title={title}
      icon={<RotateCw className='text-white_black' />}
      isLoading={isPending}
      disabled={isPending || disabled}
      onClick={handleClick}
      className={`bg-green-400 hover:bg-green-500 ${className}`}
      {...props}
    />
  );
};

/**
 * Mark a report as completed.
 */
export const MarkAsCompletedButton = ({
  title,
  targetId,
  commentId,
  onSuccess,
  onFailure,
  className,
  disabled,
  ...props
}: DataTableButtonProps) => {
  const { mutate, isPending } = useMarkReportAsCompleted();
  const queryClient = useQueryClient();
  const { invalidateCurrentAdminActivities } = useInvalidateAdmin();
  const t = useTranslations("report");

  const handleClick = useCallback(async () => {
    mutate(
      { reportId: targetId, reportType: ReportType.COMMENT },
      {
        onSuccess: async () => {
          toast.success(t("resolveSuccess"));
          await queryClient.invalidateQueries({ queryKey: ["commentReport", commentId] });
          invalidateCurrentAdminActivities();
          onSuccess && onSuccess();
        },
        onError: ({ message }) => {
          toast.error(message);
          onFailure && onFailure();
        }
      }
    );
  }, [
    t,
    commentId,
    onSuccess,
    onFailure,
    targetId,
    mutate,
    queryClient,
    invalidateCurrentAdminActivities
  ]);

  return (
    <DataTableButton
      title={title}
      icon={<Check className='text-white_black' />}
      isLoading={isPending}
      disabled={isPending || disabled}
      onClick={handleClick}
      className={`bg-purple-400 hover:bg-purple-500 ${className}`}
      {...props}
    />
  );
};

/**
 * Mark all reports as completed.
 */
export const MarkAllAsCompletedButton = ({
  title,
  targetId,
  recipeId,
  onSuccess,
  onFailure,
  className,
  disabled,
  ...props
}: DataTableButtonProps) => {
  const { mutate, isPending } = useMarkAllReport();
  const queryClient = useQueryClient();
  const { invalidateCurrentAdminActivities } = useInvalidateAdmin();
  const t = useTranslations("report");

  const handleClick = useCallback(async () => {
    mutate(
      { commentId: targetId, recipeId: recipeId as string, isReopened: false },
      {
        onSuccess: async () => {
          toast.success(t("resolveAllSuccess"));
          await queryClient.invalidateQueries({ queryKey: ["commentReport", targetId] });
          invalidateCurrentAdminActivities();
          onSuccess && onSuccess();
        },
        onError: ({ message }) => {
          toast.error(message);
          onFailure && onFailure();
        }
      }
    );
  }, [
    t,
    onSuccess,
    onFailure,
    targetId,
    recipeId,
    mutate,
    queryClient,
    invalidateCurrentAdminActivities
  ]);

  return (
    <DataTableButton
      title={title}
      icon={<Check className='text-white_black' />}
      isLoading={isPending}
      disabled={isPending || disabled}
      onClick={handleClick}
      className={`bg-purple-400 hover:bg-purple-500 ${className}`}
      {...props}
    />
  );
};

/**
 * Disable a comment.
 */
export const DisableCommentButton = ({
  title,
  targetId,
  recipeId,
  onSuccess,
  onFailure,
  className
}: CommentDataTableButtonProps) => {
  const { mutate, isPending } = useDisableComment();
  const queryClient = useQueryClient();
  const { invalidateCurrentAdminActivities } = useInvalidateAdmin();
  const t = useTranslations("administerReportComments");

  const handleClick = useCallback(async () => {
    mutate(
      { recipeId, commentId: targetId },
      {
        onSuccess: async () => {
          toast.success(t("disableSuccess"));
          await queryClient.invalidateQueries({ queryKey: ["comment", targetId] });
          invalidateCurrentAdminActivities();
          onSuccess && onSuccess();
        },
        onError: ({ message }) => {
          toast.error(message);
          onFailure && onFailure();
        }
      }
    );
  }, [
    onSuccess,
    onFailure,
    recipeId,
    targetId,
    mutate,
    queryClient,
    invalidateCurrentAdminActivities
  ]);

  return (
    <InteractiveButton
      title={title}
      icon={<BanIcon className='text-white_black' />}
      isLoading={isPending}
      onClick={handleClick}
      className={`bg-red-400 hover:bg-red-500 ${className}`}
    />
  );
};

/**
 * Restore a comment.
 */
export const RestoreCommentButton = ({
  title,
  targetId,
  recipeId,
  onSuccess,
  onFailure,
  className
}: CommentDataTableButtonProps) => {
  const { mutate, isPending } = useRestoreComment();
  const queryClient = useQueryClient();
  const { invalidateCurrentAdminActivities } = useInvalidateAdmin();
  const t = useTranslations("administerReportComments");

  const handleClick = useCallback(async () => {
    mutate(
      { recipeId, commentId: targetId },
      {
        onSuccess: async () => {
          toast.success(t("restoreSuccess"));
          await queryClient.invalidateQueries({ queryKey: ["comment", targetId] });
          invalidateCurrentAdminActivities();
          onSuccess && onSuccess();
        },
        onError: ({ message }) => {
          toast.error(message);
          onFailure && onFailure();
        }
      }
    );
  }, [
    t,
    onSuccess,
    onFailure,
    recipeId,
    targetId,
    mutate,
    queryClient,
    invalidateCurrentAdminActivities
  ]);

  return (
    <InteractiveButton
      title={title}
      icon={<RotateCw className='text-white_black' />}
      isLoading={isPending}
      onClick={handleClick}
      className={`bg-green-400 hover:bg-green-500 ${className}`}
    />
  );
};
