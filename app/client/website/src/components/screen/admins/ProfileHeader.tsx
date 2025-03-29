"use client";

import { useMemo, useState } from "react";
import { useTranslations } from "next-intl";
import { DisableAdminButton, RestoreAdminButton, SignOutButton } from "./Button";
import { ItemStatusText } from "../report/common/StatusText";
import { IAdminDetailResponse } from "@/generated/interfaces/user.interface";
import { Roles } from "@/constants/role";
import Avatar from "@/components/shared/common/Avatar";
import { useSelectUserId } from "@/slices/user.slice";

type Props = {
  admin: IAdminDetailResponse;
};

export default function ProfileHeader({ admin }: Props) {
  const [isActive, setIsActive] = useState(admin.isActive);
  const { avatarUrl, username, accountId } = admin;
  const role = Roles.ADMIN;

  const tRole = useTranslations("administerAdmins.detail.header.roles");
  const tTooltip = useTranslations("administerAdmins.tooltip");

  const userId = useSelectUserId();
  const isCurrentUser = useMemo(
    () => userId === accountId,
    [userId, accountId]
  );

  return (
    <div className='bg-white_black100 overflow-hidden rounded-xl border border-gray-200 shadow-sm dark:border-gray-600'>
      <div className='flex flex-col items-center justify-between gap-4 p-6 sm:flex-row'>
        <div className='flex-center flex-col gap-5 sm:flex-row'>
          <Avatar
            src={avatarUrl}
            alt={username}
            className='relative size-24'
          />

          <div>
            <h1 className='h3-semibold text-black_white text-center sm:text-left'>
              {username}
            </h1>
            <div className='flex flex-col items-center gap-x-2 sm:flex-row'>
              <p className='text-center text-gray-600 dark:text-gray-500'>
                {tRole(role)}
              </p>
              <ItemStatusText
                isActive={isActive}
                coloring
              />
            </div>
          </div>
        </div>

        {isCurrentUser ? (
          <SignOutButton />
        ) : isActive ? (
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
