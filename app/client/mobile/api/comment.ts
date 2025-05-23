import { protectedAxiosInstance } from "@/constants/host";
import { useInfiniteQuery, useMutation } from "react-query";

const useGetRecipeComment = (id: string) => {
  return useInfiniteQuery<GetRecipeCommentResponse>({
    queryKey: ["comments", id],
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

const useCreateComment = () => {
  return useMutation<CommentResponseType, Error, CreateCommentPayloadType>({
    mutationFn: async payload => {
      const { data } = await protectedAxiosInstance.post(
        "/api/recipe/comment-recipe",
        payload
      );
      return data;
    }
  });
};

export { useGetRecipeComment, useCreateComment };
