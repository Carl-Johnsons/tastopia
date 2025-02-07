import { SETTING_VALUE } from "@/constants/settings";

export const getBooleanValueFromSetting = (value: SETTING_VALUE.BOOLEAN) => {
  return value === SETTING_VALUE.BOOLEAN.TRUE ? true : false;
};

export const getSettingFromBooleanValue = (value: boolean) => {
  return value ? SETTING_VALUE.BOOLEAN.TRUE : SETTING_VALUE.BOOLEAN.FALSE;
};
