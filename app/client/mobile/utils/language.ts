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
