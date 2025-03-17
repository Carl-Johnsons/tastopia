"use client";

import Image from "next/image";
import { IUser } from "../../../../../../mobile/generated/interfaces/user.interface";
import Link from "next/link";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { DownvoteIcon, UpvoteIcon } from "@/components/shared/icons";
import { useState } from "react";
import { toast } from "react-toastify";
import { DeleteButton, RestoreButton } from "./Button";

type HeaderProps = {
  recipeId: string;
  title: string;
  imageUrl: string;
  description: string;
  isActive: boolean;
  voteDiff: number;
  author: IUser;
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

  const handleRestore = () => {
    setIsActive(true);
    toast.success("Restored recipe successfully.");
  };

  const handleDelete = () => {
    setIsActive(false);
    toast.success("Deleted recipe successfully.");
  };

  return (
    <div className='flex flex-col gap-6 lg:flex-row'>
      <Image
        src={imageUrl}
        alt={`Image of ${title}`}
        width={360}
        height={446}
        className='h-[300px] w-full shrink rounded-md object-cover lg:h-[446px] lg:w-[360px]'
      />
      <div className='flex grow-[8] flex-col gap-3'>
        <div className='flex items-center gap-3'>
          <h1 className='text-black_white font-semibold text-2xl'>{title}</h1>
          <StatusText
            isActive={isActive}
            coloring
          />
        </div>

        <Vote
          votes={voteDiff}
          disabled
        />

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
                <span className='font-bold text-sm'>{displayName}</span>
                <span className='text-sm'>@{accountUsername}</span>
              </div>
            </Link>
            <span className='text-sm text-gray-700'>
              {totalFollower || 0} follower
              {!!totalFollower && totalFollower > 2 && "s"}
            </span>
          </div>
        </div>

        <p className='text-black_white max-w-[70em]'>{description}</p>
        {isActive ? (
          <DeleteButton
            title='Delete'
            onSuccess={handleDelete}
            targetId={recipeId}
            className="w-fit rounded-full"
          />
        ) : (
          <RestoreButton
            title='Restore'
            onSuccess={handleRestore}
            targetId={recipeId}
            className="w-fit rounded-full"
          />
        )}
      </div>
    </div>
  );
};

export const StatusText = ({
  isActive,
  coloring
}: {
  isActive: boolean;
  coloring?: boolean;
}) => {
  return (
    <div className='flex-center flex gap-2'>
      {isActive ? (
        <>
          <div className='size-2.5 rounded-full bg-green-500' />
          <span className={`font-medium ${coloring && "text-green-500"}`}>Active</span>
        </>
      ) : (
        <>
          <div className='size-2.5 rounded-full bg-red' />
          <span className={`font-medium ${coloring && "text-red"}`}>Inactive</span>
        </>
      )}
    </div>
  );
};

export const Vote = ({ votes, disabled }: { votes: number; disabled?: boolean }) => {
  return (
    <div
      className={`flex w-fit items-center gap-1 rounded-full border border-gray-200 p-1 ${disabled && "cursor-not-allowed"}`}
    >
      <UpvoteIcon className={`${disabled && "text-gray-500"}`} />
      <span className='text-black_white font-medium text-sm'>{votes}</span>
      <div className='h-full border-r border-gray-200' />
      <DownvoteIcon className={`${disabled && "text-gray-500"}`} />
    </div>
  );
};

export default Header;
