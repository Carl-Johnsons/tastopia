import { getRecipeReports, GetRecipeReportsParams } from "@/actions/recipe.action";
import { IPaginatedAdminReportRecipeListResponse } from "@/generated/interfaces/recipe.interface";
import { useQuery } from "@tanstack/react-query";

export const useGetRecipeReports = ({
  limit,
  skip,
  sortBy,
  sortOrder,
  lang
}: GetRecipeReportsParams) => {
  return useQuery<IPaginatedAdminReportRecipeListResponse>({
    queryKey: ["users", skip, sortBy, sortOrder, lang],
    queryFn: () =>
      getRecipeReports({
        limit,
        skip,
        sortBy,
        sortOrder,
        lang
      })
  });
};
