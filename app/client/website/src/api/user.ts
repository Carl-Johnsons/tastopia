import {
  getAdminUsers,
  getUserById,
  getUserDetailReports,
  getUserReports
} from "@/actions/user.action";
import {
  IPaginatedAdminGetUserListResponse,
  IPaginatedAdminUserReportListResponse
} from "@/generated/interfaces/user.interface";
import { useErrorHandler } from "@/hooks/error/useErrorHanler";
import { IInfiniteAdminUserReportListResponse } from "@/types/user";
import { useInfiniteQuery, useQuery } from "@tanstack/react-query";
import { useLocale } from "next-intl";

export const useGetAdminUsers = (
  skip = 0,
  sortBy = "",
  sortOrder = "asc",
  keyword = "",
  limit = 6
) => {
  const { withErrorProcessor } = useErrorHandler();

  return useQuery<IPaginatedAdminGetUserListResponse>({
    queryKey: ["users", skip, sortBy, sortOrder, keyword, limit],
    queryFn: () =>
      withErrorProcessor(() => getAdminUsers(skip, sortBy, sortOrder, keyword, limit))
  });
};

export const useGetUserById = (userId: string) => {
  const { withErrorProcessor } = useErrorHandler();

  return useQuery({
    queryKey: ["user", userId],
    queryFn: () => withErrorProcessor(() => getUserById(userId))
  });
};

export const useGetUserReports = (
  skip = 0,
  sortBy = "",
  sortOrder = "asc",
  keyword = "",
  limit = 6,
  language = ""
) => {
  const currentLanguage = useLocale();
  const { withErrorProcessor } = useErrorHandler();

  return useQuery<IPaginatedAdminUserReportListResponse>({
    queryKey: [
      "userReports",
      skip,
      sortBy,
      sortOrder,
      keyword,
      limit,
      language || currentLanguage
    ],
    queryFn: () =>
      withErrorProcessor(() =>
        getUserReports(
          skip,
          sortBy,
          sortOrder,
          keyword,
          limit,
          language || currentLanguage
        )
      )
  });
};

export const useGetUserDetailReports = (accountId: string, language?: string) => {
  const currentLanguage = useLocale();
  const { withErrorProcessor } = useErrorHandler();

  return useInfiniteQuery<IInfiniteAdminUserReportListResponse>({
    queryKey: ["userReports", accountId, language || currentLanguage],
    queryFn: async ({ pageParam = 0 }) => {
      return await withErrorProcessor(() =>
        getUserDetailReports(accountId, language || currentLanguage, pageParam as number)
      );
    },
    initialPageParam: 0,
    getNextPageParam: (lastPage, pages) => {
      if (!lastPage.metadata?.hasNextPage) {
        return undefined;
      }
      return pages.length;
    }
  });
};
