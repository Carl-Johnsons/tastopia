import { useInfiniteQuery } from "react-query";
import { protectedAxiosInstance } from "@/constants/host";

type SearchUserResponse = {
  paginatedData: SearchUserResultProps[];
  metadata: {
    hasNextPage: boolean;
    totalPage: number;
  };
};

const useSearchUsers = (keyword: string) => {
  return useInfiniteQuery<SearchUserResponse>({
    queryKey: ["searchUsers", keyword],
    enabled: keyword.length > 0,
    queryFn: async ({ pageParam = 0 }) => {
      const { data } = await protectedAxiosInstance.post<SearchUserResponse>(
        "/api/user/search",
        {
          keyword,
          skip: pageParam.toString()
        }
      );
      return data;
    },
    getNextPageParam: (lastPage, pages) => {
      if (!lastPage.metadata.hasNextPage) {
        return undefined;
      }
      return pages.length;
    }
  });
};

export { useSearchUsers };
