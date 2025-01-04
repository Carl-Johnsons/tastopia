import { useInfiniteQuery, useQuery } from "react-query";
import { protectedAxiosInstance } from "@/constants/host";

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

type RecipeDetailResponse = {
  recipe: RecipeDetailType;
  authorUsername: string;
  authorAvtUrl: string;
  authorDisplayName: string;
  authorNumberOfFollower: number;
};

const useRecipeDetail = (recipeId: string) => {
  return useQuery<RecipeDetailResponse>({
    queryKey: ["recipe", recipeId],
    queryFn: async () => {
      const { data } = await protectedAxiosInstance.post<RecipeDetailResponse>(
        "/api/recipe/get-recipe-details",
        {
          recipeId
        }
      );
      return data;
    },
    enabled: !!recipeId
  });
};

export { useRecipesFeed, useRecipeDetail };
