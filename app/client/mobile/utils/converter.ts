import { SETTING_VALUE } from "@/slices/setting.slice";

export const getBooleanValueFromSetting = (value: SETTING_VALUE.BOOLEAN) => {
  return value === SETTING_VALUE.BOOLEAN.TRUE ? true : false;
} 

export const getSettingFromBooleanValue = (value: boolean) => {
  return value ? SETTING_VALUE.BOOLEAN.TRUE : SETTING_VALUE.BOOLEAN.FALSE;
} 

