import { SETTING_VALUE } from "@/constants/settings";
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
  const changeLanguage = async (lang: SETTING_VALUE.LANGUAGE) => {
    await i18next.changeLanguage(LANGUAGE[lang]);
  };

  return { changeLanguage };
};
