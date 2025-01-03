type CommentType = {
  id: string;
  recipeId: string;
  accountId: string;
  displayName: string;
  content: string;
  avatarUrl: string;
  createdAt: string;
  updatedAt: string;
  isActive: boolean;
};

type GetRecipeCommentResponse = {
  paginatedData: CommentType[];
  metadata: {
    hasNextPage: boolean;
    totalPage: number;
  };
};
