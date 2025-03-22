import { useTranslations } from "next-intl";
import React from "react";

const Status = ({ isActive }: { isActive: boolean }) => {
  const t = useTranslations("component.status");

  return isActive ? (
    <span className='flex items-center gap-1 text-sm text-green-500'>
      <span className='size-2 rounded-full bg-green-500'></span>
      {t("active")}
    </span>
  ) : (
    <span className='flex items-center gap-1 text-sm text-red-500'>
      <span className='size-2 rounded-full bg-red-500'></span>
      {t("inactive")}
    </span>
  );
};

export default Status;
