import { SETTING_KEY, SETTING_VALUE } from "@/constants/settings";
import { useAppSelector } from "@/store/hooks";
import { getBooleanValueFromSetting } from "@/utils/converter";
import { changeLanguage } from "@/utils/language";
import { createSlice } from "@reduxjs/toolkit";
import { Appearance } from "react-native";
import { PURGE } from "redux-persist";

export type SettingState = {
  [SETTING_KEY.LANGUAGE]: SETTING_VALUE.LANGUAGE;
  [SETTING_KEY.DARK_MODE]: SETTING_VALUE.BOOLEAN;
  [SETTING_KEY.NOTIFICATION_COMMENT]: SETTING_VALUE.BOOLEAN;
  [SETTING_KEY.NOTIFICATION_VOTE]: SETTING_VALUE.BOOLEAN;
  [SETTING_KEY.NOTIFICATION_FOLLOW]: SETTING_VALUE.BOOLEAN;
};

export const initialSettingState: SettingState = {
  [SETTING_KEY.LANGUAGE]: SETTING_VALUE.LANGUAGE.ENGLISH,
  [SETTING_KEY.DARK_MODE]: SETTING_VALUE.BOOLEAN.FALSE,
  [SETTING_KEY.NOTIFICATION_COMMENT]: SETTING_VALUE.BOOLEAN.FALSE,
  [SETTING_KEY.NOTIFICATION_VOTE]: SETTING_VALUE.BOOLEAN.FALSE,
  [SETTING_KEY.NOTIFICATION_FOLLOW]: SETTING_VALUE.BOOLEAN.FALSE
};

export const selectSetting = () => useAppSelector(state => state.setting);
export const selectLanguageSetting = () =>
  useAppSelector(state => state.setting[SETTING_KEY.LANGUAGE]);
export const selectDarkModeSetting = () =>
  useAppSelector(state => state.setting[SETTING_KEY.DARK_MODE]);
export const selectNotificationCommentSetting = () =>
  useAppSelector(state => state.setting[SETTING_KEY.NOTIFICATION_COMMENT]);
export const selectNotificationVoteSetting = () =>
  useAppSelector(state => state.setting[SETTING_KEY.NOTIFICATION_VOTE]);
export const selectNotificationFollowSetting = () =>
  useAppSelector(state => state.setting[SETTING_KEY.NOTIFICATION_FOLLOW]);

type SaveSettingDataAction = {
  type: string;
  payload: Partial<SettingState>;
};

export const settingSlice = createSlice({
  name: "setting",
  initialState: initialSettingState,
  reducers: {
    saveSettingData: (state, action: SaveSettingDataAction) => {
      Object.assign(state, action.payload);

      const { LANGUAGE, DARK_MODE } = action.payload;
      console.log("Setting saved:", { LANGUAGE, DARK_MODE });

      LANGUAGE && changeLanguage(LANGUAGE);
      DARK_MODE &&
        Appearance.setColorScheme(
          getBooleanValueFromSetting(DARK_MODE) ? "dark" : "light"
        );
    }
  },
  extraReducers: builder => {
    builder.addCase(PURGE, () => {
      const LANGUAGE = initialSettingState[SETTING_KEY.LANGUAGE];
      const DARK_MODE = initialSettingState[SETTING_KEY.DARK_MODE];

      changeLanguage(LANGUAGE);
      Appearance.setColorScheme(getBooleanValueFromSetting(DARK_MODE) ? "dark" : "light");

      return initialSettingState;
    });
  }
});

export const { saveSettingData } = settingSlice.actions;

export default settingSlice.reducer;
