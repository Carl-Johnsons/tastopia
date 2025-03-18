"use client";

import { useGetRecipeComments } from "@/api/recipe";
import { useGetUserById } from "@/api/user";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Button } from "@/components/ui/button";
import Loader from "@/components/ui/Loader";
import { Skeleton } from "@/components/ui/skeleton";
import { IRecipeCommentResponse } from "@/generated/interfaces/recipe.interface";
import { formatRelative } from "date-fns";
import Link from "next/link";
import { useCallback, useMemo, useState } from "react";
import { toast } from "react-toastify";
import InteractiveButton from "./Button";
import { RotateCw, Trash } from "lucide-react";

type Props = {
  recipeId: string;
};

export default function Comments({ recipeId }: Props) {
  const { data, isLoading, hasNextPage, fetchNextPage } = useGetRecipeComments(recipeId);

  if (isLoading) {
    return <Loader />;
  }

  if (data?.pages[0].paginatedData.length === 0) {
    return (
      <div className='flex-center flex h-[100px] w-full'>
        <span className='text-black_white'>This recipe does not have any comment.</span>
      </div>
    );
  }

  return (
    <div className='flex flex-col gap-3'>
      <h2 className='text-black_white text-2xl font-semibold'>Comments</h2>

      {data?.pages.map(page =>
        page.paginatedData.map(comment => (
          <Comment
            key={comment.id}
            comment={comment}
          />
        ))
      )}

      {hasNextPage && (
        <div className='flex-center'>
          <Button
            onClick={() => fetchNextPage()}
            className='w-fit rounded-full'
          >
            <span className='text-white_black'>Load more</span>
          </Button>
        </div>
      )}
    </div>
  );
}

type CommentProps = {
  comment: IRecipeCommentResponse;
};

function Comment({ comment }: CommentProps) {
  const { content, createdAt, accountId, displayName, avatarUrl } = comment;
  const time = useMemo(
    () => formatRelative(new Date(createdAt), new Date()),
    [createdAt]
  );

  const [isActive, setIsActive] = useState(comment.isActive);
  const lowOpacityOnInactive = useMemo(() => (isActive ? "" : "opacity-50"), [isActive]);

  const handleDelete = useCallback(() => {
    setIsActive(false);
    toast.success("Deleted comment successfully.");
  }, []);

  const handleRestore = useCallback(() => {
    setIsActive(true);
    toast.success("Restored comment successfully.");
  }, []);

  return (
    <div className={`grid grid-cols-[auto_1fr] items-center gap-x-2`}>
      <Link
        href={`/users/${accountId}`}
        className={lowOpacityOnInactive}
      >
        <Avatar>
          <AvatarImage src={avatarUrl} />
          <AvatarFallback className="bg-black_white">{displayName.substring(0, 1)}</AvatarFallback>
        </Avatar>
      </Link>

      <div className='flex flex-col'>
        <div className='text-black_white flex items-center gap-1.5'>
          <Link
            href={`/users/${accountId}`}
            className={`flex items-center gap-1.5 ${lowOpacityOnInactive}`}
          >
            <span className='font-bold'>{displayName}</span>
          </Link>
          <span
            className={`text-sm text-gray-700 opacity-50 dark:opacity-80 ${lowOpacityOnInactive}`}
          >
            {time}
          </span>

          <InteractiveButton
            title={`${isActive ? "Delete" : "Restore"} comment`}
            icon={
              isActive ? (
                <Trash className='text-black_white group-hover:text-red-500' />
              ) : (
                <RotateCw className='text-black_white group-hover:text-green-500' />
              )
            }
            onClick={isActive ? handleDelete : handleRestore}
            className={`hover:bg-transparent} ms-auto h-fit w-fit bg-transparent p-0 pb-1 shadow-none`}
            noText
            toolTip
          />
        </div>
      </div>

      <p className={`text-black_white col-start-2 max-w-[70em] ${lowOpacityOnInactive}`}>
        {content}
      </p>
      {}
    </div>
  );
}
