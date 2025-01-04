type SearchRecipeType = {
  id: string;
  authorId: string;
  recipeImgUrl: string;
  title: string;
  description: string;
  authorDisplayName: string;
  authorAvtUrl: string;
};

type RecipeType = SearchRecipeType & {
  voteDiff: number;
  numberOfComment: number;
};

type RecipeStep = {
  recipeId: string;
  odinalNumber: number;
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
  recipeTags: null;
  createdAt: string;
  updatedAt: string;
  id: string;
};

type RecipeResponse = {
  paginatedData: RecipeType[];
  metadata: {
    hasNextPage: boolean;
    totalPage: number;
  };
};
