"use client";

import { useEffect, useMemo, useState } from "react";
import { useTranslations } from "next-intl";
import { DisableAdminButton, RestoreAdminButton, SignOutButton } from "./Button";
import { ItemStatusText } from "../report/common/StatusText";
import { IAdminDetailResponse } from "@/generated/interfaces/user.interface";
import Avatar from "@/components/shared/common/Avatar";
import { useSelectUserId } from "@/slices/user.slice";
import { useSelectRole } from "@/slices/auth.slice";
import { Roles } from "@/constants/role";

type Props = {
  admin: IAdminDetailResponse;
  isViewingAdmin?: boolean;
};

export default function ProfileHeader({ admin, isViewingAdmin }: Props) {
  const [isActive, setIsActive] = useState(admin.isActive);
  const { avatarUrl, username, accountId } = admin;
  const currentUserRole = useSelectRole();
  const role = useMemo(
    () => (isViewingAdmin ? Roles.ADMIN : currentUserRole),
    [isViewingAdmin, currentUserRole]
  );

  const tRole = useTranslations("administerAdmins.detail.header.roles");
  const tTooltip = useTranslations("administerAdmins.tooltip");

  const userId = useSelectUserId();
  const isCurrentUser = useMemo(() => userId === accountId, [userId, accountId]);

  useEffect(() => {
    const newStatus = admin.isActive;

    if (newStatus !== isActive) {
      setIsActive(newStatus);
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [admin.isActive]);

  return (
    <div className='bg-white_black100 overflow-hidden rounded-xl border border-gray-200 shadow-sm dark:border-gray-600'>
      <div className='flex flex-col items-center justify-between gap-4 p-6 lg:flex-row'>
        <div className='flex-center flex-col gap-5 sm:flex-row'>
          <Avatar
            src={avatarUrl}
            alt={username}
            className='relative size-24'
          />

          <div>
            <h1 className='h3-semibold text-black_white text-left'>{username}</h1>
            <div className='flex flex-col items-center gap-x-2 lg:flex-row'>
              <p className='text-center text-gray-600 dark:text-gray-500'>
                {tRole(role as Roles)}
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
