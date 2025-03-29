import Avatar from "@/components/shared/common/Avatar";
import Image from "@/components/shared/common/Image";
import {
  ICommentLogResponse,
  ICommentReportAdminActivityLogResponse,
  IRecipeLogResponse,
  IRecipeReportAdminActivityLogResponse,
  ITagLogResponse,
  IUserLogResponse,
  IUserReportAdminActivityLogResponse
} from "@/generated/interfaces/tracking.interface";
import { Link } from "@/i18n/navigation";
import { useLocale, useTranslations } from "next-intl";
import useFormattedDistance from "@/hooks/format/useFormattedDistance";
import { Clock, CornerDownRight } from "lucide-react";

export const RecipeCard = ({
  recipe,
  className
}: {
  recipe: IRecipeLogResponse;
  className?: string;
}) => {
  const { title, authorUsername, authorId, imageURL, createdAt } = recipe;
  const locale = useLocale();
  const t = useTranslations("administerAdmins.detail.activity");
  const formattedDate = useFormattedDistance(createdAt as string, locale);

  return (
    <div className={`mt-4 rounded-lg border border-gray-200 p-4 ${className}`}>
      <Link href={`/recipes/${recipe.id}`}>
        <div className='text-black_white text-lg font-medium transition-colors hover:text-primary'>
          {title}
        </div>
      </Link>

      <div className='mt-1 flex flex-wrap gap-2'>
        <Link
          href={`/users/${authorId}`}
          className='group'
        >
          <span className='text-sm font-bold text-gray-700 transition-colors group-hover:text-primary dark:text-gray-400'>
            @{authorUsername}
          </span>
        </Link>

        <div className='flex items-center gap-1 text-gray-500'>
          <Clock size={16} />
          <span className='text-sm'>{`${formattedDate} ${t("ago")}`}</span>
        </div>
      </div>

      <Link href={`/recipes/${recipe.id}`}>
        <div className='mt-3 h-[300px] overflow-hidden rounded-lg'>
          <Image
            src={imageURL ?? ""}
            alt={"Recipe's image"}
            className='object-cover transition-transform duration-300 hover:scale-105'
            fill
          />
        </div>
      </Link>
    </div>
  );
};

export const CommentCard = ({
  comment,
  className
}: {
  comment: ICommentLogResponse;
  className?: string;
}) => {
  const { content, authorDisplayName, authorId, authorAvatarURL, createdAt } = comment;
  const locale = useLocale();
  const t = useTranslations("administerAdmins.detail.activity");
  const formattedDate = useFormattedDistance(createdAt, locale);

  return (
    <div className={`mt-4 rounded-lg border border-gray-200 p-4 ${className}`}>
      <div className={`grid grid-cols-[auto_1fr] items-center gap-x-2`}>
        <Link
          href={`/users/${authorId}`}
          className='transition-opacity hover:opacity-80'
        >
          <Avatar
            src={authorAvatarURL}
            alt={authorDisplayName}
          />
        </Link>

        <div className='flex flex-col'>
          <div className='flex flex-wrap items-center gap-2'>
            <Link href={`/users/${authorId}`}>
              <span className='text-black_white font-bold transition-colors hover:text-primary'>
                {authorDisplayName}
              </span>
            </Link>

            <div className='flex items-center gap-1 text-gray-500'>
              <Clock size={16} />
              <span className='text-sm'>{`${formattedDate} ${t("ago")}`}</span>
            </div>
          </div>
        </div>

        <p className='text-black_white col-span-2 mt-2 max-w-[70em]'>{content}</p>
      </div>
    </div>
  );
};

