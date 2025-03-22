import Image from "next/image";
import { ActivityItem } from "@/types/user";
import {
  BanIcon,
  CommentIcon,
  DefaultIcon,
  DownvoteIcon,
  RecipeIcon,
  UpvoteIcon
} from "@/components/shared/icons";
import { ActivityType } from "@/constants/activities";
import Empty from "@/components/shared/common/Empty";
import { useLocale, useTranslations } from "next-intl";
import { formatDistanceToNow } from "date-fns";
import { enUS, vi } from "date-fns/locale";

type ActivityFeedProps = {
  activities: ActivityItem[];
  isLoading?: boolean;
};

export default function ActivityFeed({ activities }: ActivityFeedProps) {
  const currentLanguage = useLocale();
  const t = useTranslations("userDetail.activity");

  const getActivityIcon = (type: ActivityItem["type"]) => {
    switch (type) {
      case ActivityType.CreateRecipe:
        return (
          <div className='flex size-8 items-center justify-center rounded-full bg-green-100 text-green-600'>
            <RecipeIcon />
          </div>
        );
      case ActivityType.UpvoteRecipe:
        return (
          <div className='flex size-8 items-center justify-center rounded-full bg-green-100 text-green-600'>
            <UpvoteIcon />
          </div>
        );
      case ActivityType.Ban:
        return (
          <div className='flex size-8 items-center justify-center rounded-full bg-red-100 text-red-600'>
            <BanIcon />
          </div>
        );
      case ActivityType.DownvoteRecipe:
        return (
          <div className='flex size-8 items-center justify-center rounded-full bg-red-100 text-red-600'>
            <DownvoteIcon />
          </div>
        );
      case ActivityType.CommentRecipe:
        return (
          <div className='flex size-8 items-center justify-center rounded-full bg-blue-100 text-blue-600'>
            <CommentIcon />
          </div>
        );
      default:
        return (
          <div className='flex size-8 items-center justify-center rounded-full bg-gray-100 text-gray-600'>
            <DefaultIcon />
          </div>
        );
    }
  };

  const getBgColor = (type: ActivityType) => {
    switch (type) {
      case ActivityType.CreateRecipe:
      case ActivityType.UpvoteRecipe:
        return "bg-green-50 dark:bg-black-400 border-green-200";
      case ActivityType.Ban:
      case ActivityType.DownvoteRecipe:
        return "bg-red-50 dark:bg-black-400 border-red-200";
      case ActivityType.CommentRecipe:
        return "bg-blue-50 dark:bg-black-400 border-blue-200";
      default:
        return "bg-gray-50 dark:bg-black-400 border-gray-200";
    }
  };

  return (
    <div className='bg-white_black100 rounded-xl border border-gray-200 p-6 shadow-sm dark:border-gray-600'>
      <h2 className='h3-semibold text-black_white mb-6'>{t("title")}</h2>

      <div className='space-y-6'>
        {!activities.length && <Empty />}
        {activities.map((activity: ActivityItem, index: number) => (
          <div
            key={index}
            className='flex gap-4'
          >
            <div className='shrink-0'>
              <div className='size-10 overflow-hidden rounded-full bg-orange-100'>
                <Image
                  src={activity.avtImageUrl}
                  alt={activity.userName || "User"}
                  width={40}
                  height={40}
                  className='size-full object-cover'
                />
              </div>
            </div>

            <div className='flex-1'>
              <div className='flex flex-wrap items-center gap-4'>
                <h3 className='base-semibold text-black_white'>
                  {activity.type === ActivityType.Ban ? "System" : activity.username}
                </h3>
                <div className='flex items-center gap-2 text-sm text-gray-500'>
                  <svg
                    xmlns='http://www.w3.org/2000/svg'
                    className='size-4'
                    viewBox='0 0 20 20'
                    fill='currentColor'
                  >
                    <path
                      fillRule='evenodd'
                      d='M10 18a8 8 0 100-16 8 8 0 000 16zm1-12a1 1 0 10-2 0v4a1 1 0 00.293.707l2.828 2.829a1 1 0 101.415-1.415L11 9.586V6z'
                      clipRule='evenodd'
                    />
                  </svg>
                  <span className='text-sm text-gray-500'>{activity.timeAgo}</span>
                </div>
              </div>

              <div className={`mt-2 rounded-lg border p-4 ${getBgColor(activity.type)}`}>
                <div className='mb-3 flex items-center gap-2'>
                  {getActivityIcon(activity.type)}
                  <h4 className='text-black_white font-medium'>
                    {(() => {
                      switch (activity.type) {
                        case ActivityType.CreateRecipe:
                          return t("types.createRecipe");
                        case ActivityType.Ban:
                          return t("types.disableUser");
                        case ActivityType.CommentRecipe:
                          return t("types.commentRecipe");
                        case ActivityType.UpvoteRecipe:
                          return t("types.upvoteRecipe");
                        case ActivityType.DownvoteRecipe:
                          return t("types.downvoteRecipe");
                        default:
                          return "Activity";
                      }
                    })()}
                  </h4>
                </div>

                <h3 className='text-black_white mb-1 text-lg font-medium'>
                  {activity.description}
                </h3>

                <div className='mt-3 whitespace-pre-line text-gray-700'>
                  {activity.content}
                </div>

                <div className='mt-4 rounded-lg border border-gray-200 p-4'>
                  <div className='text-black_white text-lg font-medium'>
                    {activity.recipeTitle}
                  </div>
                  <div className='mt-1 text-sm text-gray-500'>
                    @{activity.recipeAuthorUsername} · {activity.recipeTimeAgo}
                  </div>

                  <div className='mt-3 overflow-hidden rounded-lg'>
                    <Image
                      src={activity.recipeImageUrl ?? "/assets/images/pizza.jpg"}
                      alt={"activity comment"}
                      width={600}
                      height={300}
                      className='h-auto w-full object-cover'
                    />
                  </div>
                </div>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
