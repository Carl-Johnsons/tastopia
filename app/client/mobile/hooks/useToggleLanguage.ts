import {SETTING_VALUE} from "@/slices/setting.slice";
import i18next from "i18next";

const LANGUAGE = {
  [SETTING_VALUE.LANGUAGE.ENGLISH] : "en",
  [SETTING_VALUE.LANGUAGE.VIETNAMESE] : "vi",
}

export const useChangeLanguage = () => {
  /**
   * Change the application's language to a specified value.
   *
   * @param lang - The language to change to.
   *
   */
  const changeLanguage = (lang: SETTING_VALUE.LANGUAGE) => {
    i18next.changeLanguage(LANGUAGE[lang]);
  };

  return { changeLanguage };
};
