import { useAxios } from "@/context/AxiosContext";
import { useInfiniteQuery } from "react-query";

interface RecipeResponse {
  paginatedData: RecipeType[];
  metadata: {
    hasNextPage: boolean;
  };
}

const useRecipesFeed = (filterSelected: string) => {
  const { axiosInstance } = useAxios();

  return useInfiniteQuery({
    queryKey: ["recipes", filterSelected],
    queryFn: async ({ pageParam = 0 }) => {
      const { data } = await axiosInstance.post<RecipeResponse>(
        "/api/recipe/get-recipe-feed",
        {
          skip: pageParam,
          tagValues: [filterSelected]
        }
      );
      return data;
    },
    getNextPageParam: (lastPage, pages) => {
      if (!lastPage.metadata.hasNextPage) return undefined;
      return pages.length * 10;
    }
  });
};

export { useRecipesFeed };
