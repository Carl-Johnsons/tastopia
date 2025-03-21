import { getTags } from "@/actions/tag.action";
import { IPaginatedTagResponse } from "@/types/tag";
import { useQuery } from "@tanstack/react-query";

export const useGetTags = (
  skip = 0,
  sortBy = "",
  sortOrder = "asc",
  keyword = "",
  limit = 6
) => {
  return useQuery<IPaginatedTagResponse>({
    queryKey: ["users", skip, sortBy, sortOrder, keyword, limit],
    queryFn: () => getTags(skip, sortBy, sortOrder, keyword, limit)
  });
};
