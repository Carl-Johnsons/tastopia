"use client";

import { Link } from "@/i18n/navigation";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { DownvoteIcon, UpvoteIcon } from "@/components/shared/icons";
import { useState } from "react";
import { DisableRecipeButton, RestoreRecipeButton } from "./Button";
import { IAdminGetUserDetailResponse } from "@/generated/interfaces/user.interface";
import { ItemStatusText } from "../common/StatusText";
import Image from "@/components/shared/common/Image";

type HeaderProps = {
  recipeId: string;
  title: string;
  imageUrl: string;
  description: string;
  isActive: boolean;
  voteDiff: number;
  author: IAdminGetUserDetailResponse;
};

const Header = ({
  recipeId,
  title,
  imageUrl,
  description,
  voteDiff,
  author,
  ...props
}: HeaderProps) => {
  const { accountId, avatarUrl, displayName, accountUsername, totalFollower } = author;
  const [isActive, setIsActive] = useState(props.isActive);

  return (
    <div className='grid gap-6 lg:grid-cols-[300px_1fr]'>
      <div className='relative h-[300px]'>
        <Image
          src={imageUrl}
          alt={`Image of ${title}`}
          fill
          className='rounded-md object-cover'
        />
      </div>
      <div className='flex flex-col gap-3'>
        <div className='flex items-center gap-3'>
          <h1 className='text-black_white text-2xl font-semibold'>{title}</h1>
          <ItemStatusText
            isActive={isActive}
            coloring
          />
        </div>

        <Vote
          votes={voteDiff}
          disabled
        />

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
              <span className='text-sm text-gray-700'>
                {totalFollower || 0} follower
                {!!totalFollower && totalFollower > 2 && "s"}
              </span>
            </div>
          </div>

          {isActive ? (
            <DisableRecipeButton
              title='Disable'
              onSuccess={() => setIsActive(false)}
              targetId={recipeId}
              className='w-fit rounded-full'
            />
          ) : (
            <RestoreRecipeButton
              title='Restore'
              onSuccess={() => setIsActive(true)}
              targetId={recipeId}
              className='w-fit rounded-full'
            />
          )}
        </div>

        <p className='text-black_white max-w-[70em]'>{description}</p>
      </div>
    </div>
  );
};

export const Vote = ({ votes, disabled }: { votes: number; disabled?: boolean }) => {
  return (
    <div
      className={`flex w-fit items-center gap-1 rounded-full border border-gray-200 p-1 ${disabled && "cursor-not-allowed"}`}
    >
      <UpvoteIcon className={`${disabled && "text-gray-500"}`} />
      <span className='text-black_white text-sm font-medium'>{votes}</span>
      <div className='h-full border-r border-gray-200' />
      <DownvoteIcon className={`${disabled && "text-gray-500"}`} />
    </div>
  );
};

export default Header;
