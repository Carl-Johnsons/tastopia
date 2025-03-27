import { ReportStatus } from "@/constants/reports";
import { useTranslations } from "next-intl";

export const ReportStatusText = ({
  status,
  coloring
}: {
  status: ReportStatus;
  coloring?: boolean;
}) => {
  const t = useTranslations("component.status");

  return (
    <div className='flex min-w-fit items-center gap-1 text-sm'>
      {status === ReportStatus.Done ? (
        <>
          <div className='size-2 rounded-full bg-green-500' />
          <span className={`font-medium ${coloring && "text-green-500"}`}>
            {t("done")}
          </span>
        </>
      ) : (
        <>
          <div className='size-2 rounded-full bg-red-500' />
          <span className={`font-medium ${coloring && "text-red-500"}`}>
            {t("pending")}
          </span>
        </>
      )}
    </div>
  );
};

export const ItemStatusText = ({
  isActive,
  coloring
}: {
  isActive: boolean;
  coloring?: boolean;
}) => {
  const t = useTranslations("component.status");

  return (
    <div className='flex-center flex min-w-fit gap-2'>
      {isActive ? (
        <>
          <div className='size-2.5 rounded-full bg-green-500' />
          <span className={`font-medium text-black_white ${coloring && "text-green-500"}`}>{t("active")}</span>
        </>
      ) : (
        <>
          <div className='size-2.5 rounded-full bg-red' />
          <span className={`font-medium text-black_white ${coloring && "text-red"}`}>{t("inactive")}</span>
        </>
      )}
    </div>
  );
};

export default ReportStatusText;
