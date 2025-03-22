"use client";

import Image from "next/image";
import Status from "@/components/ui/Status";
import { IAdminGetUserDetailResponse } from "@/generated/interfaces/user.interface";
import { adminBanUser } from "@/actions/user.action";
import { useState } from "react";
import { toast } from "react-toastify";
import { Button } from "@/components/ui/button";
import { Ban, RotateCcw } from "lucide-react";
import { useTranslations } from "next-intl";

type ProfileHeaderProps = {
  user: IAdminGetUserDetailResponse;
  onRestore?: (userId: string) => Promise<void>;
  isRestoring?: boolean;
};

export default function ProfileHeader({ user }: ProfileHeaderProps) {
  const t = useTranslations("userDetail");
  const [active, setActive] = useState<boolean>(user.isAccountActive);

  const handleToggleStatus = async () => {
    const result = await adminBanUser(user.accountId);
    if (result.userId) {
      const newStatus = result.isRestored;
      setActive(newStatus);

      if (result.isRestored) {
        toast.success(t("notifications.userRestored"));
      } else {
        toast.success(t("notifications.userDisabled"));
      }
    } else {
      toast.error(t("notifications.error"));
    }
  };
  return (
    <div className='bg-white_black100 overflow-hidden rounded-xl border border-gray-200 shadow-sm dark:border-gray-600'>
      <div className='flex flex-col items-center justify-between gap-4 p-6 sm:flex-row'>
        <div className='flex items-center gap-5'>
          <div className='relative size-24 overflow-hidden rounded-full bg-orange-100'>
            {user.avatarUrl ? (
              <Image
                src={user.avatarUrl}
                alt={user.accountUsername}
                fill
                className='object-cover'
              />
            ) : (
              <div className='flex size-full items-center justify-center bg-orange-200 text-3xl font-bold text-orange-600'>
                {user.accountUsername.charAt(0)}
              </div>
            )}
          </div>

          <div>
            <h1 className='h3-semibold text-black_white'>{user.accountUsername}</h1>
            <div className='flex items-center gap-2'>
              <p className='text-gray-600'>{t("header.role")}</p>
              <Status isActive={active} />
            </div>
          </div>
        </div>

        {active ? (
          <Button
            className='text-white_black rounded-full bg-red hover:bg-red/90 dark:bg-red/80'
            onClick={handleToggleStatus}
          >
            <Ban />
            <p>{t("header.actions.disable")}</p>
          </Button>
        ) : (
          <Button
            className='text-white_black rounded-full bg-green hover:bg-green/90 dark:bg-green/80'
            onClick={handleToggleStatus}
          >
            <RotateCcw />
            <p>{t("header.actions.restore")}</p>
          </Button>
        )}
      </div>
    </div>
  );
}
