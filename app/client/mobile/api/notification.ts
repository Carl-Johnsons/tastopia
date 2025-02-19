import { protectedAxiosInstance } from "@/constants/host";
import { SETTING_VALUE } from "@/constants/settings";
import { IPaginatedNotificationListResponse } from "@/generated/interfaces/notification.interface";
import { selectAccessToken } from "@/slices/auth.slice";
import { selectSetting } from "@/slices/setting.slice";
import { stringify } from "@/utils/debug";
import { AxiosError } from "axios";
import { useInfiniteQuery } from "react-query";

export const useGetNotification = () => {
  const accessToken = selectAccessToken();
  const { LANGUAGE } = selectSetting();

  return useInfiniteQuery<IPaginatedNotificationListResponse, Error>({
    queryKey: "getNotification",
    enabled: !!accessToken,
    queryFn: async ({ pageParam = 0 }) => {
      try {
        const { data } =
          await protectedAxiosInstance.get<IPaginatedNotificationListResponse>(
            "/api/notification",
            {
              params: {
                lang: LANGUAGE === SETTING_VALUE.LANGUAGE.ENGLISH ? "en" : "vi",
                skip: pageParam
              }
            }
          );
        return data;
      } catch (error) {
        if (error instanceof AxiosError) {
          const data = error.response?.data as IErrorResponseDTO;
          throw new Error(data.message);
        }

        throw new Error("An error has occurred.");
      }
    },
    getNextPageParam: (lastPage, allPages) => {
      if (!lastPage.metadata.hasNextPage) {
        return undefined;
      }

      console.log(allPages, stringify(allPages));

      return allPages.length;
    }
  });
};
