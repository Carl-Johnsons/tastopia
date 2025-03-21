import { ICommentDetailResponse } from "@/generated/interfaces/recipe.interface";
import { getUserById } from "@/actions/user.action";
import Content from "./Content";

type Props = {
  comment: ICommentDetailResponse;
  recipeId: string;
  className?: string;
};

export default async function CommentDetail({ comment, recipeId, className }: Props) {
  const { id, authorId, content, isActive } = comment;
  const author = await getUserById(authorId);

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
