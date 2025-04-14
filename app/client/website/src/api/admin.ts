import {
  disableAdmin,
  getAdminActivies,
  getAdminById,
  getAdmins,
  getCurrentAdminActivies,
  getCurrentAdminDetail,
  restoreAdmin
} from "@/actions/admin.action";
import { IPaginatedAdminActivityLogListResponse } from "@/generated/interfaces/tracking.interface";
import { IAdminDetailResponse } from "@/generated/interfaces/user.interface";
import { useErrorHandler } from "@/hooks/error/useErrorHanler";
import { PaginatedQueryParams } from "@/types/common";
import {
  InfiniteData,
  QueryKey,
  useInfiniteQuery,
  useMutation,
  useQuery,
  UseQueryOptions
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
  const { withErrorProcessor } = useErrorHandler();

  return useQuery({
    queryKey: ["admins", skip, sortBy, sortOrder, lang, keyword, limit],
    queryFn: () =>
      withErrorProcessor(() =>
        getAdmins({
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

export const useGetAdminById = (id: string, self?: boolean) => {
  const { withErrorProcessor } = useErrorHandler();

  console.log("queryKey", self ? ["currentAdmin"] : ["admin", id]);
  return useQuery({
    queryKey: self ? ["currentAdmin"] : ["admin", id],
    queryFn: () =>
      withErrorProcessor(() => (self ? getCurrentAdminDetail() : getAdminById(id)))
  });
};

export const useGetCurrentAdminDetail = (
  queryOptions: Omit<
    UseQueryOptions<
      IAdminDetailResponse | null,
      unknown,
      IAdminDetailResponse | null,
      QueryKey
    >,
    "queryKey" | "queryFn" | "initialData"
  > = {}
) => {
  const { withErrorProcessor } = useErrorHandler();

  return useQuery({
    ...queryOptions,
    queryKey: ["currentAdmin"],
    queryFn: () => withErrorProcessor(() => getCurrentAdminDetail())
  });
};

export const useDisableAdmin = () => {
  const { withErrorProcessor } = useErrorHandler();

  return useMutation<void, Error, string>({
    mutationFn: id => withErrorProcessor(() => disableAdmin(id))
  });
};

export const useRestoreAdmin = () => {
  const { withErrorProcessor } = useErrorHandler();

  return useMutation<void, Error, string>({
    mutationFn: id => withErrorProcessor(() => restoreAdmin(id))
  });
};

export const useGetAdminActivities = (
  accountId: string,
  { lang, self }: Pick<PaginatedQueryParams, "lang"> & { self?: boolean }
) => {
  const queryKey = self ? ["currentAdminActivities"] : ["adminActivities", accountId];
  const { withErrorProcessor } = useErrorHandler();

  return useInfiniteQuery<
    IPaginatedAdminActivityLogListResponse,
    AxiosError,
    InfiniteData<IPaginatedAdminActivityLogListResponse>,
    string[],
    number | undefined
  >({
    queryKey,
    initialPageParam: 0,
    queryFn: ({ pageParam }) => {
      const params = {
        skip: pageParam,
        lang
      };

      return withErrorProcessor(() =>
        self ? getCurrentAdminActivies(params) : getAdminActivies(accountId, params)
      );
    },
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