export const UserCard = ({
  user,
  className
}: {
  user: IUserLogResponse;
  className?: string;
}) => {
  const { displayName, username, avatarURL, id } = user;

  return (
    <div className={`mt-4 rounded-lg border border-gray-200 p-4 ${className}`}>
      <div className='flex-center flex-col gap-2'>
        <Link
          href={`/users/${id}`}
          className='transition-opacity hover:opacity-80'
        >
          <Avatar
            src={avatarURL}
            alt={displayName}
          />
        </Link>

        <Link href={`/users/${id}`}>
          <div className='text-black_white flex flex-col items-center'>
            <span className='font-bold transition-colors hover:text-primary'>
              {displayName}
            </span>
            <span className='text-gray-500 transition-colors hover:text-primary'>
              @{username}
            </span>
          </div>
        </Link>
      </div>
    </div>
  );
};

export const TagCard = ({ tag }: { tag: ITagLogResponse }) => {
  const { id, value, code, category, status, imageUrl } = tag;

  return (
    <div className='mt-4 rounded-lg border border-gray-200 p-4'>
      <div className='flex items-center gap-4'>
        <div className='size-12 overflow-hidden rounded-lg'>
          <Image
            src={imageUrl ?? ""}
            alt={value}
            className='size-full object-cover'
            width={48}
            height={48}
          />
        </div>

        <div className='flex flex-col'>
          <Link href={`/tags/${id}`}>
            <div className='text-black_white text-lg font-medium'>#{value}</div>
          </Link>

          <div className='flex items-center gap-2 text-sm text-gray-500'>
            <span>{category}</span>
            <span>·</span>
            <span>{code}</span>
            <span>·</span>
            <span
              className={`rounded-full px-2 py-0.5 text-xs ${
                status.toLowerCase() === "active"
                  ? "bg-green-100 text-green-800"
                  : "bg-gray-100 text-gray-800"
              } `}
            >
              {status}
            </span>
          </div>
        </div>
      </div>
    </div>
  );
};

export const RecipeReportCard = ({
  report
}: {
  report: IRecipeReportAdminActivityLogResponse;
}) => {
  const locale = useLocale();
  const t = useTranslations("administerAdmins.detail.activity");
  const { recipe, report: reportDetails, reporter } = report;

  const formattedDate = useFormattedDistance(reportDetails.createdAt, locale);

  return (
    <div className='mt-4 flex flex-col gap-4'>
      <div className='rounded-lg border border-gray-200 p-4'>
        <div className='flex flex-col gap-3.5'>
          <div>
            <span className='text-black_white font-bold'>
              {reportDetails.additionalDetail}
            </span>
          </div>

          <div className='flex flex-wrap gap-2'>
            {reportDetails.reasons.map((reason, index) => (
              <span
                key={`${reason}-${index}`}
                className='bg-white_black w-fit rounded-full px-3 py-1 text-sm font-medium text-primary'
              >
                {reason}
              </span>
            ))}
          </div>

          <div className='flex flex-wrap gap-2'>
            <div className='flex items-center gap-2'>
              <Link
                href={`/users/${reporter.id}`}
                className='transition-opacity hover:opacity-80'
              >
                <Avatar
                  src={reporter.avatarURL}
                  alt={reporter.username}
                />
              </Link>
              <Link
                href={`/users/${reporter.id}`}
                className='group'
              >
                <span className='text-sm font-bold text-gray-700 group-hover:text-primary dark:text-gray-400'>
                  {reporter.displayName}
                </span>
              </Link>
            </div>

            <div className='flex items-center gap-1 text-gray-500'>
              <Clock size={16} />
              <span className='text-sm'>{`${formattedDate} ${t("ago")}`}</span>
            </div>
          </div>
        </div>
      </div>

      <div className='flex gap-2'>
        <Reference />
        <RecipeCard
          recipe={recipe}
          className='grow'
        />
      </div>
    </div>
  );
};

