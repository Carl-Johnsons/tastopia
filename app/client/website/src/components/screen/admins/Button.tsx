"use client";

import { useDisableAdmin, useRestoreAdmin } from "@/api/admin";
import InteractiveButton, { DataTableButton } from "@/components/shared/common/Button";
import { BanIcon } from "@/components/shared/icons";
import { Link, useRouter } from "@/i18n/navigation";
import { updateAdmin } from "@/slices/admin.slice";
import { useAppDispatch } from "@/store/hooks";
import { DataTableButtonProps } from "@/types/report";
import { useQueryClient } from "@tanstack/react-query";
import { Pen, RotateCw, Search } from "lucide-react";
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
  const url = useMemo(() => `/admins/${targetId}`, [targetId]);

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

export const RestoreAdminButton = ({
  title,
  targetId,
  onSuccess,
  onFailure,
  className,
  ...props
}: DataTableButtonProps) => {
  const { mutate, isPending } = useRestoreAdmin();
  const queryClient = useQueryClient();
  const t = useTranslations("administerAdmins.notifications");

  const handleClick = useCallback(async () => {
    mutate(targetId, {
      onSuccess: async () => {
        toast.success(t("restoreSuccess"));
        await queryClient.invalidateQueries({ queryKey: ["admin", targetId] });
        onSuccess && onSuccess();
      },
      onError: ({ message }) => {
        toast.error(t("error"));
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
      {...props}
    />
  );
};

export const DisableAdminButton = ({
  title,
  targetId,
  onSuccess,
  onFailure,
  className,
  ...props
}: DataTableButtonProps) => {
  const { mutate, isPending } = useDisableAdmin();
  const queryClient = useQueryClient();
  const t = useTranslations("administerAdmins.notifications");

  const handleClick = useCallback(async () => {
    mutate(targetId, {
      onSuccess: async () => {
        toast.success(t("disableSuccess"));
        await queryClient.invalidateQueries({ queryKey: ["admin", targetId] });
        onSuccess && onSuccess();
      },
      onError: ({ message }) => {
        toast.error(t("error"));
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
      {...props}
    />
  );
};

export const UpdateAdminButton = ({
  title,
  targetId,
  onSuccess,
  onFailure,
  className,
  ...props
}: DataTableButtonProps) => {
  const dispatch = useAppDispatch();

  const handleClick = useCallback(async () => {
    dispatch(updateAdmin({ targetId }));
  }, [dispatch, targetId]);

  return (
    <InteractiveButton
      title={title}
      icon={<Pen className='text-white_black' />}
      onClick={handleClick}
      className={`bg-blue-400 hover:bg-blue-500 ${className}`}
      {...props}
    />
  );
};
