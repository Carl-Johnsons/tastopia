import {
    disableRecipe,
  getRecipeComments,
  getRecipeReports,
  GetRecipeReportsParams,
  markReportAsCompleted,
  reopenReport,
  restoreRecipe
} from "@/actions/recipe.action";
import {
  IPaginatedAdminReportRecipeListResponse,
  IPaginatedRecipeCommentListResponse,
  IReportDTO
} from "@/generated/interfaces/recipe.interface";
import {
  InfiniteData,
  useInfiniteQuery,
  useMutation,
  useQuery
} from "@tanstack/react-query";
import { AxiosError } from "axios";

export const useGetRecipeReports = ({
  limit,
  skip,
  sortBy,
  sortOrder,
  lang,
  keyword
}: GetRecipeReportsParams) => {
  return useQuery<IPaginatedAdminReportRecipeListResponse>({
    queryKey: ["reports", skip, sortBy, sortOrder, lang, keyword, limit],
    queryFn: () =>
      getRecipeReports({
        limit,
        skip,
        sortBy,
        sortOrder,
        lang,
        keyword
      })
  });
};

export const useGetRecipeComments = (recipeId: string) => {
  return useInfiniteQuery<
    IPaginatedRecipeCommentListResponse,
    AxiosError,
    InfiniteData<IPaginatedRecipeCommentListResponse>,
    string[],
    number | undefined
  >({
    queryKey: ["comments", recipeId],
    initialPageParam: 0,
    queryFn: ({ pageParam }) =>
      getRecipeComments({
        recipeId,
        options: {
          skip: pageParam
        }
      }),
    getNextPageParam: (lastPage, allPages) => {
      if (!lastPage.metadata?.hasNextPage) return undefined;
      return allPages.length;
    }
  });
};

export const useReopenReport = () => {
  return useMutation<void, Error, IReportDTO>({
    mutationFn: (params) => reopenReport(params)
  });
};

export const useMarkReportAsCompleted = () => {
  return useMutation<void, Error, IReportDTO>({
    mutationFn: (params) => markReportAsCompleted(params)
  });
};

export const useDisableRecipe = () => {
  return useMutation<void, Error, string>({
    mutationFn: (id) => disableRecipe(id)
  });
}

export const useRestoreRecipe = () => {
  return useMutation<void, Error, string>({
    mutationFn: (id) => restoreRecipe(id)
  });
}

