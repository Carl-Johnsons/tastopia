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

type CommentCustomType = {
  id: string;
  content: string;
};

type GetRecipeCommentResponse = {
  paginatedData: CommentType[];
  metadata: {
    hasNextPage: boolean;
    totalPage: number;
  };
};

type CreateCommentPayloadType = {
  recipeId: string;
  content: string;
};

type CommentResponseType = {
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
