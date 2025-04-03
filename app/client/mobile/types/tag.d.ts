type SelectedTag = {
  id?: string;
  code: string;
  value: string;
};

type TagType = {
  id: string;
  value: {
    en: string;
    vi: string;
  };
  code: string;
  category: number;
  status: string;
  imageUrl: string;
  recipeTags: any[];
  createdAt: string;
  updatedAt: string;
};

type SearchTagResponse = {
  paginatedData: TagType[];
  metadata: {
    hasNextPage: boolean;
    totalPage: number;
  };
};
