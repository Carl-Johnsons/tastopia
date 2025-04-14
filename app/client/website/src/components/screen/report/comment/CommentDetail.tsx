"use client";

import { ICommentDetailResponse } from "@/generated/interfaces/recipe.interface";
import Content from "./Content";
import { useGetUserById } from "@/api/user";
import SomethingWentWrong from "@/components/shared/common/Error";
import Loader from "@/components/ui/Loader";

type Props = {
  comment: ICommentDetailResponse;
  recipeId: string;
  className?: string;
};

export default function CommentDetail({ comment, recipeId, className }: Props) {
  const { id, authorId, content, isActive } = comment;
  const { data: author, isLoading, isError } = useGetUserById(authorId);

  if (isError) return <SomethingWentWrong />;
  if (isLoading || !author) return <Loader />;

  return (
    <div
      className={`bg-white_black100 flex h-fit w-full max-w-[1000px] flex-col gap-4 rounded-xl border border-gray-200 p-6 shadow-sm dark:border-gray-600 ${className}`}
    >
      <Content
        commentId={id}
        recipeId={recipeId}
        content={content}
        author={author}
        createdAt={comment.createdAt}
        isActive={isActive}
      />
    </div>
  );
}
