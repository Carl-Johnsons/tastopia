import { protectedAxiosInstance } from "@/constants/host";
import { SETTING_VALUE } from "@/constants/settings";
import { selectAccessToken } from "@/slices/auth.slice";
import { selectSetting } from "@/slices/setting.slice";
import { AxiosError } from "axios";
import { useQuery } from "react-query";

export const useGetNotification = () => {
  const accessToken = selectAccessToken();
  const { LANGUAGE } = selectSetting();
  const SKIP = 0;

  return useQuery<IPaginatedNotificationListResponse, Error>({
    queryKey: "getNotification",
    enabled: !!accessToken,
    queryFn: async () => {
      const url = `/api/notification?lang=${LANGUAGE === SETTING_VALUE.LANGUAGE.ENGLISH ? "en" : "vi"}&skip=${SKIP}`;

      try {
        const { data } =
          await protectedAxiosInstance.get<IPaginatedNotificationListResponse>(url);
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
