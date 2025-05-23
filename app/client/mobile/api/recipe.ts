import { useInfiniteQuery, useMutation, useQuery, useQueryClient } from "react-query";
import { protectedAxiosInstance } from "@/constants/host";
import {
  BookMarkRecipeResponse,
  CreateRecipePayloadType,
  ICommentResponse,
  RecipeDetailResponse,
  RecipeResponse,
  RecipeStep
} from "@/types/recipe";
import {
  IRecipe,
  IUserReportCommentDTO,
  IUserReportRecipeDTO,
  IUserReportRecipeResponse
} from "@/generated/interfaces/recipe.interface";

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

const useRecipesFeedByAuthorId = (authorId: string) => {
  return useInfiniteQuery<RecipeResponse>({
    queryKey: ["recipesByAuthorId", authorId],
    queryFn: async ({ pageParam = 0 }) => {
      const { data } = await protectedAxiosInstance.post<RecipeResponse>(
        "/api/recipe/get-recipe-feed-by-author-id",
        {
          skip: pageParam.toString(),
          authorId: authorId
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

const useCommentsByAuthorId = (accountId: string) => {
  return useInfiniteQuery<ICommentResponse>({
    queryKey: ["commentsByAuthorId", accountId],
    queryFn: async ({ pageParam = 0 }) => {
      const { data } = await protectedAxiosInstance.post<ICommentResponse>(
        "/api/recipe/get-account-recipe-comments",
        {
          skip: pageParam.toString(),
          accountId: accountId
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

const useRecipeDetail = (recipeId: string) => {
  const queryClient = useQueryClient();

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
    enabled: !!recipeId,
    onSuccess: async () => {
      await queryClient.invalidateQueries({ queryKey: ["getRecipeViewingHistory"] });
      await queryClient.invalidateQueries({ queryKey: ["searchRecipeViewingHistory"] });
    }
  });
};

const useRecipeSteps = (recipeId: string) => {
  return useQuery<RecipeStep[]>({
    queryKey: ["recipeSteps", recipeId],
    queryFn: async () => {
      const { data } = await protectedAxiosInstance.post<RecipeStep[]>(
        "/api/recipe/get-recipe-steps",
        {
          recipeId
        }
      );
      return data;
    },
    enabled: !!recipeId
  });
};

const useCreateRecipe = () => {
  return useMutation<IRecipe, Error, CreateRecipePayloadType>({
    mutationFn: async payload => {
      const { data } = await protectedAxiosInstance.post(
        "/api/recipe/create-recipe",
        payload,
        {
          headers: {
            "Content-Type": "multipart/form-data"
          }
        }
      );
      return data;
    }
  });
};

const useBookmarkRecipe = () => {
  return useMutation<BookMarkRecipeResponse, Error, { recipeId: string }>({
    mutationFn: async ({ recipeId }) => {
      const { data } = await protectedAxiosInstance.post("/api/recipe/bookmark-recipe", {
        recipeId: recipeId
      });
      return data;
    }
  });
};

const useVoteRecipe = () => {
  return useMutation<any, Error, { recipeId: string; isUpvote: boolean }>({
    mutationFn: async ({ recipeId, isUpvote }) => {
      const { data } = await protectedAxiosInstance.post("/api/recipe/vote-recipe", {
        recipeId: recipeId,
        isUpvote: isUpvote
      });
      return data;
    }
  });
};

const useDeleteOwnRecipe = () => {
  return useMutation<any, Error, { recipeId: string }>({
    mutationFn: async ({ recipeId }) => {
      const { data } = await protectedAxiosInstance.post(
        "/api/recipe/delete-own-recipe",
        {
          recipeId: recipeId
        }
      );
      return data;
    }
  });
};

const useReportRecipeCommentReason = (language: string, reportType: string) => {
  return useQuery<ReportRecipeCommentReasonResponse>({
    queryKey: ["reportRecipeReason", language, reportType],
    queryFn: async () => {
      const { data } =
        await protectedAxiosInstance.post<ReportRecipeCommentReasonResponse>(
          "/api/recipe/get-report-reasons",
          {
            language,
            reportType
          }
        );
      return data;
    },
    enabled: !!language && !!reportType
  });
};

const useReportRecipe = () => {
  return useMutation<IUserReportRecipeResponse, Error, IUserReportRecipeDTO>({
    mutationFn: async ({ recipeId, reasonCodes, additionalDetails }) => {
      const { data } = await protectedAxiosInstance.post(
        "/api/recipe/user-report-recipe",
        {
          recipeId,
          reasonCodes,
          additionalDetails
        }
      );
      return data;
    }
  });
};

const useReportComment = () => {
  return useMutation<IUserReportRecipeResponse, Error, IUserReportCommentDTO>({
    mutationFn: async ({ commentId, recipeId, reasonCodes, additionalDetails }) => {
      const { data } = await protectedAxiosInstance.post(
        "/api/recipe/user-report-comment",
        {
          commentId,
          recipeId,
          reasonCodes,
          additionalDetails
        }
      );
      return data;
    }
  });
};

type UpdateComment = {
  content: string;
  commentId: string;
};

const useUpdateComment = () => {
  return useMutation<any, Error, UpdateComment>({
    mutationFn: async ({ commentId, content }) => {
      const { data } = await protectedAxiosInstance.post("/api/recipe/update-comment", {
        commentId,
        content
      });
      return data;
    }
  });
};

const useDeleteComment = () => {
  return useMutation<any, Error, { commentId: string }>({
    mutationFn: async ({ commentId }) => {
      const { data } = await protectedAxiosInstance.post("/api/recipe/delete-comment", {
        commentId: commentId
      });
      return data;
    }
  });
};

const useGetBookmarks = () => {
  return useInfiniteQuery<RecipeResponse>({
    queryKey: ["bookmarks"],
    queryFn: async ({ pageParam = 0 }) => {
      const { data } = await protectedAxiosInstance.post<RecipeResponse>(
        "/api/recipe/get-recipe-bookmarks",
        {
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

const useGetDeletedRecipe = () => {
  return useInfiniteQuery<RecipeResponse>({
    queryKey: ["deletedRecipes"],
    queryFn: async ({ pageParam = 0 }) => {
      const { data } = await protectedAxiosInstance.post<RecipeResponse>(
        "/api/recipe/get-recipe-bin",
        {
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

const useRestoreOwnRecipe = () => {
  return useMutation<any, Error, { recipeId: string }>({
    mutationFn: async ({ recipeId }) => {
      const { data } = await protectedAxiosInstance.post(
        "/api/recipe/restore-own-recipe",
        {
          recipeId: recipeId
        }
      );
      return data;
    }
  });
};

export {
  useRecipesFeed,
  useRecipesFeedByAuthorId,
  useCommentsByAuthorId,
  useRecipeDetail,
  useRecipeSteps,
  useCreateRecipe,
  useBookmarkRecipe,
  useVoteRecipe,
  useDeleteOwnRecipe,
  useReportRecipeCommentReason,
  useReportRecipe,
  useReportComment,
  useUpdateComment,
  useDeleteComment,
  useGetBookmarks,
  useGetDeletedRecipe,
  useRestoreOwnRecipe
};
