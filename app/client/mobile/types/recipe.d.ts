type SearchRecipeType = {
  id: string;
  authorId: string;
  recipeImgUrl: string;
  title: string;
  description: string;
  authorDisplayName: string;
  authorAvtUrl: string;
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
  vote?: string;
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

type RecipeDetailResponse = {
  recipe: RecipeDetailType;
  authorUsername: string;
  authorAvtUrl: string;
  authorDisplayName: string;
  authorNumberOfFollower: number;
  vote: string;
  isBookmarked: boolean;
  tags: TagType[] | null;
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
