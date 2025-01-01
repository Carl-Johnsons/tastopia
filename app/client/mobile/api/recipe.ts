import { API_HOST, protectedAxiosInstance } from "@/constants/host";
import { selectAccessToken } from "@/slices/auth.slice";
import axios from "axios";
import { useInfiniteQuery } from "react-query";
// import { protectedAxiosInstance } from "@/constants/host";

interface RecipeResponse {
  paginatedData: RecipeType[];
  metadata: {
    hasNextPage: boolean;
    totalPage: number;
  };
}

const useRecipesFeed = (filterSelected: string) => {
  return useInfiniteQuery<RecipeResponse>({
    queryKey: ["recipes", filterSelected],
    queryFn: async ({ pageParam = 0 }) => {
      const { data } = await protectedAxiosInstance.post<RecipeResponse>(
        "/api/recipe/get-recipe-feed",
        {
          skip: pageParam.toString(),
          tagValues: [filterSelected]
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

export { useRecipesFeed };
