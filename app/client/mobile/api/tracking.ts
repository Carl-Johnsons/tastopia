import { protectedAxiosInstance } from "@/constants/host";
import { VoteType } from "@/constants/recipe";
import { IAdvancePaginatedMetadata } from "@/generated/interfaces/common.interface";
import { selectAccessToken } from "@/slices/auth.slice";
import { AxiosError } from "axios";
import { useInfiniteQuery } from "react-query";

export interface IPaginatedRecipeViewingHistoryValue {
  paginatedData: IRecipeViewingHistoryResponse[];
  metadata: IAdvancePaginatedMetadata;
}

export interface IPaginatedRecipeViewingHistoryResponse {
  value: IPaginatedRecipeViewingHistoryValue;
  isFailure: boolean;
  isSuccess: boolean;
  errors: IErrorResponseDTO[];
}

export interface IErrorResponseDTO {
  status: number;
  statusCode: string | null;
  message: string | null;
}

export interface IRecipeViewingHistoryResponse {
  id: string;
  authorId: string;
  recipeImgUrl: string;
  title: string;
  description: string;
  authorDisplayName: string;
  authorAvtUrl: string;
  voteDiff: number;
  numberOfComment: number;
  vote: VoteType;
  createdAt: string;
  updatedAt: string;
}

export interface IUseGetRecipeViewingHistoryParams {
  pageParam?: number;
  keyword?: string;
}

export const useGetRecipeViewingHistory = () => {
  const accessToken = selectAccessToken();

  return useInfiniteQuery<IPaginatedRecipeViewingHistoryValue, Error>({
    queryKey: "getRecipeViewingHistory",
    enabled: !!accessToken,
    queryFn: async ({ pageParam = 0 }) => {
      try {
        const { data } =
          await protectedAxiosInstance.get<IPaginatedRecipeViewingHistoryResponse>(
            "/api/tracking/get-user-view-recipe-detail-history",
            {
              params: {
                skip: pageParam
              }
            }
          );
        return data.value;
      } catch (error) {
        const DEFAULT_ERROR_MESSAGE = "An error has occurred.";

        if (error instanceof AxiosError) {
          const data = error.response?.data as IErrorResponseDTO;
          throw new Error(data.message || DEFAULT_ERROR_MESSAGE);
        }

        throw new Error(DEFAULT_ERROR_MESSAGE);
      }
    },
    getNextPageParam: (lastPage, allPages) => {
      if (!lastPage.metadata.hasNextPage) {
        return undefined;
      }

      return allPages.length;
    }
  });
};

export const useSearchRecipeViewingHistory = (keyword?: string) => {
  const accessToken = selectAccessToken();

  return useInfiniteQuery<IPaginatedRecipeViewingHistoryValue, Error>({
    queryKey: ["searchRecipeViewingHistory", keyword],
    enabled: !!accessToken && !!keyword && keyword?.length > 0,
    queryFn: async ({ pageParam = 0 }) => {
      try {
        const { data } =
          await protectedAxiosInstance.post<IPaginatedRecipeViewingHistoryResponse>(
            "/api/tracking/search-user-view-recipe-detail-history",
            {
              skip: pageParam,
              keyword
            }
          );
        return data.value;
      } catch (error) {
        const DEFAULT_ERROR_MESSAGE = "An error has occurred.";

        if (error instanceof AxiosError) {
          const data = error.response?.data as IErrorResponseDTO;
          throw new Error(data.message || DEFAULT_ERROR_MESSAGE);
        }

        throw new Error(DEFAULT_ERROR_MESSAGE);
      }
    },
    getNextPageParam: (lastPage, allPages) => {
      if (!lastPage.metadata.hasNextPage) {
        return undefined;
      }

      return allPages.length;
    }
  });
};
