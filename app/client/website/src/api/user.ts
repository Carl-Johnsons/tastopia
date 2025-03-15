import { getAdminUsers } from "@/actions/user.action";
import { IPaginatedAdminGetUserListResponse } from "@/generated/interfaces/user.interface";
import { useQuery } from "@tanstack/react-query";

export const useGetAdminUsers = (skip = 0, sortBy = "", sortOrder = "asc", keyword = "", limit = 6) => {
  return useQuery<IPaginatedAdminGetUserListResponse>({
    queryKey: ["users", skip, sortBy, sortOrder, keyword, limit],
    queryFn: () =>  getAdminUsers(skip, sortBy, sortOrder, keyword, limit),
  });
};