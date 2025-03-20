import { getAdminUsers, getUserById, getUserReports } from "@/actions/user.action";
import {
  IPaginatedAdminGetUserListResponse,
  IPaginatedAdminUserReportListResponse
} from "@/generated/interfaces/user.interface";
import { useQuery } from "@tanstack/react-query";
import { useLocale } from "next-intl";

export const useGetAdminUsers = (
  skip = 0,
  sortBy = "",
  sortOrder = "asc",
  keyword = "",
  limit = 6
) => {
  return useQuery<IPaginatedAdminGetUserListResponse>({
    queryKey: ["users", skip, sortBy, sortOrder, keyword, limit],
    queryFn: () => getAdminUsers(skip, sortBy, sortOrder, keyword, limit)
  });
};

export const useGetUserById = (userId: string) => {
  return useQuery({
    queryKey: ["user", userId],
    queryFn: () => getUserById(userId)
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
      getUserReports(skip, sortBy, sortOrder, keyword, limit, language || currentLanguage)
  });
};
