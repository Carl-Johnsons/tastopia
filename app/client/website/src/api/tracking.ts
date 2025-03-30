import { getActivityLogs } from "@/actions/tracking.aciton";
import { PaginatedQueryParams } from "@/types/common";
import { useQuery } from "@tanstack/react-query";

export const useGetActivityLogs = ({
  limit,
  skip,
  sortBy,
  sortOrder,
  lang,
  keyword
}: PaginatedQueryParams) => {
  return useQuery({
    queryKey: ["activityLogs", skip, sortBy, sortOrder, lang, keyword, limit],
    queryFn: () =>
      getActivityLogs({
        limit,
        skip,
        sortBy,
        sortOrder,
        lang,
        keyword
      })
  });
};
