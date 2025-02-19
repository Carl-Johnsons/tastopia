import { SETTING_VALUE } from "@/constants/settings";
import i18next from "i18next";

type LanguageMap = {
  [key: string]: string;
};

const languageMap: LanguageMap = {
  vi: "Vietnamese",
  en: "English"
};

export const compareLanguages = (code: string, name: string) => {
  return languageMap[code] === name;
};

const LANGUAGE = {
  [SETTING_VALUE.LANGUAGE.ENGLISH]: "en",
  [SETTING_VALUE.LANGUAGE.VIETNAMESE]: "vi"
};

/**
 * Change the application's language to a specified value.
 *
 * @param lang - The language to change to.
 *
 */
export const changeLanguage = async (lang: SETTING_VALUE.LANGUAGE) => {
  await i18next.changeLanguage(LANGUAGE[lang]);
};
