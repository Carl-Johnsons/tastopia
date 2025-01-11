type SearchUserResultType = {
  id: string;
  avtUrl: string;
  displayName: string;
  username: string;
  numberOfRecipe: number;
  isFollowing: boolean;
};

type SearchUserResponse = {
  paginatedData: SearchUserResultType[];
  metadata: {
    hasNextPage: boolean;
    totalPage: number;
  };
};
