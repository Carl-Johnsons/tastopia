/* eslint-disable tailwindcss/enforces-shorthand */
"use client";

import { Clock } from "lucide-react";
import { Avatar, AvatarImage } from "@/components/ui/avatar";
import { useRouter } from "@/i18n/navigation";
import { useMemo, useState } from "react";
import { IReportRecipeResponse } from "@/generated/interfaces/recipe.interface";
import { format } from "date-fns";
import { ReportStatus, ReportType } from "@/constants/reports";
import StatusText from "../common/StatusText";
import {
  MarkAsCompletedButton as MarkAsCompletedRecipeButton,
  ReopenReportButton as ReopenReportRecipeButton
} from "../recipe/Button";
import {
  MarkAsCompletedButton as MarkAsCompletedCommentButton,
  ReopenReportButton as ReopenReportCommentButton
} from "../comment/Button";

type ReportListProps = {
  reports: IReportRecipeResponse[];
  reportType: ReportType;
  recipeId?: string;
  className?: string;
};

export default function ReportList({
  reportType,
  reports,
  recipeId,
  className
}: ReportListProps) {
  return (
    <div
      className={`flex gap-8 overflow-x-scroll xl:flex-col ${reportType === ReportType.COMMENT && "grid justify-items-center gap-4 sm:grid-cols-[repeat(auto-fill,minmax(300px,1fr))] overflow-x-auto"} ${className}`}
    >
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
            reportType={reportType}
            reportId={id}
            {...(reportType === ReportType.COMMENT && { recipeId })}
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
  reportType: ReportType;
  reportId: string;
  recipeId?: string;
  reportReason?: string;
  status: ReportStatus;
  reportCodes: string[];
  reporter: string;
  reporterAvatar: string;
  createdAt: string;
};

const ReportItem = ({
  reportType,
  reportId,
  recipeId,
  reportReason,
  status,
  reportCodes,
  reporter,
  reporterAvatar,
  createdAt
}: ReportItemProps) => {
  const router = useRouter();
  const [isActive, setIsActive] = useState(status !== ReportStatus.Done);

  const MarkAsCompletedComponent = useMemo(() => {
    switch (reportType) {
      case ReportType.COMMENT:
        return MarkAsCompletedCommentButton;
      case ReportType.RECIPE:
        return MarkAsCompletedRecipeButton;
      default:
        throw new Error("Unsupported report type");
    }
  }, [reportType]);

  const ReOpenReportComponent = useMemo(() => {
    switch (reportType) {
      case ReportType.COMMENT:
        return ReopenReportCommentButton;
      case ReportType.RECIPE:
        return ReopenReportRecipeButton;
      default:
        throw new Error("Unsupported report type");
    }
  }, [reportType]);

  return (
    <div
      className={`flex min-w-[340px] max-w-[400px] flex-col gap-3.5 rounded-md p-5 dark:border xl:h-fit xl:min-w-fit ${isActive ? "bg-red-200 dark:border-red dark:bg-transparent" : "bg-green-100 dark:border-green dark:bg-transparent"} ${reportType === ReportType.COMMENT && "h-full w-full min-w-[200px] max-w-full xl:h-full xl:w-full"}`}
    >
      <div className='flex justify-between'>
        <StatusText
          status={isActive ? ReportStatus.Pending : ReportStatus.Done}
          coloring
        />
        {isActive ? (
          <MarkAsCompletedComponent
            title='Mark as completed'
            targetId={reportId}
            {...(reportType === ReportType.COMMENT && { recipeId })}
            onSuccess={() => {
              setIsActive(false);
            }}
          />
        ) : (
          <ReOpenReportComponent
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