export const CommentReportCard = ({
  report
}: {
  report: ICommentReportAdminActivityLogResponse;
}) => {
  const locale = useLocale();
  const t = useTranslations("administerAdmins.detail.activity");
  const { recipe, comment, report: reportDetails, reporter } = report;

  const formattedDate = useFormattedDistance(reportDetails.createdAt, locale);

  return (
    <div className='mt-4 flex flex-col gap-4'>
      <div className='rounded-lg border border-gray-200 p-4'>
        <div className='flex flex-col gap-3.5'>
          <div>
            <span className='text-black_white font-bold'>
              {reportDetails.additionalDetail}
            </span>
          </div>

          <div className='flex flex-wrap gap-2'>
            {reportDetails.reasons.map((reason, index) => (
              <span
                key={`${reason}-${index}`}
                className='bg-white_black w-fit rounded-full px-3 py-1 text-sm font-medium text-primary'
              >
                {reason}
              </span>
            ))}
          </div>

          <div className='flex flex-wrap gap-2'>
            <div className='flex items-center gap-2'>
              <Link
                href={`/users/${reporter.id}`}
                className='transition-opacity hover:opacity-80'
              >
                <Avatar
                  src={reporter.avatarURL}
                  alt={reporter.username}
                />
              </Link>
              <Link
                href={`/users/${reporter.id}`}
                className='group'
              >
                <span className='text-sm font-bold text-gray-700 group-hover:text-primary dark:text-gray-400'>
                  {reporter.displayName}
                </span>
              </Link>
            </div>

            <div className='flex items-center gap-1 text-gray-500'>
              <Clock size={16} />
              <span className='text-sm'>{`${formattedDate} ${t("ago")}`}</span>
            </div>
          </div>
        </div>
      </div>

      <div className='flex gap-2'>
        <Reference />
        <div className='grow'>
          <CommentCard comment={comment} />
          <div className='mt-2 flex grow gap-2'>
            <Reference />
            <RecipeCard
              recipe={recipe}
              className='w-full'
            />
          </div>
        </div>
      </div>
    </div>
  );
};

export const UserReportCard = ({
  report
}: {
  report: IUserReportAdminActivityLogResponse;
}) => {
  const locale = useLocale();
  const t = useTranslations("administerAdmins.detail.activity");
  const { user, report: reportDetails, reporter } = report;

  const formattedDate = useFormattedDistance(reportDetails.createdAt, locale);

  return (
    <div className='mt-4 flex flex-col gap-4'>
      <div className='rounded-lg border border-gray-200 p-4'>
        <div className='flex flex-col gap-3.5'>
          <div>
            <span className='text-black_white font-bold'>
              {reportDetails.additionalDetail}
            </span>
          </div>

          <div className='flex flex-wrap gap-2'>
            {reportDetails.reasons.map((reason, index) => (
              <span
                key={`${reason}-${index}`}
                className='bg-white_black w-fit rounded-full px-3 py-1 text-sm font-medium text-primary'
              >
                {reason}
              </span>
            ))}
          </div>

          <div className='flex flex-wrap gap-2'>
            <div className='flex items-center gap-2'>
              <Link
                href={`/users/${reporter.id}`}
                className='transition-opacity hover:opacity-80'
              >
                <Avatar
                  src={reporter.avatarURL}
                  alt={reporter.username}
                />
              </Link>
              <Link
                href={`/users/${reporter.id}`}
                className='group'
              >
                <span className='text-sm font-bold text-gray-700 group-hover:text-primary dark:text-gray-400'>
                  {reporter.displayName}
                </span>
              </Link>
            </div>

            <div className='flex items-center gap-1 text-gray-500'>
              <Clock size={16} />
              <span className='text-sm'>{`${formattedDate} ${t("ago")}`}</span>
            </div>
          </div>
        </div>
      </div>

      <div className='flex gap-2'>
        <Reference />
        <UserCard
          user={user}
          className='grow'
        />
      </div>
    </div>
  );
};

const Reference = () => {
  return (
    <CornerDownRight
      size={18}
      className='min-w-fit text-gray-500'
    />
  );
};
