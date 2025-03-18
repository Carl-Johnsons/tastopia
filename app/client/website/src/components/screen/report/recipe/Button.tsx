"use client";

import {
  useDisableRecipe,
  useMarkReportAsCompleted,
  useReopenReport,
  useRestoreRecipe
} from "@/api/recipe";
import { BanIcon, LoadingIcon } from "@/components/shared/icons";
import { Button } from "@/components/ui/button";
import {
  Tooltip,
  TooltipContent,
  TooltipProvider,
  TooltipTrigger
} from "@/components/ui/tooltip";
import { ReportType } from "@/constants/reports";
import { useQueryClient } from "@tanstack/react-query";
import { Check, RotateCw, Search } from "lucide-react";
import { useRouter } from "next/navigation";
import { ReactNode, useCallback, useMemo, useState } from "react";
import { toast } from "react-toastify";

type TableDataButtonProps = {
  title: string;
  targetId: string;
  onSuccess?: () => void;
  onFailure?: () => void;
  className?: string;
};

export const ViewDetailButton = ({
  title,
  onSuccess,
  onFailure,
  targetId,
  className
}: TableDataButtonProps) => {
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
  }, [onSuccess, onFailure, targetId]);

  return (
    <InteractiveButton
      title={title}
      icon={<Search className='text-white_black' />}
      isLoading={isLoading}
      onClick={handleClick}
      className={className}
      noText
      toolTip
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
}: TableDataButtonProps) => {
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
          await queryClient.invalidateQueries({ queryKey: ["report", targetId] });
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
    <InteractiveButton
      title={title}
      icon={<RotateCw className='text-white_black' />}
      isLoading={isPending}
      onClick={handleClick}
      className={`bg-green-400 hover:bg-green-500 ${className}`}
      noText
      toolTip
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
}: TableDataButtonProps) => {
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
  }, [onSuccess, onFailure, targetId, mutate]);

  return (
    <InteractiveButton
      title={title}
      icon={<RotateCw className='text-white_black' />}
      isLoading={isPending}
      onClick={handleClick}
      className={`bg-green-400 hover:bg-green-500 ${className}`}
      noText
      toolTip
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
}: TableDataButtonProps) => {
  const { mutate, isPending } = useMarkReportAsCompleted();
  const queryClient = useQueryClient();

  const handleClick = useCallback(async () => {
    mutate(
      { reportId: targetId, reportType: ReportType.RECIPE },
      {
        onSuccess: async () => {
          toast.success("Report marked as completed successfully.");
          await queryClient.invalidateQueries({ queryKey: ["report", targetId] });
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
    <InteractiveButton
      title={title}
      icon={<Check className='text-white_black' />}
      isLoading={isPending}
      onClick={handleClick}
      className={`bg-purple-400 hover:bg-purple-500 ${className}`}
      noText
      toolTip
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
}: TableDataButtonProps) => {
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
  }, [onSuccess, onFailure, targetId, mutate]);

  return (
    <InteractiveButton
      title={title}
      icon={<BanIcon className='text-white_black' />}
      isLoading={isPending}
      onClick={handleClick}
      className={`bg-red-400 hover:bg-red-500 ${className}`}
      noText
      toolTip
    />
  );
};

type InteractiveButtonProps = {
  icon: ReactNode;
  isLoading?: boolean;
  title: string;
  onClick?: () => void;
  className?: string;
  noTruncateText?: boolean;
  noText?: boolean;
  toolTip?: boolean;
};

export const InteractiveButton = ({
  icon,
  isLoading,
  title,
  onClick,
  className,
  noTruncateText,
  noText,
  toolTip
}: InteractiveButtonProps) => {
  const RenderedButton = useMemo(
    () => (
      <Button
        className={`relative flex items-center gap-1 ${className}`}
        onClick={onClick}
      >
        {isLoading ? <LoadingIcon /> : icon}

        {!noText && (
          <span
            className={`${!noTruncateText && "hidden 2xl:inline"} text-white_black text-sm font-medium`}
          >
            {title}
          </span>
        )}
      </Button>
    ),
    [icon, isLoading, title, onClick, className, noTruncateText, noText]
  );

  return toolTip ? (
    <TooltipProvider delayDuration={500}>
      <Tooltip>
        <TooltipTrigger className='w-fit'>{RenderedButton}</TooltipTrigger>
        <TooltipContent className='rounded-md bg-gray-900'>
          <div className='text-sm text-white'>
            <span>{title}</span>
          </div>
        </TooltipContent>
      </Tooltip>
    </TooltipProvider>
  ) : (
    RenderedButton
  );
};

export default InteractiveButton;
