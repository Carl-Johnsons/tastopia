"use client";

import { useGetRecipeComments } from "@/api/recipe";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Button } from "@/components/ui/button";
import Loader from "@/components/ui/Loader";
import { IRecipeCommentResponse } from "@/generated/interfaces/recipe.interface";
import { formatRelative } from "date-fns";
import { Link } from "@/i18n/navigation";
import { useMemo, useState } from "react";
import { SmallDisableCommentButton, SmallRestoreCommentButton } from "./Button";

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
            recipeId={recipeId}
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
  recipeId: string;
};

function Comment({ comment, recipeId }: CommentProps) {
  const { id, content, createdAt, accountId, displayName, avatarUrl } = comment;
  const time = useMemo(
    () => formatRelative(new Date(createdAt), new Date()),
    [createdAt]
  );

  const [isActive, setIsActive] = useState(comment.isActive);
  const lowOpacityOnInactive = useMemo(() => (isActive ? "" : "opacity-50"), [isActive]);

  return (
    <div className={`grid grid-cols-[auto_1fr] items-center gap-x-2`}>
      <Link
        href={`/users/${accountId}`}
        className={lowOpacityOnInactive}
      >
        <Avatar>
          <AvatarImage src={avatarUrl} />
          <AvatarFallback className='bg-black_white'>
            {displayName.substring(0, 1)}
          </AvatarFallback>
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

          {isActive ? (
            <SmallDisableCommentButton
              title='Disable'
              targetId={id}
              recipeId={recipeId}
              onSuccess={() => setIsActive(false)}
            />
          ) : (
            <SmallRestoreCommentButton
              title='Restore'
              targetId={id}
              recipeId={recipeId}
              onSuccess={() => setIsActive(true)}
            />
          )}
        </div>
      </div>

      <p className={`text-black_white col-start-2 max-w-[70em] ${lowOpacityOnInactive}`}>
        {content}
      </p>
    </div>
  );
}
