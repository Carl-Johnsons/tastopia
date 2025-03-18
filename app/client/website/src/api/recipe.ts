import {
  getRecipeComments,
  getRecipeReports,
  GetRecipeReportsParams
} from "@/actions/recipe.action";
import {
  IPaginatedAdminReportRecipeListResponse,
  IPaginatedRecipeCommentListResponse
} from "@/generated/interfaces/recipe.interface";
import { InfiniteData, useInfiniteQuery, useQuery } from "@tanstack/react-query";
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
