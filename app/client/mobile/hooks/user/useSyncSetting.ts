import { UpdateSettingParams, useGetUserSettings, useUpdateSetting } from "@/api/user";
import { SETTING_KEY } from "@/constants/settings";
import { saveSettingData, selectSetting } from "@/slices/setting.slice";
import { useCallback, useState } from "react";
import { useDispatch } from "react-redux";

type UpdateSettingStatus = "todo" | "loading" | "success" | "error";

const useSyncSetting = () => {
  const [isLoading, setIsLoading] = useState(false);
  const [status, setStatus] = useState<UpdateSettingStatus>("todo");
  const setting = selectSetting();
  const { mutateAsync } = useUpdateSetting();
  const getUserSettings = useGetUserSettings();
  const dispatch = useDispatch();

  const KEYS: Array<SETTING_KEY> = [
    SETTING_KEY.LANGUAGE,
    SETTING_KEY.DARK_MODE,
    SETTING_KEY.NOTIFICATION_COMMENT,
    SETTING_KEY.NOTIFICATION_VOTE,
    SETTING_KEY.NOTIFICATION_FOLLOW
  ];

  /**
   * Upload the local user's settings to the server.
   */
  const upload = useCallback(async () => {
    setIsLoading(true);
    setStatus("loading");

    const data: UpdateSettingParams = {
      settings: KEYS.map(key => ({ key: key, value: setting[key] }))
    };

    await mutateAsync(
      { ...data },
      {
        onSuccess: () => {
          setStatus("success");
        },
        onError: () => {
          setStatus("error");
        },
        onSettled: () => {
          setIsLoading(false);
        }
      }
    );
  }, [setting]);

  /**
   * Fetch the user's settings and then save it.
   */
  const fetch = useCallback(async () => {
    setIsLoading(true);
    setStatus("loading");

    try {
      const { data: settings } = await getUserSettings.refetch({ throwOnError: true });
      const UNION_SETTING: any = {};

      settings?.map(item => {
        const key = item.setting.code;
        const value = item.settingValue;

        UNION_SETTING[key] = value;
      });

      dispatch(saveSettingData(UNION_SETTING));
      setStatus("success");
    } catch (error) {
      setStatus("error");
    } finally {
      setIsLoading(false);
    }
  }, [getUserSettings, dispatch, saveSettingData]);

  return { upload, fetch, isLoading, status };
};

export default useSyncSetting;
