import {
  disableAdmin,
  getAdminActivies,
  getAdminById,
  getAdmins,
  restoreAdmin
} from "@/actions/admin.action";
import { IPaginatedAdminActivityLogListResponse } from "@/generated/interfaces/tracking.interface";
import { PaginatedQueryParams } from "@/types/common";
import {
  InfiniteData,
  useInfiniteQuery,
  useMutation,
  useQuery
} from "@tanstack/react-query";
import { AxiosError } from "axios";

export const useGetAdmins = ({
  limit,
  skip,
  sortBy,
  sortOrder,
  lang,
  keyword
}: PaginatedQueryParams) => {
  console.log("params in useGetAdmins:", limit, skip, sortBy, sortOrder, lang, keyword);

  return useQuery({
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

export const useGetAdminById = (id: string) => {
  return useQuery({
    queryKey: ["admin", id],
    queryFn: () => getAdminById(id)
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

export const useGetAdminActivities = (
  accountId: string,
  { lang }: Pick<PaginatedQueryParams, "lang">
) => {
  return useInfiniteQuery<
    IPaginatedAdminActivityLogListResponse,
    AxiosError,
    InfiniteData<IPaginatedAdminActivityLogListResponse>,
    string[],
    number | undefined
  >({
    queryKey: ["adminActivities", accountId],
    initialPageParam: 0,
    queryFn: ({ pageParam }) =>
      getAdminActivies(accountId, {
        skip: pageParam,
        lang
      }),
    getNextPageParam: (lastPage, allPages) => {
      const totalPage = lastPage.metadata?.totalPage;
      const currentPage = lastPage.metadata?.currentPage;

      if (!totalPage || !currentPage) return undefined;
      const hasNextPage = currentPage < totalPage;

      if (!hasNextPage) return undefined;
      return allPages.length;
    }
  });
};
