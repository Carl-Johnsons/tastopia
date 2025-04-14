import { IAdminUserReportResponse } from "@/generated/interfaces/user.interface";
import { Ban, Check, RotateCcw, Search } from "lucide-react";
import { useRouter } from "@/i18n/navigation";
import { useState } from "react";
import { adminBanUser, markUserReport } from "@/actions/user.action";
import { toast } from "react-toastify";
import { ReportStatus } from "@/constants/reports";
import ReportStatusComponent from "@/components/ui/ReportStatus";
import { format } from "date-fns";
import TooltipButton from "@/components/ui/TooltipButton";
import { useTranslations } from "next-intl";
import { useInvalidateAdmin } from "@/hooks/query";

export const reportColumns = (t: any) => [
  {
    name: t("columns.username"),
    selector: (report: IAdminUserReportResponse) => report?.reportedUsername,
    sortable: true,
    width: "160px"
  },
  {
    name: t("columns.name"),
    selector: (report: IAdminUserReportResponse) => report?.reportedDisplayName,
    sortable: true,
    hide: 1460
  },
  {
    name: t("columns.reporter"),
    selector: (report: IAdminUserReportResponse) => report?.reporterDisplayName ?? "",
    hide: 1234,
    sortable: true
  },
  {
    name: t("columns.reportReason"),
    selector: (report: IAdminUserReportResponse) => report?.reportReason ?? "",
    hide: 1026,
    width: "300px",
    sortable: true
  },
  {
    name: t("columns.createDate"),
    selector: (report: IAdminUserReportResponse) =>
      format(new Date(report?.createdAt), "dd/MM/yyyy") ?? "",
    hide: 1612,
    sortable: true,
    center: true
  },
  {
    name: t("columns.status"),
    hide: 600,
    sortable: true,
    cell: (report: IAdminUserReportResponse) => (
      <ReportStatusComponent status={report.status} />
    )
  },
  {
    name: t("columns.action"),
    ignoreRowClick: true,
    button: true,
    width: "190px"
  }
];

type ActionButtonsProps = {
  reportId: string;
  reportedId: string;
  status: ReportStatus;
  reportedIsActive: boolean;
  onStatusUpdate: (reportId: string, status: ReportStatus) => void;
};
export const ActionButtons = ({
  reportId,
  reportedId,
  status,
  reportedIsActive,
  onStatusUpdate
}: ActionButtonsProps) => {
  const t = useTranslations("administerReportUsers");
  const router = useRouter();
  const [reportStatus, setReportStatus] = useState<ReportStatus>(status);
  const [reportedStatus, setReportedStatus] = useState<boolean>(reportedIsActive);
  const { invalidateCurrentAdminActivities } = useInvalidateAdmin();

  const handleDetailClick = () => {
    router.push(`/reports/users/detail/${reportedId}`);
  };

  const handleBanUser = async () => {
    const { ok, data: result } = await adminBanUser(reportedId);

    if (!ok || !result?.userId) {
      toast.error(t("notifications.error"));
      return;
    }

    const newStatus = result.isRestored;
    setReportedStatus(newStatus);

    if (result.isRestored) {
      toast.success(t("notifications.restoreUserSuccess"));
    } else {
      toast.success(t("notifications.disableUserSuccess"));
    }

    invalidateCurrentAdminActivities();
  };

  const handleMarkReport = async () => {
    const { ok, data: result } = await markUserReport(reportId);

    if (!ok || !result?.userReport.reportedId) {
      toast.error(t("notifications.error"));
      return;
    }

    const newStatus = result.userReport.status as unknown as ReportStatus;
    setReportStatus(newStatus);
    onStatusUpdate(reportId, newStatus);

    if (result.isReopened) {
      toast.success(t("notifications.restoreReportSuccess"));
    } else {
      toast.success(t("notifications.completeReportSuccess"));
    }

    invalidateCurrentAdminActivities();
  };

  return (
    <div className='flex gap-2'>
      <TooltipButton
        title={t("tooltip.reportDetail")}
        icon={<Search className='text-white_black' />}
        onClick={handleDetailClick}
        className='bg-primary hover:bg-secondary'
      />

      {/* Ban */}
      {reportedStatus ? (
        <TooltipButton
          title={t("tooltip.disableUser")}
          icon={<Ban className='text-white_black' />}
          onClick={handleBanUser}
          className='bg-red hover:bg-red-600'
        />
      ) : (
        <TooltipButton
          title={t("tooltip.restoreUser")}
          icon={<RotateCcw className='text-white_black' />}
          onClick={handleBanUser}
          className='bg-green-400 hover:bg-green-500'
        />
      )}

      {/* Mark report */}
      {reportStatus === ReportStatus.Pending ? (
        <TooltipButton
          title={t("tooltip.markAsComplete")}
          icon={<Check className='text-white_black' />}
          onClick={handleMarkReport}
          className='bg-purple-400 hover:bg-purple-500'
        />
      ) : (
        <TooltipButton
          title={t("tooltip.restoreReport")}
          icon={<RotateCcw className='text-white_black' />}
          onClick={handleMarkReport}
          className='bg-green-400 hover:bg-green-500'
        />
      )}
    </div>
  );
};
