import Avatar from "@/components/shared/common/Avatar";
import Image from "@/components/shared/common/Image";
import {
  ICommentLogResponse,
  IRecipeLogResponse,
  IUserLogResponse
} from "@/generated/interfaces/tracking.interface";
import { Link } from "@/i18n/navigation";
import { formatDistanceToNow } from "date-fns";
import { enUS, vi } from "date-fns/locale";
import { useLocale, useTranslations } from "next-intl";

export const RecipeCard = ({ recipe }: { recipe: IRecipeLogResponse }) => {
  const { title, authorDisplayName, imageURL } = recipe;
  const locale = useLocale();
  const t = useTranslations("administerAdmins.detail.activity");
  const createdAt = formatDistanceToNow(new Date(recipe.createdAt as string), {
    locale: locale === "en" ? enUS : vi
  });

  return (
    <div className='mt-4 rounded-lg border border-gray-200 p-4'>
      <div className='text-black_white text-lg font-medium'>{title}</div>
      <div className='mt-1 text-sm text-gray-500'>
        {authorDisplayName} · {`${createdAt} ${t("ago")}`}
      </div>

      <div className='mt-3 h-[300px] overflow-hidden rounded-lg'>
        <Image
          src={imageURL ?? ""}
          alt={"Recipe's image"}
          className='object-cover'
          fill
        />
      </div>
    </div>
  );
};

export const CommentCard = ({ comment }: { comment: ICommentLogResponse }) => {
  const { content, authorDisplayName, authorId, authorAvatarURL } = comment;
  const locale = useLocale();
  const t = useTranslations("administerAdmins.detail.activity");
  const createdAt = formatDistanceToNow(new Date(comment.createdAt as string), {
    locale: locale === "en" ? enUS : vi
  });

  return (
    <div className='mt-4 rounded-lg border border-gray-200 p-4'>
      <div className={`grid grid-cols-[auto_1fr] items-center gap-x-2`}>
        <Link href={`/users/${authorId}`}>
          <Avatar
            src={authorAvatarURL}
            alt={authorDisplayName}
          />
        </Link>

        <div className='flex flex-col'>
          <div className='text-black_white flex items-center gap-1.5'>
            <Link
              href={`/users/${authorId}`}
              className={`flex items-center gap-1.5`}
            >
              <span className='font-bold'>{authorDisplayName}</span>
            </Link>
            <span className={`text-sm text-gray-700 opacity-50 dark:opacity-80`}>
              {`${createdAt} ${t("ago")}`}
            </span>
          </div>
        </div>

        <p className='text-black_white col-span-2 max-w-[70em]'>{content}</p>
      </div>
    </div>
  );
};

export const UserCard = ({ user }: { user: IUserLogResponse }) => {
  const { displayName, username, avatarURL, id } = user;

  return (
    <div className='mt-4 rounded-lg border border-gray-200 p-4'>
      <div className='flex-center flex-col gap-2'>
        <Link href={`/users/${id}`}>
          <Avatar
            src={avatarURL}
            alt={displayName}
          />
        </Link>

        <Link href={`/users/${id}`}>
          <div className='text-black_white flex flex-col items-center'>
            <span className='font-bold'>{displayName}</span>
            <span className='text-gray-500'>@{username}</span>
          </div>
        </Link>
      </div>
    </div>
  );
};
