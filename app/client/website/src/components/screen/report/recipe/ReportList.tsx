"use client";

import { Clock } from "lucide-react";
import { Avatar, AvatarImage } from "@/components/ui/avatar";
import { useRouter } from "@/i18n/navigation";
import { useState } from "react";
import { IReportRecipeResponse } from "@/generated/interfaces/recipe.interface";
import { format } from "date-fns";
import { MarkAsCompletedButton, ReopenReportButton } from "./Button";
import { ReportStatus } from "@/constants/reports";
import StatusText from "../common/StatusText";

type ReportListProps = {
  reports: IReportRecipeResponse[];
  className?: string;
};

export default function ReportList({ reports, className }: ReportListProps) {
  return (
    <div className={`flex gap-8 overflow-x-scroll xl:flex-col ${className}`}>
      {reports.map(
        ({
          id,
          additionalDetail,
          reasons,
          status,
          reporterUsername,
          createdAt,
          reporterAvtUrl
        }) => (
          <ReportItem
            key={id}
            reportId={id}
            reportReason={additionalDetail}
            status={status as ReportStatus}
            reportCodes={reasons}
            reporter={reporterUsername}
            reporterAvatar={reporterAvtUrl}
            createdAt={format(new Date(createdAt), "h:mm a - dd/MM/yyyy")}
          />
        )
      )}
    </div>
  );
}

type ReportItemProps = {
  reportId: string;
  reportReason?: string;
  status: ReportStatus;
  reportCodes: string[];
  reporter: string;
  reporterAvatar: string;
  createdAt: string;
};

const ReportItem = ({
  reportId,
  reportReason,
  status,
  reportCodes,
  reporter,
  reporterAvatar,
  createdAt
}: ReportItemProps) => {
  const router = useRouter();
  const [isActive, setIsActive] = useState(status !== ReportStatus.Done);

  return (
    <div
      className={`flex min-w-[340px] max-w-[400px] flex-col gap-3.5 rounded-md p-5 dark:border xl:h-fit xl:min-w-fit ${isActive ? "bg-red-200 dark:border-red dark:bg-transparent" : "bg-green-100 dark:border-green dark:bg-transparent"}`}
    >
      <div className='flex justify-between'>
        <StatusText
          status={isActive ? ReportStatus.Pending : ReportStatus.Done}
          coloring
        />
        {isActive ? (
          <MarkAsCompletedButton
            title='Mark as completed'
            targetId={reportId}
            onSuccess={() => {
              setIsActive(false);
            }}
          />
        ) : (
          <ReopenReportButton
            title='Reopen report'
            targetId={reportId}
            onSuccess={() => {
              setIsActive(true);
            }}
          />
        )}
      </div>
      <div>
        <span className='text-black_white font-bold'>{reportReason}</span>
      </div>
      <div className='flex flex-col gap-2'>
        {reportCodes.map((code, index) => (
          <span
            key={code + index}
            className='bg-white_black w-fit rounded-full px-3 py-1 text-sm font-medium text-primary'
          >
            {code}
          </span>
        ))}
      </div>
      <div className='flex items-center gap-2 text-gray-700'>
        <button
          onClick={() => {
            router.push("/users/bb06e4ec-f371-45d5-804e-22c65c77f67d");
          }}
        >
          <Avatar>
            <AvatarImage src={reporterAvatar} />
          </Avatar>
        </button>
        <button
          onClick={() => {
            router.push("/users/bb06e4ec-f371-45d5-804e-22c65c77f67d");
          }}
        >
          <span className='text-sm font-bold text-gray-700 hover:text-black dark:text-gray-400 dark:hover:text-white'>
            {reporter}
          </span>
        </button>
      </div>
      <div className='flex gap-2 text-gray-700 dark:text-gray-400'>
        <Clock />
        <span className='text-sm'>{createdAt}</span>
      </div>
    </div>
  );
};
