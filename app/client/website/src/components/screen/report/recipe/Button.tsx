"use client";

import {
  useDisableRecipe,
  useMarkAllReport,
  useMarkReportAsCompleted,
  useReopenReport,
  useRestoreRecipe
} from "@/api/recipe";
import InteractiveButton, { DataTableButton } from "@/components/shared/common/Button";
import { BanIcon } from "@/components/shared/icons";
import { ReportType } from "@/constants/reports";
import { CommentDataTableButtonProps, DataTableButtonProps } from "@/types/report";
import { useQueryClient } from "@tanstack/react-query";
import { Check, Loader2, RotateCw, Search, Trash } from "lucide-react";
import { Link, useRouter } from "@/i18n/navigation";
import { useCallback, useMemo, useState } from "react";
import { toast } from "react-toastify";
import { useDisableComment, useRestoreComment } from "@/api/comment";
import { useInvalidateAdmin } from "@/hooks/query";
import { useTranslations } from "next-intl";

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
 * Reopen all reports.
 */
export const ReopenAllReportsButton = ({
  title,
  targetId,
  onSuccess,
  onFailure,
  className,
  ...props
}: DataTableButtonProps) => {
  const { mutate, isPending } = useMarkAllReport();
  const queryClient = useQueryClient();
  const { invalidateCurrentAdminActivities } = useInvalidateAdmin();
  const t = useTranslations("report");

  const handleClick = useCallback(() => {
    mutate(
      {
        recipeId: targetId,
        isReopened: true
      },
      {
        onSuccess: async () => {
          toast.success(t("reopenAllSuccess"));
          await queryClient.invalidateQueries({ queryKey: ["recipeReport", targetId] });
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
      onClick={handleClick}
      className={`bg-green-400 hover:bg-green-500 ${className}`}
      {...props}
    />
  );
};

/**
 * Reopen a report.
 */
export const ReopenReportButton = ({
  title,
  targetId,
  recipeId,
  onSuccess,
  onFailure,
  className
}: DataTableButtonProps) => {
  const { mutate, isPending } = useReopenReport();
  const queryClient = useQueryClient();
  const { invalidateCurrentAdminActivities } = useInvalidateAdmin();
  const t = useTranslations("report");

  const handleClick = useCallback(() => {
    mutate(
      {
        reportId: targetId,
        reportType: ReportType.RECIPE
      },
      {
        onSuccess: async () => {
          toast.success(t("reopenSuccess"));
          await queryClient.invalidateQueries({ queryKey: ["recipeReport", recipeId] });
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
      onClick={handleClick}
      className={`bg-green-400 hover:bg-green-500 ${className}`}
    />
  );
};

/**
 * Restore a recipe.
 */
export const RestoreRecipeButton = ({
  targetId,
  onSuccess,
  onFailure,
  className
}: DataTableButtonProps) => {
  const { mutate, isPending } = useRestoreRecipe();
  const queryClient = useQueryClient();
  const { invalidateCurrentAdminActivities } = useInvalidateAdmin();
  const t = useTranslations("administerReportRecipes.notifications");
  const tButton = useTranslations("administerReportRecipes.actions");

  const handleClick = useCallback(async () => {
    mutate(targetId, {
      onSuccess: async () => {
        toast.success(t("restoreRecipeSuccess"));
        await queryClient.invalidateQueries({ queryKey: ["recipe", targetId] });
        invalidateCurrentAdminActivities();
        onSuccess && onSuccess();
      },
      onError: ({ message }) => {
        toast.error(message);
        onFailure && onFailure();
      }
    });
  }, [
    t,
    onSuccess,
    onFailure,
    targetId,
    mutate,
    queryClient,
    invalidateCurrentAdminActivities
  ]);

  return (
    <InteractiveButton
      title={tButton("restore")}
      icon={<RotateCw className='text-white_black' />}
      isLoading={isPending}
      onClick={handleClick}
      className={`bg-green-400 hover:bg-green-500 ${className}`}
    />
  );
};

/**
 * Mark all reports as completed.
 */
export const MarkAllReportsAsCompletedButton = ({
  title,
  targetId,
  onSuccess,
  onFailure,
  className,
  ...props
}: DataTableButtonProps) => {
  const { mutate, isPending } = useMarkAllReport();
  const queryClient = useQueryClient();
  const { invalidateCurrentAdminActivities } = useInvalidateAdmin();
  const t = useTranslations("report");

  const handleClick = useCallback(async () => {
    mutate(
      { recipeId: targetId, isReopened: false },
      {
        onSuccess: async () => {
          toast.success(t("resolveAllSuccess"));
          await queryClient.invalidateQueries({ queryKey: ["recipeReport", targetId] });
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
    mutate,
    queryClient,
    invalidateCurrentAdminActivities
  ]);

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
 * Mark a report as completed.
 */
export const MarkAsCompletedButton = ({
  title,
  targetId,
  recipeId,
  onSuccess,
  onFailure,
  className
}: DataTableButtonProps) => {
  const { mutate, isPending } = useMarkReportAsCompleted();
  const queryClient = useQueryClient();
  const { invalidateCurrentAdminActivities } = useInvalidateAdmin();
  const t = useTranslations("report");

  const handleClick = useCallback(async () => {
    mutate(
      { reportId: targetId, reportType: ReportType.RECIPE },
      {
        onSuccess: async () => {
          toast.success(t("resolveSuccess"));
          await queryClient.invalidateQueries({ queryKey: ["recipeReport", recipeId] });
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
      onClick={handleClick}
      className={`bg-purple-400 hover:bg-purple-500 ${className}`}
    />
  );
};

/**
 * Disable a recipe.
 */
export const DisableRecipeButton = ({
  targetId,
  onSuccess,
  onFailure,
  className
}: DataTableButtonProps) => {
  const { mutate, isPending } = useDisableRecipe();
  const queryClient = useQueryClient();
  const { invalidateCurrentAdminActivities } = useInvalidateAdmin();
  const t = useTranslations("administerReportRecipes.notifications");
  const tButton = useTranslations("administerReportRecipes.actions");

  const handleClick = useCallback(async () => {
    mutate(targetId, {
      onSuccess: async () => {
        toast.success(t("disableRecipeSuccess"));
        await queryClient.invalidateQueries({ queryKey: ["recipe", targetId] });
        invalidateCurrentAdminActivities();
        onSuccess && onSuccess();
      },
      onError: ({ message }) => {
        toast.error(message);
        onFailure && onFailure();
      }
    });
  }, [
    t,
    onSuccess,
    onFailure,
    targetId,
    mutate,
    queryClient,
    invalidateCurrentAdminActivities
  ]);

  return (
    <InteractiveButton
      title={tButton("disable")}
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
  const { invalidateCurrentAdminActivities } = useInvalidateAdmin();
  const t = useTranslations("administerReportComments.notifications");
  const tButton = useTranslations("administerReportComments.actions");

  const handleClick = useCallback(async () => {
    mutate(
      { recipeId, commentId: targetId },
      {
        onSuccess: async () => {
          onSuccess && onSuccess();
          toast.success(t("disableSuccess"));
          await queryClient.invalidateQueries({ queryKey: ["comment", targetId] });
          await queryClient.invalidateQueries({ queryKey: ["recipeComments", recipeId] });
          invalidateCurrentAdminActivities();
        },
        onError: ({ message }) => {
          onFailure && onFailure();
          toast.error(message);
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
    <DataTableButton
      title={tButton("disable")}
      icon={<Trash className='text-black_white group-hover:text-red-500' />}
      isLoading={isPending}
      onClick={handleClick}
      loader={<Loader2 className={`text-black_white animate-spin`} />}
      className={`ms-auto size-fit bg-transparent p-0 pb-1 shadow-none hover:bg-transparent ${className}`}
      loaderClassName='text-black'
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
  const { invalidateCurrentAdminActivities } = useInvalidateAdmin();
  const t = useTranslations("administerReportComments.notifications");
  const tButton = useTranslations("administerReportComments.actions");

  const handleClick = useCallback(async () => {
    mutate(
      { recipeId, commentId: targetId },
      {
        onSuccess: async () => {
          onSuccess && onSuccess();
          toast.success(t("restoreSuccess"));
          await queryClient.invalidateQueries({ queryKey: ["comment", targetId] });
          await queryClient.invalidateQueries({ queryKey: ["recipeComments", recipeId] });
          invalidateCurrentAdminActivities();
        },
        onError: ({ message }) => {
          onFailure && onFailure();
          toast.error(message);
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
    <DataTableButton
      title={tButton("restore")}
      icon={<RotateCw className='text-black_white group-hover:text-green-500' />}
      isLoading={isPending}
      loader={<Loader2 className={`text-black_white animate-spin`} />}
      onClick={handleClick}
      className={`ms-auto size-fit bg-transparent p-0 pb-1 shadow-none hover:bg-transparent ${className}`}
    />
  );
};
