import { formatDistanceToNow } from "date-fns";
import { enUS, vi } from "date-fns/locale";
import { Locale } from "next-intl";
import { useMemo } from "react";

export const useFormattedDistance = (date: string, locale: Locale) => {
  return useMemo(() => {
    return formatDistanceToNow(new Date(date), {
      locale: locale === "en" ? enUS : vi
    });
  }, [date, locale]);
};

export default useFormattedDistance;
