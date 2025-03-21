"use client";

import { Link } from "@/i18n/navigation";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { useMemo, useState } from "react";
import { IAdminGetUserDetailResponse } from "@/generated/interfaces/user.interface";
import { DisableCommentButton, RestoreCommentButton } from "./Button";
import { formatRelative } from "date-fns";
import { ItemStatusText } from "../common/StatusText";

type Props = {
  commentId: string;
  recipeId: string;
  content: string;
  isActive: boolean;
  createdAt: string;
  author: IAdminGetUserDetailResponse;
};

const Content = ({ commentId, recipeId, content, author, ...props }: Props) => {
  const { accountId, avatarUrl, displayName, accountUsername, totalFollower } = author;
  const [isActive, setIsActive] = useState(props.isActive);
  const lowOpacityOnInactive = useMemo(() => (isActive ? "" : "opacity-50"), [isActive]);
  const createdAt = useMemo(
    () => formatRelative(new Date(props.createdAt), new Date()),
    [props.createdAt]
  );

  return (
    <div className='flex flex-col gap-3 min-w-[320px]'>
      <div className='flex items-center justify-between'>
        <ItemStatusText
          isActive={isActive}
          coloring
        />

        <span
          className={`text-sm text-gray-700 dark:text-gray-500 opacity-50 dark:opacity-80 ${lowOpacityOnInactive}`}
        >
          {createdAt}
        </span>
      </div>

      <div className='flex items-center justify-between'>
        <div className='flex items-center gap-2'>
          <Link href={`/users/${accountId}`}>
            <Avatar>
              <AvatarImage src={avatarUrl} />
              <AvatarFallback>{accountUsername.substring(0, 1)}</AvatarFallback>
            </Avatar>
          </Link>
          <div className='flex flex-col'>
            <Link href={`/users/${accountId}`}>
              <div className='text-black_white flex gap-1.5'>
                <span className='text-sm font-bold'>{displayName}</span>
                <span className='text-sm'>@{accountUsername}</span>
              </div>
            </Link>
            <span className='text-sm text-gray-700 dark:text-gray-500'>
              {totalFollower || 0} follower
              {!!totalFollower && totalFollower > 2 && "s"}
            </span>
          </div>
        </div>

        {isActive ? (
          <DisableCommentButton
            title='Disable'
            onSuccess={() => setIsActive(false)}
            targetId={commentId}
            recipeId={recipeId}
            className='w-fit rounded-full'
          />
        ) : (
          <RestoreCommentButton
            title='Restore'
            onSuccess={() => setIsActive(true)}
            targetId={commentId}
            recipeId={recipeId}
            className='w-fit rounded-full'
          />
        )}
      </div>

      <p className='text-black_white max-w-[70em]'>{content}</p>
    </div>
  );
};

export default Content;
