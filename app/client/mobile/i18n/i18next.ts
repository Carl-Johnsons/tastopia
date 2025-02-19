import i18n from "i18next";
import { initReactI18next } from "react-i18next";

import resources from "./resourceTranslation";

i18n.use(initReactI18next).init({
  resources,
  compatibilityJSON: "v3",
  lng: "vi",
  fallbackLng: "vi"
});

export default i18n;
