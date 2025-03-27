"use client";

import Image from "next/image";
import { useState } from "react";
import { useTranslations } from "next-intl";
import { IAdminGetAdminDetailResponse } from "@/types/admin";
import { DisableAdminButton, RestoreAdminButton } from "./Button";
import { ItemStatusText } from "../report/common/StatusText";

type Props = {
  admin: IAdminGetAdminDetailResponse;
};

export default function ProfileHeader({ admin }: Props) {
  const { accountId, role, avatarUrl, username } = admin;
  const [isActive, setIsActive] = useState(admin.isActive);
  const tRole = useTranslations("administerAdmins.detail.header.roles");
  const tTooltip = useTranslations("administerAdmins.tooltip");

  return (
    <div className='bg-white_black100 overflow-hidden rounded-xl border border-gray-200 shadow-sm dark:border-gray-600'>
      <div className='flex flex-col items-center justify-between gap-4 p-6 sm:flex-row'>
        <div className='flex flex-col items-center gap-5 sm:flex-row'>
          <div className='relative size-24 overflow-hidden rounded-full bg-orange-100'>
            {avatarUrl ? (
              <Image
                src={avatarUrl}
                alt={`${username}'s avatar`}
                fill
                className='object-cover'
              />
            ) : (
              <div className='flex size-full items-center justify-center bg-orange-200 text-3xl font-bold text-orange-600'>
                {username.charAt(0)}
              </div>
            )}
          </div>

          <div>
            <h1 className='h3-semibold text-black_white'>{username}</h1>
            <div className='flex flex-col items-center gap-x-2 sm:flex-row'>
              <p className='text-gray-600 dark:text-gray-500'>{tRole(role)}</p>
              <ItemStatusText
                isActive={isActive}
                coloring
              />
            </div>
          </div>
        </div>

        {isActive ? (
          <DisableAdminButton
            title={tTooltip("disable")}
            targetId={accountId}
            onSuccess={() => {
              setIsActive(false);
            }}
          />
        ) : (
          <RestoreAdminButton
            title={tTooltip("restore")}
            targetId={accountId}
            onSuccess={() => {
              setIsActive(true);
            }}
          />
        )}
      </div>
    </div>
  );
}
