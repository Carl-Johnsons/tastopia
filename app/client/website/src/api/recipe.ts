import {
  disableRecipe,
  getRecipeComments,
  getRecipeReportById,
  GetRecipeReportDetailParams,
  getRecipeReports,
  getRecipes,
  MarkAllRecipeReportParams,
  markAllReports,
  markReportAsCompleted,
  reopenReport,
  restoreRecipe
} from "@/actions/recipe.action";
import {
  IAdminReportRecipeDetailResponse,
  IPaginatedAdminRecipeListResponse,
  IPaginatedAdminReportRecipeListResponse,
  IPaginatedRecipeCommentListResponse,
  IReportDTO
} from "@/generated/interfaces/recipe.interface";
import { useErrorHandler } from "@/hooks/error/useErrorHanler";
import { ApiError } from "@/lib/error/errorHanler";
import { PaginatedQueryParams } from "@/types/common";
import {
  InfiniteData,
  useInfiniteQuery,
  useMutation,
  useQuery
} from "@tanstack/react-query";
import { AxiosError } from "axios";

export const useGetRecipes = ({
  limit,
  skip,
  sortBy,
  sortOrder,
  lang,
  keyword
}: PaginatedQueryParams) => {
  const { withErrorProcessor } = useErrorHandler();

  return useQuery<IPaginatedAdminRecipeListResponse>({
    queryKey: ["recipes", skip, sortBy, sortOrder, lang, keyword, limit],
    queryFn: () =>
      withErrorProcessor(() =>
        getRecipes({
          limit,
          skip,
          sortBy,
          sortOrder,
          lang,
          keyword
        })
      )
  });
};

export const useGetRecipeReports = ({
  limit,
  skip,
  sortBy,
  sortOrder,
  lang,
  keyword
}: PaginatedQueryParams) => {
  const { withErrorProcessor } = useErrorHandler();

  return useQuery<IPaginatedAdminReportRecipeListResponse>({
    queryKey: ["recipeReports", skip, sortBy, sortOrder, lang, keyword, limit],
    queryFn: () =>
      withErrorProcessor(() =>
        getRecipeReports({
          limit,
          skip,
          sortBy,
          sortOrder,
          lang,
          keyword
        })
      )
  });
};

export const useGetRecipeComments = (recipeId: string) => {
  const { withErrorProcessor } = useErrorHandler();

  return useInfiniteQuery<
    IPaginatedRecipeCommentListResponse,
    AxiosError,
    InfiniteData<IPaginatedRecipeCommentListResponse>,
    string[],
    number | undefined
  >({
    queryKey: ["recipeComments", recipeId],
    initialPageParam: 0,
    queryFn: ({ pageParam }) =>
      withErrorProcessor(() =>
        getRecipeComments({
          recipeId,
          options: {
            skip: pageParam
          }
        })
      ),
    getNextPageParam: (lastPage, allPages) => {
      if (!lastPage.metadata?.hasNextPage) return undefined;
      return allPages.length;
    }
  });
};

export const useReopenReport = () => {
  const { withErrorProcessor } = useErrorHandler();

  return useMutation<void, ApiError, IReportDTO>({
    mutationFn: params => withErrorProcessor(() => reopenReport(params))
  });
};

export const useReopenAllReports = () => {
  const { withErrorProcessor } = useErrorHandler();

  return useMutation<void, ApiError, IReportDTO>({
    mutationFn: params => withErrorProcessor(() => reopenReport(params))
  });
};

export const useMarkReportAsCompleted = () => {
  const { withErrorProcessor } = useErrorHandler();

  return useMutation<void, ApiError, IReportDTO>({
    mutationFn: params => withErrorProcessor(() => markReportAsCompleted(params))
  });
};

export const useMarkAllReport = () => {
  const { withErrorProcessor } = useErrorHandler();

  return useMutation<void, ApiError, MarkAllRecipeReportParams>({
    mutationFn: params => withErrorProcessor(() => markAllReports(params))
  });
};

export const useDisableRecipe = () => {
  const { withErrorProcessor } = useErrorHandler();

  return useMutation<void, ApiError, string>({
    mutationFn: id => withErrorProcessor(() => disableRecipe(id))
  });
};

export const useRestoreRecipe = () => {
  const { withErrorProcessor } = useErrorHandler();

  return useMutation<void, ApiError, string>({
    mutationFn: id => withErrorProcessor(() => restoreRecipe(id))
  });
};

export const useGetRecipeReport = (params: GetRecipeReportDetailParams) => {
  const { withErrorProcessor } = useErrorHandler();
  const { options } = params;
  const { lang } = options || {};

  return useQuery<IAdminReportRecipeDetailResponse>({
    queryKey: ["recipeReport", params.recipeId, lang],
    queryFn: () => withErrorProcessor(() => getRecipeReportById(params))
  });
};
