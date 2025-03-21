"use client";

import {
  useDisableComment,
  useMarkReportAsCompleted,
  useReopenReport,
  useRestoreComment
} from "@/api/comment";
import InteractiveButton, { DataTableButton } from "@/components/shared/common/Button";
import { BanIcon } from "@/components/shared/icons";
import { ReportType } from "@/constants/reports";
import { useRouter } from "@/i18n/navigation";
import { CommentDataTableButtonProps, DataTableButtonProps } from "@/types/report";
import { useQueryClient } from "@tanstack/react-query";
import { Check, RotateCw, Search } from "lucide-react";
import { useCallback, useState } from "react";
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

  const handleClick = useCallback(() => {
    setIsLoading(true);

    try {
      router.push(`/reports/comments/detail/recipe/${recipeId}/comment/${targetId}`);
      onSuccess && onSuccess();
    } catch (error) {
      onFailure && onFailure();
      console.log(error);
    } finally {
      setIsLoading(false);
    }
  }, [onSuccess, onFailure, targetId]);

  return (
    <DataTableButton
      title={title}
      icon={<Search className='text-white_black' />}
      isLoading={isLoading}
      onClick={handleClick}
      className={className}
    />
  );
};

/**
 * Reopen a report.
 */
export const ReopenReportButton = ({
  title,
  targetId,
  onSuccess,
  onFailure,
  className,
  ...props
}: DataTableButtonProps) => {
  const { mutate, isPending } = useReopenReport();
  const queryClient = useQueryClient();

  const handleClick = useCallback(() => {
    mutate(
      {
        reportId: targetId,
        reportType: ReportType.COMMENT
      },
      {
        onSuccess: async () => {
          toast.success("Report reopened successfully.");
          await queryClient.invalidateQueries({ queryKey: ["commentReport", targetId] });
          onSuccess && onSuccess();
        },
        onError: ({ message }) => {
          toast.error(message);
          onFailure && onFailure();
        }
      }
    );
  }, [onSuccess, onFailure, targetId, mutate]);

  return (
    <DataTableButton
      title={title}
      icon={<RotateCw className='text-white_black' />}
      isLoading={isPending}
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
  onSuccess,
  onFailure,
  className,
  ...props
}: DataTableButtonProps) => {
  const { mutate, isPending } = useMarkReportAsCompleted();
  const queryClient = useQueryClient();

  const handleClick = useCallback(async () => {
    mutate(
      { reportId: targetId, reportType: ReportType.COMMENT },
      {
        onSuccess: async () => {
          toast.success("Report marked as completed successfully.");
          await queryClient.invalidateQueries({ queryKey: ["commentReport", targetId] });
          onSuccess && onSuccess();
        },
        onError: ({ message }) => {
          toast.error(message);
          onFailure && onFailure();
        }
      }
    );
  }, [onSuccess, onFailure, targetId, mutate]);

  return (
    <DataTableButton
      title={title}
      icon={<Check className='text-white_black' />}
      isLoading={isPending}
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

  const handleClick = useCallback(async () => {
    mutate(
      { recipeId, commentId: targetId },
      {
        onSuccess: async () => {
          toast.success("Comment disabled successfully.");
          await queryClient.invalidateQueries({ queryKey: ["comment", targetId] });
          onSuccess && onSuccess();
        },
        onError: ({ message }) => {
          toast.error(message);
          onFailure && onFailure();
        }
      }
    );
  }, [onSuccess, onFailure, recipeId, targetId, mutate]);

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

  const handleClick = useCallback(async () => {
    mutate(
      { recipeId, commentId: targetId },
      {
        onSuccess: async () => {
          toast.success("Comment restored successfully.");
          await queryClient.invalidateQueries({ queryKey: ["comment", targetId] });
          onSuccess && onSuccess();
        },
        onError: ({ message }) => {
          toast.error(message);
          onFailure && onFailure();
        }
      }
    );
  }, [onSuccess, onFailure, recipeId, targetId, mutate]);

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
