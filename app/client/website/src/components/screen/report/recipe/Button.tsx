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
import { DataTableButtonProps } from "@/types/report";
import { useQueryClient } from "@tanstack/react-query";
import { Check, RotateCw, Search } from "lucide-react";
import { useRouter } from "@/i18n/navigation";
import { useCallback, useState } from "react";
import { toast } from "react-toastify";

export const ViewDetailButton = ({
  title,
  onSuccess,
  onFailure,
  targetId,
  className
}: DataTableButtonProps) => {
  const router = useRouter();
  const [isLoading, setIsLoading] = useState(false);

  const handleClick = useCallback(() => {
    setIsLoading(true);

    try {
      router.push(`/reports/recipes/detail/${targetId}`);
      onSuccess && onSuccess();
    } catch (error) {
      onFailure && onFailure();
      console.log(error);
    } finally {
      setIsLoading(false);
    }
  }, [onSuccess, onFailure, targetId, router]);

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
