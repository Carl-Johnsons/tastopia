import { useInfiniteQuery, useMutation, useQuery, useQueryClient } from "react-query";
import { protectedAxiosInstance } from "@/constants/host";
import { SearchRecipeResponse } from "@/types/recipe";

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
  const finalTagCodes =
    tagCodes.length > 0 ? tagCodes.map(tagCode => tagCode.toUpperCase()) : ["ALL"];

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
    enabled: true,
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

const useSearchTagsCommunity = (
  keyword: string,
  tagCodes: string[],
  category: string
) => {
  const finalTagCodes = tagCodes.length > 0 ? tagCodes : ["ALL"];
  return useInfiniteQuery<SearchTagResponse>({
    queryKey: ["searchTagsCommunity", keyword],
    enabled: true,
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

type SearchHistory = {
  value: string[];
};

const useSearchRecipeHistory = () => {
  return useQuery<SearchHistory>({
    queryKey: ["recipeSearchHistory"],
    queryFn: async () => {
      const { data } = await protectedAxiosInstance.get<SearchHistory>(
        "/api/tracking/get-user-search-recipe-history"
      );
      return data;
    }
  });
};

const useSearchUserHistory = () => {
  return useQuery<SearchHistory>({
    queryKey: ["userSearchHistory"],
    queryFn: async () => {
      const { data } = await protectedAxiosInstance.get<SearchHistory>(
        "/api/tracking/get-user-search-user-history"
      );
      return data;
    }
  });
};

const createUserSearchRecipeKeyword = () => {
  const queryClient = useQueryClient();
  return useMutation<string, Error, { keyword: string }>({
    mutationFn: async ({ keyword }) => {
      const { data } = await protectedAxiosInstance.post(
        "/api/recipe/create-user-search-recipe",
        {
          keyword: keyword
        }
      );

      await queryClient.invalidateQueries({ queryKey: ["recipeSearchHistory"] });
      return data;
    }
  });
};

const createUserSearchUserKeyword = () => {
  const queryClient = useQueryClient();
  return useMutation<string, Error, { keyword: string }>({
    mutationFn: async ({ keyword }) => {
      const { data } = await protectedAxiosInstance.post(
        "/api/user/create-user-search-user",
        {
          keyword: keyword
        }
      );

      await queryClient.invalidateQueries({ queryKey: ["userSearchHistory"] });
      return data;
    }
  });
};

const useDeleteSearchRecipeHistory = () => {
  return useMutation<any, Error, { keyword: string }>({
    mutationFn: async ({ keyword }) => {
      const { data } = await protectedAxiosInstance.post(
        "/api/tracking/delete-user-search-recipe-history",
        {
          keyword: keyword
        }
      );
      return data;
    }
  });
};

const useDeleteSearchUserHistory = () => {
  return useMutation<any, Error, { keyword: string }>({
    mutationFn: async ({ keyword }) => {
      const { data } = await protectedAxiosInstance.post(
        "/api/tracking/delete-user-search-user-history",
        {
          keyword: keyword
        }
      );
      return data;
    }
  });
};

export {
  useSearchUsers,
  useSearchRecipes,
  useSearchTags,
  useSearchTagsCommunity,
  useSearchRecipeHistory,
  useSearchUserHistory,
  createUserSearchRecipeKeyword,
  createUserSearchUserKeyword,
  useDeleteSearchRecipeHistory,
  useDeleteSearchUserHistory
};
