"use client";

import { useDisableRecipe, useRestoreRecipe } from "@/api/recipe";
import InteractiveButton, { DataTableButton } from "@/components/shared/common/Button";
import { BanIcon } from "@/components/shared/icons";
import { useErrorHandler } from "@/hooks/error/useErrorHanler";
import { useInvalidateAdmin } from "@/hooks/query";
import { Link, useRouter } from "@/i18n/navigation";
import { DataTableButtonProps } from "@/types/report";
import { useQueryClient } from "@tanstack/react-query";
import { RotateCw, Search } from "lucide-react";
import { useTranslations } from "next-intl";
import { useCallback, useMemo, useState } from "react";
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
  const url = useMemo(() => `/recipes/${targetId}`, [targetId]);

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
 * Restore a recipe.
 */
export const RestoreRecipeButton = ({
  title,
  targetId,
  onSuccess,
  onFailure,
  className,
  ...props
}: DataTableButtonProps) => {
  const { mutate, isPending } = useRestoreRecipe();
  const queryClient = useQueryClient();
  const { invalidateCurrentAdminActivities } = useInvalidateAdmin();
  const t = useTranslations("administerReportRecipes.notifications");
  const { handleError } = useErrorHandler();

  const handleClick = useCallback(async () => {
    mutate(targetId, {
      onSuccess: async () => {
        toast.success(t("restoreRecipeSuccess"));
        await queryClient.invalidateQueries({ queryKey: ["recipe", targetId] });
        invalidateCurrentAdminActivities();
        onSuccess && onSuccess();
      },
      onError: error => {
        handleError(error);
        onFailure && onFailure();
      }
    });
  }, [
    t,
    handleError,
    onSuccess,
    onFailure,
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
      {...props}
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
  className,
  ...props
}: DataTableButtonProps) => {
  const { mutate, isPending } = useDisableRecipe();
  const queryClient = useQueryClient();
  const { invalidateCurrentAdminActivities } = useInvalidateAdmin();
  const t = useTranslations("administerReportRecipes.notifications");
  const { handleError } = useErrorHandler();

  const handleClick = useCallback(async () => {
    mutate(targetId, {
      onSuccess: async () => {
        toast.success(t("disableRecipeSuccess"));
        await queryClient.invalidateQueries({ queryKey: ["recipe", targetId] });
        invalidateCurrentAdminActivities();
        onSuccess && onSuccess();
      },
      onError: error => {
        handleError(error);
        onFailure && onFailure();
      }
    });
  }, [
    t,
    handleError,
    onSuccess,
    onFailure,
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
      {...props}
    />
  );
};
