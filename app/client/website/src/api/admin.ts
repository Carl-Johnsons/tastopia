import { disableAdmin, getAdmins, restoreAdmin } from "@/actions/admin.action";
import { IPaginatedAdminListResponse } from "@/types/admin";
import { PaginatedQueryParams } from "@/types/common";
import { useMutation, useQuery } from "@tanstack/react-query";

export const useGetAdmins = ({
  limit,
  skip,
  sortBy,
  sortOrder,
  lang,
  keyword
}: PaginatedQueryParams) => {
  return useQuery<IPaginatedAdminListResponse>({
    queryKey: ["admins", skip, sortBy, sortOrder, lang, keyword, limit],
    queryFn: () =>
      getAdmins({
        limit,
        skip,
        sortBy,
        sortOrder,
        lang,
        keyword
      })
  });
};

export const useDisableAdmin = () => {
  return useMutation<void, Error, string>({
    mutationFn: id => disableAdmin(id)
  });
};

export const useRestoreAdmin = () => {
  return useMutation<void, Error, string>({
    mutationFn: id => restoreAdmin(id)
  });
};
