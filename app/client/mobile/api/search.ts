import { useInfiniteQuery } from "react-query";
import { protectedAxiosInstance } from "@/constants/host";

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

const useSearchRecipes = (keyword: string, tagCodes: string[]) => {
  const finalTagCodes = tagCodes.length > 0 ? tagCodes : ["ALL"];
  return useInfiniteQuery<SearchRecipeResponse>({
    queryKey: ["searchRecipes", keyword],
    enabled: keyword.length > 0 || tagCodes.length > 0,
    queryFn: async ({ pageParam = 0 }) => {
      const { data } = await protectedAxiosInstance.post<SearchRecipeResponse>(
        "/api/recipe/search-recipe",
        {
          keyword,
          tagCodes: finalTagCodes,
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

const useSearchTags = (keyword: string, tagCodes: string[], category: string) => {
  const finalTagCodes = tagCodes.length > 0 ? tagCodes : ["ALL"];
  return useInfiniteQuery<SearchTagResponse>({
    queryKey: ["searchTags", keyword],
    enabled: keyword.length > 0,
    queryFn: async ({ pageParam = 0 }) => {
      const { data } = await protectedAxiosInstance.post<SearchTagResponse>(
        "/api/recipe/get-tag",
        {
          keyword,
          tagCodes: finalTagCodes,
          category,
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

export { useSearchUsers, useSearchRecipes, useSearchTags };
