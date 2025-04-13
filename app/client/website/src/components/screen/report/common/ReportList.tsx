/* eslint-disable tailwindcss/enforces-shorthand */
"use client";

import { Clock } from "lucide-react";
import { Avatar, AvatarImage } from "@/components/ui/avatar";
import { useRouter } from "@/i18n/navigation";
import { useEffect, useMemo, useState } from "react";
import { IReportRecipeResponse } from "@/generated/interfaces/recipe.interface";
import { format } from "date-fns";
import { ReportStatus, ReportType } from "@/constants/reports";
import StatusText from "../common/StatusText";
import {
  MarkAllReportsAsCompletedButton as MarkAllRecipeReportsAsCompletedButton,
  MarkAsCompletedButton as MarkAsCompletedRecipeButton,
  ReopenReportButton as ReopenReportRecipeButton,
  ReopenAllReportsButton as ReopenAllRecipeReportsButton
} from "../recipe/Button";
import {
  MarkAllAsCompletedButton as MarkAllCommentReportsAsCompletedButton,
  MarkAsCompletedButton as MarkAsCompletedCommentButton,
  ReopenReportButton as ReopenReportCommentButton,
  ReopenAllReportsButton as ReopenAllCommentReportsButton
} from "../comment/Button";

type ReportListProps = {
  reports: Array<IReportRecipeResponse>;
  reportType: ReportType;
  recipeId?: string;
  commentId?: string;
  targetId: string;
  className?: string;
};

export default function ReportList({
  reportType,
  reports,
  recipeId,
  commentId,
  targetId,
  className
}: ReportListProps) {
  const hasPending = useMemo(
    () => reports.some(report => report.status === ReportStatus.Pending),
    [reports]
  );

  const hasDone = useMemo(
    () => reports.some(report => report.status === ReportStatus.Done),
    [reports]
  );

  const hasDifferentStates = useMemo(() => hasPending && hasDone, [hasPending, hasDone]);

  const MarkAllReportsAsCompletedButton = useMemo(() => {
    switch (reportType) {
      case ReportType.COMMENT:
        return MarkAllCommentReportsAsCompletedButton;
      case ReportType.RECIPE:
        return MarkAllRecipeReportsAsCompletedButton;
      default:
        throw new Error("Unsupported report type");
    }
  }, [reportType]);

  const ReopenAllReportsButton = useMemo(() => {
    switch (reportType) {
      case ReportType.COMMENT:
        return ReopenAllCommentReportsButton;
      case ReportType.RECIPE:
        return ReopenAllRecipeReportsButton;
      default:
        throw new Error("Unsupported report type");
    }
  }, [reportType]);

  return (
    <div className={`flex flex-col gap-8 overflow-x-hidden ${className}`}>
      <div>
        <p className='text-black_white base-medium mb-2 flex w-full flex-col gap-4'>
          {"Report list"}
        </p>
        <div className='flex gap-2'>
          <MarkAllReportsAsCompletedButton
            title={"Resolve All"}
            targetId={targetId}
            recipeId={recipeId}
            commentId={commentId}
            className='w-fit rounded-full'
            disabled={!hasPending}
            noTruncateText
            noText={false}
          />
          <ReopenAllReportsButton
            title={"Reopen All"}
            targetId={targetId}
            recipeId={recipeId}
            commentId={commentId}
            className='w-fit rounded-full'
            disabled={!hasDone}
            noTruncateText
            noText={false}
          />
        </div>
      </div>

      <div
        className={`flex gap-8 overflow-x-scroll xl:flex-col ${reportType === ReportType.COMMENT && "grid justify-items-center gap-4 overflow-x-auto lg:grid-cols-[repeat(auto-fill,minmax(330px,1fr))]"}`}
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
              recipeId={recipeId}
              reportId={id}
              commentId={commentId}
              reportReason={additionalDetail}
              status={
                hasDifferentStates
                  ? (status as ReportStatus)
                  : hasDone
                    ? ReportStatus.Done
                    : ReportStatus.Pending
              }
              reportCodes={reasons}
              reporter={reporterUsername}
              reporterAvatar={reporterAvtUrl}
              createdAt={format(new Date(createdAt), "h:mm a - dd/MM/yyyy")}
            />
          )
        )}
      </div>
    </div>
  );
}

type ReportItemProps = {
  reportType: ReportType;
  reportId: string;
  recipeId?: string;
  commentId?: string;
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
  commentId,
  reportReason,
  status,
  reportCodes,
  reporter,
  reporterAvatar,
  createdAt
}: ReportItemProps) => {
  const router = useRouter();
  const [isActive, setIsActive] = useState(status !== ReportStatus.Done);

  useEffect(() => {
    setIsActive(status !== ReportStatus.Done);
  }, [status]);

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
            recipeId={recipeId}
            commentId={commentId}
            onSuccess={() => {
              setIsActive(false);
            }}
          />
        ) : (
          <ReOpenReportComponent
            title='Reopen report'
            targetId={reportId}
            recipeId={recipeId}
            commentId={commentId}
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
