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

export const reportColumns = [
  {
    name: "Username",
    selector: (report: IAdminUserReportResponse) => report?.reportedUsername,
    sortable: true
  },
  {
    name: "Name",
    selector: (report: IAdminUserReportResponse) => report?.reportedDisplayName,
    sortable: true,
    hide: 1460
  },
  {
    name: "Reporter",
    selector: (report: IAdminUserReportResponse) => report?.reporterDisplayName ?? "",
    hide: 1234,
    sortable: true
  },
  {
    name: "Report reason",
    selector: (report: IAdminUserReportResponse) => report?.reportReason ?? "",
    hide: 1026,
    width: "300px",
    sortable: true
  },
  {
    name: "Created date",
    selector: (report: IAdminUserReportResponse) =>
      format(new Date(report?.createdAt), "dd/MM/yyyy") ?? "",
    hide: 1612,
    sortable: true,
    center: true
  },
  {
    name: "Status",
    hide: 600,
    sortable: true,
    cell: (report: IAdminUserReportResponse) => (
      <ReportStatusComponent status={report.status} />
    )
  },
  {
    name: "Action",
    ignoreRowClick: true,
    button: true,
    width: "190px"
  }
];

export const columnFieldMap: Record<string, keyof IAdminUserReportResponse> = {
  Username: "reportedUsername",
  Name: "reportedDisplayName",
  Reporter: "reporterDisplayName",
  "Report reason": "reportReason",
  "Created date": "createdAt",
  Status: "status"
};

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
  const router = useRouter();
  const [reportStatus, setReportStatus] = useState<ReportStatus>(status);
  const [reportedStatus, setReportedStatus] = useState<boolean>(reportedIsActive);

  const handleDetailClick = () => {
    router.push(`/reports/users/detail/${reportedId}`);
  };

  const handleBanUser = async () => {
    const result = await adminBanUser(reportedId);
    if (result.userId) {
      const newStatus = result.isRestored;
      setReportedStatus(newStatus);

      if (result.isRestored) {
        toast.success("Reopen user successfully!");
      } else {
        toast.success("Disable user successfully!");
      }
    } else {
      toast.error("Something went wrong!");
    }
  };

  const handleMarkReport = async () => {
    const result = await markUserReport(reportId);
    if (result.userReport.reportedId) {
      const newStatus = result.userReport.status;
      setReportStatus(newStatus);
      onStatusUpdate(reportId, newStatus);

      if (result.isRestored) {
        toast.success("Reopen report successfully!");
      } else {
        toast.success("Disable report successfully!");
      }
    } else {
      toast.error("Something went wrong!");
    }
  };

  return (
    <div className='flex gap-2'>
      <TooltipButton
        title='Go to reported detail'
        icon={<Search className='text-white_black' />}
        onClick={handleDetailClick}
        className='bg-primary hover:bg-secondary'
      />

      {/* Ban */}
      {reportedStatus ? (
        <TooltipButton
          title='Ban user'
          icon={<Ban className='text-white_black' />}
          onClick={handleBanUser}
          className='bg-red hover:bg-red-600'
        />
      ) : (
        <TooltipButton
          title='Restore user'
          icon={<RotateCcw className='text-white_black' />}
          onClick={handleBanUser}
          className='bg-green-400 hover:bg-green-500'
        />
      )}

      {/* Mark report */}
      {reportStatus === ReportStatus.Pending ? (
        <TooltipButton
          title='Mark report as complete'
          icon={<Check className='text-white_black' />}
          onClick={handleMarkReport}
          className='bg-purple-400 hover:bg-purple-500'
        />
      ) : (
        <TooltipButton
          title='Restore report'
          icon={<RotateCcw className='text-white_black' />}
          onClick={handleMarkReport}
          className='bg-green-400 hover:bg-green-500'
        />
      )}
    </div>
  );
};
