"use client";

import { ActivityItemType } from "./ActivityFeed";
import { useLocale, useTranslations } from "next-intl";
import { formatDistanceToNow } from "date-fns";
import { useAdminActivityFeed } from "@/hooks/screen/admin";
import { enUS, vi } from "date-fns/locale";
import { Clock } from "lucide-react";
import { useMemo } from "react";
import { useGetAdminById } from "@/api/admin";
import Loader from "@/components/ui/Loader";
import Avatar from "@/components/shared/common/Avatar";

type ActivityItemProps = {
  activity: ActivityItemType;
};

export const ActivityItem = ({ activity }: ActivityItemProps) => {
  const { data: admin, isFetching, isLoading } = useGetAdminById(activity.accountId);
  const { createdAt, activityType, entityType } = activity;
  const t = useTranslations("administerAdmins.detail.activity");

  const locale = useLocale();
  const timeAgo = useMemo(
    () =>
      formatDistanceToNow(new Date(createdAt), {
        locale: locale === "en" ? enUS : vi
      }),
    [createdAt, locale]
  );

  const { getBgColor, getActivityIcon, getActivityTitle, getEntityTitle, getEntityCard } =
    useAdminActivityFeed();

  const EntityCard = useMemo(() => getEntityCard(activity), [getEntityCard, activity]);
  const title = useMemo(
    () => `${getActivityTitle(activityType)} ${getEntityTitle(entityType).toLowerCase()}`,
    [activityType, entityType, getActivityTitle, getEntityTitle]
  );

  if (isFetching || isLoading || !admin) return <Loader />;
  const { avatarUrl, username } = admin;

  return (
    <div className='flex gap-4'>
      <Avatar src={avatarUrl} alt={username}/>
      <div className='flex w-full flex-col gap-2'>
        <div className='flex flex-wrap items-center gap-4'>
          <h3 className='base-semibold text-black_white'>{username}</h3>
          <div className='flex items-center gap-2 text-sm text-gray-500'>
            <Clock className='size-4' />
            <span>{`${timeAgo} ${t("ago")}`}</span>
          </div>
        </div>

        <div
          className={`flex flex-col gap-2 rounded-lg border p-4 ${getBgColor(activityType)}`}
        >
          <div className='flex items-center gap-2'>
            {getActivityIcon(activityType)}
            <h4 className='text-black_white font-medium'>{title}</h4>
          </div>
          {EntityCard}
        </div>
      </div>
    </div>
  );
};
