import { useAppSelector } from "@/store/hooks";
import { getSettingFromBooleanValue } from "@/utils/converter";
import { createSlice } from "@reduxjs/toolkit";
import { Appearance } from "react-native";
import { PURGE } from "redux-persist";

export enum SETTING_KEY {
  LANGUAGE = 'LANGUAGE',
  DARK_MODE = 'DARK_MODE',
  NOTIFICATION_COMMENT = 'NOTIFICATION_COMMENT',
  NOTIFICATION_VOTE = 'NOTIFICATION_VOTE',
  NOTIFICATION_FOLLOW = 'NOTIFICATION_FOLLOW',
}

export namespace SETTING_VALUE {
  export enum LANGUAGE {
    VIETNAMESE = 'VIETNAMESE',
    ENGLISH = 'ENGLISH',
  }

  export enum BOOLEAN {
    TRUE = 'TRUE',
    FALSE = 'FALSE',
  }
}

export type SettingState = {
  [SETTING_KEY.LANGUAGE]: SETTING_VALUE.LANGUAGE;
  [SETTING_KEY.DARK_MODE]: SETTING_VALUE.BOOLEAN;
  [SETTING_KEY.NOTIFICATION_COMMENT]: SETTING_VALUE.BOOLEAN;
  [SETTING_KEY.NOTIFICATION_VOTE]: SETTING_VALUE.BOOLEAN;
  [SETTING_KEY.NOTIFICATION_FOLLOW]: SETTING_VALUE.BOOLEAN;
};

const isDarkMode = Appearance.getColorScheme() === "dark";

export const initialSettingState: SettingState = {
  [SETTING_KEY.LANGUAGE]: SETTING_VALUE.LANGUAGE.VIETNAMESE,
  [SETTING_KEY.DARK_MODE]: getSettingFromBooleanValue(isDarkMode),
  [SETTING_KEY.NOTIFICATION_COMMENT]: SETTING_VALUE.BOOLEAN.FALSE,
  [SETTING_KEY.NOTIFICATION_VOTE]: SETTING_VALUE.BOOLEAN.FALSE,
  [SETTING_KEY.NOTIFICATION_FOLLOW]: SETTING_VALUE.BOOLEAN.FALSE,
};

export const selectSetting = () => useAppSelector(state => state.setting);
export const selectLanguageSetting = () => useAppSelector(state => state.setting[SETTING_KEY.LANGUAGE]);
export const selectDarkModeSetting = () => useAppSelector(state => state.setting[SETTING_KEY.DARK_MODE]);
export const selectNotificationCommentSetting = () => useAppSelector(state => state.setting[SETTING_KEY.NOTIFICATION_COMMENT]);
export const selectNotificationVoteSetting = () => useAppSelector(state => state.setting[SETTING_KEY.NOTIFICATION_VOTE]);
export const selectNotificationFollowSetting = () => useAppSelector(state => state.setting[SETTING_KEY.NOTIFICATION_FOLLOW]);

export const settingSlice = createSlice({
  name: "setting",
  initialState: initialSettingState,
  reducers: {
    saveSettingData: (state, action) => {
      Object.assign(state, action.payload);
    }
  },
  extraReducers: builder => {
    builder.addCase(PURGE, () => {
      return initialSettingState;
    });
  }
});

export const { saveSettingData } = settingSlice.actions;

export default settingSlice.reducer;
