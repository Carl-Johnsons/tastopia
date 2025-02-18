import { SETTING_KEY, SETTING_VALUE } from "@/constants/settings";
import { useAppSelector } from "@/store/hooks";
import { createSlice } from "@reduxjs/toolkit";
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
