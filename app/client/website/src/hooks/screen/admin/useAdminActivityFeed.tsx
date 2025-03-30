import {
  CommentCard,
  CommentReportCard,
  RecipeCard,
  RecipeReportCard,
  TagCard,
  UserCard,
  UserReportCard
} from "@/components/screen/admins/ActivityCard";
import { ActivityItemType } from "@/components/screen/admins/ActivityFeed";
import { UpvoteIcon } from "@/components/shared/icons";
import { ActivityEntityType, ActivityType } from "@/constants/admin";
import {
  ICommentAdminActivityLogResponse,
  ICommentReportAdminActivityLogResponse,
  IRecipeAdminActivityLogResponse,
  IRecipeReportAdminActivityLogResponse,
  ITagAdminActivityLogResponse,
  IUserAdminActivityLogResponse,
  IUserReportAdminActivityLogResponse
} from "@/generated/interfaces/tracking.interface";
import { BanIcon, Check, Info, Plus, RotateCcw } from "lucide-react";
import { useTranslations } from "next-intl";
import { useCallback } from "react";

export const useAdminActivityFeed = () => {
  const tT = useTranslations("administerAdmins.detail.activity.types");
  const tE = useTranslations("administerAdmins.detail.activity.entity");

  const getActivityTitle = useCallback(
    (activityType: ActivityType) => {
      switch (activityType) {
        case ActivityType.CREATE:
          return tT("create");
        case ActivityType.UPDATE:
          return tT("update");
        case ActivityType.DISABLE:
          return tT("disable");
        case ActivityType.RESTORE:
          return tT("restore");
        case ActivityType.MARK_COMPLETE:
          return tT("markComplete");
        case ActivityType.REOPEN:
          return tT("reopen");
        default:
          console.error("Invalid activity type", activityType);
          return "Activity";
      }
    },
    [tT]
  );

  const getEntityTitle = useCallback(
    (entityType: ActivityEntityType) => {
      switch (entityType) {
        case ActivityEntityType.RECIPE:
          return tE("recipe");
        case ActivityEntityType.COMMENT:
          return tE("comment");
        case ActivityEntityType.TAG:
          return tE("tag");
        case ActivityEntityType.USER:
          return tE("user");
        case ActivityEntityType.REPORT_USER:
          return tE("reportUser");
        case ActivityEntityType.REPORT_RECIPE:
          return tE("reportRecipe");
        case ActivityEntityType.REPORT_COMMENT:
          return tE("reportComment");
        default:
          console.error("Invalid entity type", entityType);
          return "Entity";
      }
    },
    [tE]
  );

  const getEntityCard = useCallback((activity: ActivityItemType) => {
    switch (activity.entityType) {
      case ActivityEntityType.RECIPE:
        return (
          <RecipeCard recipe={(activity as IRecipeAdminActivityLogResponse).recipe} />
        );
      case ActivityEntityType.COMMENT:
        return (
          <CommentCard comment={(activity as ICommentAdminActivityLogResponse).comment} />
        );
      case ActivityEntityType.USER:
        return <UserCard user={(activity as IUserAdminActivityLogResponse).user} />;
      case ActivityEntityType.TAG:
        return <TagCard tag={(activity as ITagAdminActivityLogResponse).tag} />;
      case ActivityEntityType.REPORT_RECIPE:
        return (
          <RecipeReportCard report={activity as IRecipeReportAdminActivityLogResponse} />
        );
      case ActivityEntityType.REPORT_COMMENT:
        return (
          <CommentReportCard
            report={activity as ICommentReportAdminActivityLogResponse}
          />
        );
      case ActivityEntityType.REPORT_USER:
        return (
          <UserReportCard report={activity as IUserReportAdminActivityLogResponse} />
        );
      default:
        throw new Error("Invalid activity type");
    }
  }, []);

  const getActivityIcon = useCallback((type: ActivityItemType["activityType"]) => {
    switch (type) {
      case ActivityType.CREATE:
        return (
          <div className='flex size-8 items-center justify-center rounded-full bg-green-100 text-green-600'>
            <Plus />
          </div>
        );
      case ActivityType.UPDATE:
        return (
          <div className='flex size-8 items-center justify-center rounded-full bg-green-100 text-green-600'>
            <UpvoteIcon />
          </div>
        );
      case ActivityType.DISABLE:
        return (
          <div className='flex size-8 items-center justify-center rounded-full bg-red-100 text-red-600'>
            <BanIcon />
          </div>
        );
      case ActivityType.RESTORE:
        return (
          <div className='flex size-8 items-center justify-center rounded-full bg-green-100 text-green-600'>
            <RotateCcw />
          </div>
        );
      case ActivityType.MARK_COMPLETE:
        return (
          <div className='flex size-8 items-center justify-center rounded-full bg-purple-100 text-purple-600'>
            <Check />
          </div>
        );
      case ActivityType.REOPEN:
        return (
          <div className='flex size-8 items-center justify-center rounded-full bg-green-100 text-green-600'>
            <RotateCcw />
          </div>
        );
      default:
        return (
          <div className='flex size-8 items-center justify-center rounded-full bg-red-100 text-red-600'>
            <Info />
          </div>
        );
    }
  }, []);

  const getBgColor = useCallback((type: ActivityType) => {
    switch (type) {
      case ActivityType.CREATE:
      case ActivityType.RESTORE:
      case ActivityType.REOPEN:
        return "bg-green-50 dark:bg-black-400 border-green-200";
      case ActivityType.UPDATE:
        return "bg-blue-50 dark:bg-black-400 border-blue-200";
      case ActivityType.DISABLE:
        return "bg-red-50 dark:bg-black-400 border-red-200";
      case ActivityType.MARK_COMPLETE:
        return "bg-purple-50 dark:bg-black-400 border-purple-200";
      default:
        return "bg-gray-50 dark:bg-black-400 border-gray-200";
    }
  }, []);

  return { getActivityIcon, getActivityTitle, getEntityTitle, getBgColor, getEntityCard };
};
