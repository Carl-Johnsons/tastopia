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
import useWindowDimensions from "@/hooks/useWindowDimensions";
import { cn } from "@/lib/utils";
import { useQueryClient } from "@tanstack/react-query";
import { cva } from "class-variance-authority";
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
    />
  );
};

const buttonVariants = cva(
  "inline-flex items-center justify-center gap-2 whitespace-nowrap rounded-md text-sm font-medium transition-colors focus-visible:outline-none focus-visible:ring-1 focus-visible:ring-ring disabled:pointer-events-none disabled:opacity-50 [&_svg]:pointer-events-none [&_svg]:size-4 [&_svg]:shrink-0",
  {
    variants: {
      variant: {
        default: "bg-primary text-primary-foreground shadow hover:bg-primary/90",
        destructive:
          "bg-destructive text-destructive-foreground shadow-sm hover:bg-destructive/90",
        outline:
          "border border-input bg-background shadow-sm hover:bg-accent hover:text-accent-foreground",
        secondary:
          "bg-secondary text-secondary-foreground shadow-sm hover:bg-secondary/80",
        ghost: "hover:bg-accent hover:text-accent-foreground",
        link: "text-primary underline-offset-4 hover:underline"
      },
      size: {
        default: "h-9 px-4 py-2",
        sm: "h-8 rounded-md px-3 text-xs",
        lg: "h-10 rounded-md px-8",
        icon: "h-9 w-9"
      }
    },
    defaultVariants: {
      variant: "default",
      size: "default"
    }
  }
);

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
  ...props
}: InteractiveButtonProps) => {
  const { width } = useWindowDimensions();
  const toolTip = useMemo(() => width < 768 || props.toolTip, [width]);
  const RenderedContent = useMemo(
    () => (
      <div className={`relative flex items-center gap-1.5`}>
        {isLoading ? <LoadingIcon className='text-white_black' /> : icon}

        {!noText && (
          <span
            className={`${!noTruncateText && "hidden md:inline"} text-white_black text-sm font-medium`}
          >
            {title}
          </span>
        )}
      </div>
    ),
    [icon, isLoading, title, className, noTruncateText, noText]
  );

  return toolTip ? (
    <TooltipProvider delayDuration={500}>
      <Tooltip>
        <TooltipTrigger
          onClick={onClick}
          className={cn(
            buttonVariants({ variant: "default", size: "default", className })
          )}
        >
          {RenderedContent}
        </TooltipTrigger>
        <TooltipContent className='rounded-md bg-gray-900'>
          <div className='text-sm text-white'>
            <span>{title}</span>
          </div>
        </TooltipContent>
      </Tooltip>
    </TooltipProvider>
  ) : (
    <Button
      className={className}
      onClick={onClick}
    >
      {RenderedContent}
    </Button>
  );
};

export default InteractiveButton;
