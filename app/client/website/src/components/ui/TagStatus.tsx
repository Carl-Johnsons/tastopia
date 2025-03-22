import { useTranslations } from "next-intl";
import React from "react";

const ReportStatus = ({ status }: { status: string }) => {
  const t = useTranslations("component.status");

  const renderStatus = (status: string) => {
    switch (status) {
      case "Inactive":
        return (
          <span className='flex items-center gap-1 text-sm text-red-500'>
            <span className='size-2 rounded-full bg-red-500'></span>
            {t("inactive")}
          </span>
        );
      case "Active":
        return (
          <span className='flex items-center gap-1 text-sm text-green-500'>
            <span className='size-2 rounded-full bg-green-500'></span>
            {t("active")}
          </span>
        );
      case "Pending":
        return (
          <span className='flex items-center gap-1 text-sm text-blue-500'>
            <span className='size-2 rounded-full bg-blue-500'></span>
            {t("pending")}
          </span>
        );
    }
  };

  return renderStatus(status);
};

export default ReportStatus;
