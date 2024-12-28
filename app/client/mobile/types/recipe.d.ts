type SearchRecipeType = {
  id: string;
  authorId: string;
  title: string;
  description: string;
  authorDisplayName: string;
  authorAvtUrl: string;
};

type RecipeType = SearchRecipeType & {
  voteDiff: number;
  numberOfComment: number;
};
