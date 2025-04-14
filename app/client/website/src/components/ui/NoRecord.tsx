"use client";

import { Inbox } from "lucide-react";
import { useTranslations } from "next-intl";

const NoRecord = () => {
  const t = useTranslations("noRecords");

  return (
    <div className='flex flex-col items-center justify-center py-10'>
      <Inbox className='size-10 text-gray-400' />
      <p className='mt-2 text-sm text-gray-500'>{t("content")}</p>
    </div>
  );
};

export default NoRecord;
