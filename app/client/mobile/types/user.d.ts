type SearchUserResultType = {
  id: string;
  avtUrl: string;
  displayName: string;
  username: string;
  numberOfRecipe: number;
  isFollowing: boolean;
};

type User = {
  _id: string;
  name: string;
  email: string;
  username: string;
  bio: string;
  profilePic: string;
  followers: string[];
  following: string[];
};

type SearchUserResponse = {
  paginatedData: SearchUserResultType[];
  metadata: {
    hasNextPage: boolean;
    totalPage: number;
  };
};
