import { protectedAxiosInstance } from "@/constants/host";
import { SETTING_VALUE } from "@/constants/settings";
import { IPaginatedNotificationListResponse } from "@/generated/interfaces/notification.interface";
import { selectAccessToken } from "@/slices/auth.slice";
import { selectSetting } from "@/slices/setting.slice";
import { useInfiniteQuery, useMutation } from "react-query";
import { IErrorResponseDTO } from "@/generated/interfaces/common.interface";
import { useErrorHandler } from "@/hooks/useErrorHandler";
import { NotificationCategories } from "@/generated/enums/notification.enum";

export const useGetNotification = (type: NotificationCategories) => {
  const accessToken = selectAccessToken();
  const { LANGUAGE } = selectSetting();
  const { handleError } = useErrorHandler();

  return useInfiniteQuery<IPaginatedNotificationListResponse, Error>({
    queryKey: ["getNotification", type],
    enabled: !!accessToken,
    queryFn: async ({ pageParam = 0 }) => {
      const { data } =
        await protectedAxiosInstance.get<IPaginatedNotificationListResponse>(
          "/api/notification",
          {
            params: {
              lang: LANGUAGE === SETTING_VALUE.LANGUAGE.ENGLISH ? "en" : "vi",
              skip: pageParam,
              type
            }
          }
        );
      return data;
    },
    onError: error => handleError(error),
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
      const { data } = await protectedAxiosInstance.post<SetViewedNotificationResponse>(
        "/api/notification/set-view-notification",
        { notificationId }
      );
      return data;
    }
  });
};
