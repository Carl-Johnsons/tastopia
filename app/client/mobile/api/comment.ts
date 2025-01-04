import { protectedAxiosInstance } from "@/constants/host";
import { useInfiniteQuery, useQuery } from "react-query";

const useGetRecipeComment = (id: string) => {
  console.log("id", id);
  return useInfiniteQuery<GetRecipeCommentResponse>({
    queryKey: ["recipes", id],
    queryFn: async ({ pageParam = 0 }) => {
      const { data } = await protectedAxiosInstance.post<GetRecipeCommentResponse>(
        "/api/recipe/get-recipe-comments",
        {
          recipeId: id,
          skip: pageParam.toString()
        }
      );
      return data;
    },
    getNextPageParam: (lastPage, pages) => {
      if (!lastPage.metadata.hasNextPage) {
        return undefined;
      }
      return pages.length;
    }
  });
};

export { useGetRecipeComment };
