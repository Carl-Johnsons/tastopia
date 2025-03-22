"use client";

import {
  useDisableRecipe,
  useMarkReportAsCompleted,
  useReopenReport,
  useRestoreRecipe
} from "@/api/recipe";
import InteractiveButton, { DataTableButton } from "@/components/shared/common/Button";
import { BanIcon } from "@/components/shared/icons";
import { ReportType } from "@/constants/reports";
import { CommentDataTableButtonProps, DataTableButtonProps } from "@/types/report";
import { useQueryClient } from "@tanstack/react-query";
import { Check, RotateCw, Search, Trash } from "lucide-react";
import { Link, useRouter } from "@/i18n/navigation";
import { useCallback, useMemo, useState } from "react";
import { toast } from "react-toastify";
import { useDisableComment, useRestoreComment } from "@/api/comment";

export const ViewDetailButton = ({
  title,
  onSuccess,
  onFailure,
  targetId,
  className
}: DataTableButtonProps) => {
  const router = useRouter();
  const [isLoading, setIsLoading] = useState(false);
  const url = useMemo(() => `/reports/recipes/detail/${targetId}`, [targetId]);

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
  }, [onSuccess, onFailure, targetId, router, url]);

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
  onSuccess,
  onFailure,
  className
}: DataTableButtonProps) => {
  const { mutate, isPending } = useReopenReport();
  const queryClient = useQueryClient();

  const handleClick = useCallback(() => {
    mutate(
      {
        reportId: targetId,
        reportType: ReportType.RECIPE
      },
      {
        onSuccess: async () => {
          toast.success("Report reopened successfully.");
          await queryClient.invalidateQueries({ queryKey: ["recipeReport", targetId] });
          onSuccess && onSuccess();
        },
        onError: ({ message }) => {
          toast.error(message);
          onFailure && onFailure();
        }
      }
    );
  }, [onSuccess, onFailure, targetId, mutate, queryClient]);

  return (
    <DataTableButton
      title={title}
      icon={<RotateCw className='text-white_black' />}
      isLoading={isPending}
      onClick={handleClick}
      className={`bg-green-400 hover:bg-green-500 ${className}`}
    />
  );
};

/**
 * Restore a recipe.
 */
export const RestoreRecipeButton = ({
  title,
  targetId,
  onSuccess,
  onFailure,
  className
}: DataTableButtonProps) => {
  const { mutate, isPending } = useRestoreRecipe();
  const queryClient = useQueryClient();

  const handleClick = useCallback(async () => {
    mutate(targetId, {
      onSuccess: async () => {
        toast.success("Recipe restored successfully.");
        await queryClient.invalidateQueries({ queryKey: ["recipe", targetId] });
        onSuccess && onSuccess();
      },
      onError: ({ message }) => {
        toast.error(message);
        onFailure && onFailure();
      }
    });
  }, [onSuccess, onFailure, targetId, mutate, queryClient]);

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

/**
 * Mark a report as completed.
 */
export const MarkAsCompletedButton = ({
  title,
  targetId,
  onSuccess,
  onFailure,
  className
}: DataTableButtonProps) => {
  const { mutate, isPending } = useMarkReportAsCompleted();
  const queryClient = useQueryClient();

  const handleClick = useCallback(async () => {
    mutate(
      { reportId: targetId, reportType: ReportType.RECIPE },
      {
        onSuccess: async () => {
          toast.success("Report marked as completed successfully.");
          await queryClient.invalidateQueries({ queryKey: ["recipeReport", targetId] });
          onSuccess && onSuccess();
        },
        onError: ({ message }) => {
          toast.error(message);
          onFailure && onFailure();
        }
      }
    );
  }, [onSuccess, onFailure, targetId, mutate, queryClient]);

  return (
    <DataTableButton
      title={title}
      icon={<Check className='text-white_black' />}
      isLoading={isPending}
      onClick={handleClick}
      className={`bg-purple-400 hover:bg-purple-500 ${className}`}
    />
  );
};

/**
 * Disable a recipe.
 */
export const DisableRecipeButton = ({
  title,
  targetId,
  onSuccess,
  onFailure,
  className
}: DataTableButtonProps) => {
  const { mutate, isPending } = useDisableRecipe();
  const queryClient = useQueryClient();

  const handleClick = useCallback(async () => {
    mutate(targetId, {
      onSuccess: async () => {
        toast.success("Recipe disabled successfully.");
        await queryClient.invalidateQueries({ queryKey: ["recipe", targetId] });
        onSuccess && onSuccess();
      },
      onError: ({ message }) => {
        toast.error(message);
        onFailure && onFailure();
      }
    });
  }, [onSuccess, onFailure, targetId, mutate, queryClient]);

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

export const SmallDisableCommentButton = ({
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
  }, [onSuccess, onFailure, recipeId, targetId, mutate, queryClient]);

  return (
    <DataTableButton
      title={title}
      icon={<Trash className='text-black_white group-hover:text-red-500' />}
      isLoading={isPending}
      onClick={handleClick}
      className={`ms-auto size-fit bg-transparent p-0 pb-1 shadow-none hover:bg-transparent ${className}`}
      loaderClassName="text-black_white"
    />
  );
};

/**
 * Restore a comment.
 */
export const SmallRestoreCommentButton = ({
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
  }, [onSuccess, onFailure, recipeId, targetId, mutate, queryClient]);

  return (
    <DataTableButton
      title={title}
      icon={<RotateCw className='text-black_white group-hover:text-green-500' />}
      isLoading={isPending}
      onClick={handleClick}
      className={`ms-auto size-fit bg-transparent p-0 pb-1 shadow-none hover:bg-transparent ${className}`}
      loaderClassName="text-black_white"
    />
  );
};
