import { useLocale } from "next-intl";

const useLocaleTable = () => {
  const currentLanguage = useLocale();
  return currentLanguage === "vi"
    ? {
        rowsPerPageText: "Số dòng trên trang",
        rangeSeparatorText: "trên"
      }
    : {
        rowsPerPageText: "Rows per page",
        rangeSeparatorText: "of"
      };
};

export default useLocaleTable;
