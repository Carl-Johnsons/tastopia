"use client";

import Empty from "@/components/shared/common/Empty";
import { IAdminActivityLogResponse } from "@/generated/interfaces/tracking.interface";
import { ActivityItem } from "./ActivityItem";
import { useTranslations } from "next-intl";
import { useGetAdminActivities } from "@/api/admin";
import Loader from "@/components/ui/Loader";
import { Button } from "@/components/ui/button";

export type ActivityItemType = IAdminActivityLogResponse;

type ActivityFeedProps = {
  accountId: string;
  isLoading?: boolean;
};

export default function ActivityFeed({ accountId }: ActivityFeedProps) {
  const t = useTranslations("administerAdmins.detail.activity");
  const { data, isFetching, isLoading, hasNextPage, fetchNextPage } =
    useGetAdminActivities(accountId);

  return (
    <div className='bg-white_black100 rounded-xl border border-gray-200 p-6 shadow-sm dark:border-gray-600'>
      <h2 className='h3-semibold text-black_white mb-6'>{t("title")}</h2>

      <div className='space-y-6'>
        {isFetching || isLoading || !data?.pages ? (
          <Loader />
        ) : (
          !!data.pages[0].paginatedData.length || <Empty />
        )}
        {data?.pages.map(page => {
          return page.paginatedData.map((activity: ActivityItemType, index: number) => {
            return (
              <ActivityItem
                key={activity.entityId + index}
                activity={activity}
              />
            );
          });
        })}

        {hasNextPage && (
          <div className='flex-center'>
            <Button
              onClick={() => fetchNextPage()}
              className='w-fit rounded-full'
            >
              <span className='text-white_black'>{t("loadMore")}</span>
            </Button>
          </div>
        )}
      </div>
    </div>
  );
}
