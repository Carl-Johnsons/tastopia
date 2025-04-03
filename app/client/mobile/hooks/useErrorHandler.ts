import { handleError as baseHanlder } from "@/lib/error/errorHanler";
import { useTranslation } from "react-i18next";

export const useErrorHandler = () => {
  const { t } = useTranslation("error");

  const handleError = (error: any) => {
    baseHanlder(error, t);
  };

  return { handleError };
};
