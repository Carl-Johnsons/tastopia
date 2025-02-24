export enum VoteType {
  UPVOTE = "Upvote",
  DOWNVOTE = "Downvote",
  NONE = "None"
}

type SearchRecipeType = {
  id: string;
  authorId: string;
  recipeImgUrl: string;
  title: string;
  description: string;
  authorDisplayName: string;
  authorAvtUrl: string;
};

type MiniRecipeType = Omit<SearchRecipeType, "authorAvtUrl" | "description"> & {
  voteDiff: number;
  vote: VoteType;
};

type SearchRecipeResponse = {
  paginatedData: SearchRecipeType[];
  metadata: {
    hasNextPage: boolean;
    totalPage: number;
  };
};

type RecipeType = SearchRecipeType & {
  voteDiff?: number;
  numberOfComment?: number;
  vote?: VoteType;
};

type MiniRecipeType = {
  isMinimal?: boolean;
};

type RecipeStep = {
  recipeId: string;
  ordinalNumber: number;
  content: string;
  attachedImageUrls: string[] | null;
  createdAt: string;
  updatedAt: string;
  id: string;
};

type RecipeDetailType = {
  authorId: string;
  title: string;
  description: string;
  imageUrl: string;
  ingredients: string[];
  cookTime: string;
  serves: number;
  voteDiff: number;
  numberOfComment: number;
  isActive: boolean;
  totalView: number;
  steps: RecipeStep[];
  comments: null;
  recipeVotes: null;
  recipeTags: TagType[] | null;
  createdAt: string;
  updatedAt: string;
  id: string;
};

type SimilarRecipe = {
  recipeId: string;
  imageUrl: string;
  title: string;
};

type RecipeDetailResponse = {
  recipe: RecipeDetailType;
  authorUsername: string;
  authorAvtUrl: string;
  authorDisplayName: string;
  authorNumberOfFollower: number;
  vote: VoteType;
  isBookmarked: boolean;
  tags: TagType[] | null;
  similarRecipes: SimilarRecipe[];
};

type RecipeResponse = {
  paginatedData: RecipeType[];
  metadata: {
    hasNextPage: boolean;
    totalPage: number;
  };
};

type CreateRecipePayloadType = {
  data: FormData;
};

type BookMarkRecipeResponse = {
  userBookmarkRecipe: {
    accountId: string;
    recipeId: string;
    recipe: null | unknown;
    createdAt: string;
    updatedAt: string;
    id: string;
  };
  isBookmark: boolean;
};

interface ICommentResponse {
  paginatedData: IAccountRecipeCommentResponse[];
  metadata: {
    hasNextPage: boolean;
    totalPage: number;
  };
}
