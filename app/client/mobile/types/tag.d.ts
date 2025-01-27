type SelectedTag = {
  id?: string;
  code: string;
  value: string;
};

type SearchTagType = {
  id: string;
  value: string;
  code: string;
  category: number;
  status: string;
  imageUrl: string;
  recipeTags: any[];
  createdAt: string;
  updatedAt: string;
};

type SearchTagResponse = {
  paginatedData: SearchTagType[];
  metadata: {
    hasNextPage: boolean;
    totalPage: number;
  };
};
