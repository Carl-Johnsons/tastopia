import { protectedAxiosInstance } from "@/constants/host";
import { SETTING_VALUE } from "@/constants/settings";
import { IPaginatedNotificationListResponse } from "@/generated/interfaces/notification.interface";
import { selectAccessToken } from "@/slices/auth.slice";
import { selectSetting } from "@/slices/setting.slice";
import { AxiosError } from "axios";
import { useInfiniteQuery, useMutation } from "react-query";
import { IErrorResponseDTO } from "@/generated/interfaces/common.interface";

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
          throw new Error(data.message || "An error has occurred.");
        }

        throw new Error("An error has occurred.");
      }
    },
    getNextPageParam: (lastPage, allPages) => {
      if (!lastPage.metadata?.hasNextPage) {
        return undefined;
      }
      return allPages.length;
    }
  });
};

type SetViewedNotificationResponse =
  | {
      recipientId: string;
      isViewed: boolean;
    }
  | IErrorResponseDTO;

type SetViewedNotificationParams = {
  notificationId: string;
};

export const useSetViewedNotification = () => {
  return useMutation<SetViewedNotificationResponse, Error, SetViewedNotificationParams>({
    mutationKey: "getNotification",
    mutationFn: async ({ notificationId }) => {
      try {
        const { data } = await protectedAxiosInstance.post<SetViewedNotificationResponse>(
          "/api/notification/set-view-notification",
          { notificationId }
        );
        return data;
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
