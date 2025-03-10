import { useAxios } from "@/hooks/api/useAxios";
import { useSelectAccessToken } from "@/slices/auth.slice";
import { UserState } from "@/slices/user.slice";
import { useQuery } from "@tanstack/react-query";
import { AxiosError } from "axios";

export type GetUserDetailsResponse = UserState;

export const useGetUserDetails = () => {
  const accessToken = useSelectAccessToken();
  const { protectedAxiosInstance } = useAxios();

  return useQuery<GetUserDetailsResponse, Error>({
    queryKey: ["getUserDetails"],
    enabled: !!accessToken,
    queryFn: async () => {
      const url = "/api/user/get-current-user-details";

      try {
        const { data } = await protectedAxiosInstance.get(url);
        return data as GetUserDetailsResponse;
      } catch (error) {
        if (error instanceof AxiosError) {
          const data = error.response?.data as IErrorResponseDTO;
          throw new Error(data.message);
        }

        throw new Error("An error has occurred.");
      }
    }
  });
};
