"use client";

import { Check, Clock, Loader2, RotateCcw } from "lucide-react";
import { useRouter } from "@/i18n/navigation";
import { useCallback, useEffect, useMemo, useRef, useState } from "react";
import { format } from "date-fns";
import { ReportStatus } from "@/constants/reports";
import { useGetUserDetailReports, useMarkAllReports } from "@/api/user";
import { filterUniqueItems } from "@/utils/dataFilter";
import { IAdminUserReportDetailResponse } from "@/generated/interfaces/user.interface";
import TooltipButton from "@/components/ui/TooltipButton";
import { markUserReport } from "@/actions/user.action";
import { toast } from "react-toastify";
import StatusText from "../common/StatusText";
import { useTranslations } from "next-intl";
import { DataTableButton } from "@/components/shared/common/Button";
import { useQueryClient } from "@tanstack/react-query";
import Avatar from "@/components/shared/common/Avatar";

type ReportListType = {
  reportedId: string;
};

export default function ReportList({ reportedId }: ReportListType) {
  const [isPendingCompleting, setIsPendingCompleting] = useState(false);
  const [isPendingReopening, setIsPendingReopening] = useState(false);
  const [reports, setReports] = useState<IAdminUserReportDetailResponse[]>();

  const { data, fetchNextPage, hasNextPage, isFetchingNextPage, isLoading } =
    useGetUserDetailReports(reportedId);

  const { mutateAsync: markAllReports } = useMarkAllReports();

  const accountId = reportedId;
  const t = useTranslations("administerReportUsers.notifications");
  const tR = useTranslations("report");
  const queryClient = useQueryClient();

  const handleMarkAllReports = useCallback(
    async (isReopened: boolean) => {
      if (isReopened) {
        setIsPendingReopening(true);
      } else {
        setIsPendingCompleting(true);
      }

      await markAllReports(
        {
          accountId,
          isReopened
        },
        {
          onSuccess: async () => {
            await queryClient.invalidateQueries({ queryKey: ["userReports", accountId] });

            if (isReopened) {
              toast.success(tR("reopenAllSuccess"));
            } else {
              toast.success(tR("resolveAllSuccess"));
            }
          },
          onError: () => {
            toast.error(t("error"));
          },
          onSettled: () => {
            if (isReopened) {
              setIsPendingReopening(false);
            } else {
              setIsPendingCompleting(false);
            }
          }
        }
      );
    },
    [accountId, t, tR, markAllReports, queryClient]
  );

  const hasPending = useMemo(
    () => !!reports && reports.some(report => report.status === ReportStatus.Pending),
    [reports]
  );

  const hasDone = useMemo(
    () => !!reports && reports.some(report => report.status === ReportStatus.Done),
    [reports]
  );

  const hasDifferentStates = useMemo(() => hasPending && hasDone, [hasPending, hasDone]);

  const scrollRef = useRef<HTMLDivElement>(null);

  const handleScroll = () => {
    if (scrollRef.current) {
      const {
        scrollTop,
        scrollHeight,
        clientHeight,
        scrollLeft,
        scrollWidth,
        clientWidth
      } = scrollRef.current;

      const isVerticalScroll = scrollHeight > clientHeight;
      const isHorizontalScroll = scrollWidth > clientWidth;

      let shouldFetch = false;

      if (isVerticalScroll) {
        shouldFetch = scrollTop + clientHeight >= scrollHeight - 10;
      } else if (isHorizontalScroll) {
        shouldFetch = scrollLeft + clientWidth >= scrollWidth - 10;
      }

      if (shouldFetch && hasNextPage && !isFetchingNextPage) {
        fetchNextPage();
      }
    }
  };

  useEffect(() => {
    const currentRef = scrollRef.current;
    if (currentRef) {
      currentRef.addEventListener("scroll", handleScroll);
      return () => currentRef.removeEventListener("scroll", handleScroll);
    }
  }, [hasNextPage, isFetchingNextPage]);

  useEffect(() => {
    if (data?.pages) {
      const uniqueData = filterUniqueItems(data.pages, "reportId");
      setReports(uniqueData);
    }
  }, [data]);

  return (
    <div className='flex flex-col gap-8 overflow-x-hidden p-6'>
      <div>
        <p className='text-black_white base-medium mb-2 flex w-full flex-col gap-4'>
          {tR("reportList")}
        </p>
        <div className='flex gap-2'>
          <DataTableButton
            title={tR("resolveAll")}
            onClick={() => handleMarkAllReports(false)}
            className='w-fit bg-purple-400 hover:bg-purple-500'
            icon={<Check className='text-white_black' />}
            isLoading={isPendingCompleting}
            disabled={!hasPending || isPendingCompleting}
          />
          <DataTableButton
            title={tR("reopenAll")}
            onClick={() => handleMarkAllReports(true)}
            className='w-fit bg-green-400 hover:bg-green-500'
            icon={<RotateCcw className='text-white_black' />}
            isLoading={isPendingReopening}
            disabled={!hasDone || isPendingReopening}
          />
        </div>
      </div>

      <div
        ref={scrollRef}
        className='flex max-h-[640px] w-full gap-4 overflow-x-auto rounded-lg xl:flex-col xl:overflow-x-hidden xl:overflow-y-scroll'
      >
        {isLoading ? (
          <div className='flex-center'>
            <Loader2 className='size-5 animate-spin text-primary' />
          </div>
        ) : (
          reports?.map(report => (
            <ReportItem
              key={report.reportId}
              reportId={report.reportId}
              reportedId={accountId}
              reportReason={report.additionalDetails}
              status={
                hasDifferentStates
                  ? (report.status as ReportStatus)
                  : hasDone
                    ? ReportStatus.Done
                    : ReportStatus.Pending
              }
              reportCodes={report.reportReason}
              reporter={report.reporterUsername}
              reporterId={report.reporterId}
              reporterAvatar={report.reporterAvtUrl}
              createdAt={format(new Date(report.createdAt), "h:mm a - dd/MM/yyyy")}
            />
          ))
        )}
        {isFetchingNextPage && (
          <div className='flex-center text-center'>
            <Loader2 className='size-5 animate-spin text-primary' />
          </div>
        )}
      </div>
    </div>
  );
}

type ReportItemProps = {
  reportId: string;
  reportedId: string;
  reportReason?: string;
  status: ReportStatus;
  reportCodes: string[];
  reporter: string;
  reporterId: string;
  reporterAvatar: string;
  createdAt: string;
};

const ReportItem = ({
  reportId,
  reportedId,
  reportReason,
  status,
  reportCodes,
  reporter,
  reporterId,
  reporterAvatar,
  createdAt
}: ReportItemProps) => {
  const router = useRouter();
  const [isActive, setIsActive] = useState(status !== ReportStatus.Done);

  const t = useTranslations("administerReportUsers.notifications");
  const queryClient = useQueryClient();
  const accountId = reportedId;

  useEffect(() => {
    const newStatus = status !== ReportStatus.Done;

    if (newStatus !== isActive) {
      setIsActive(newStatus);
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [status]);

  const handleMarkReport = useCallback(async () => {
    const { ok, data: result } = await markUserReport(reportId);

    if (!ok || !result?.userReport.reportedId) {
      toast.error(t("error"));
      return;
    }

    await queryClient.invalidateQueries({ queryKey: ["userReports", accountId] });
    setIsActive(result.isReopened);

    if (result.isReopened) {
      toast.success(t("restoreReportSuccess"));
    } else {
      toast.success(t("completeReportSuccess"));
    }
  }, [reportId, t, accountId, queryClient]);

  return (
    <div
      className={`flex h-fit min-w-[300px] max-w-[400px] flex-col gap-3.5 rounded-md p-5 dark:border max-lg:min-w-[320px] xl:min-w-fit ${isActive ? "bg-red-200 dark:border-red dark:bg-transparent" : "bg-green-100 dark:border-green dark:bg-transparent"}`}
    >
      <div className='flex justify-between'>
        <StatusText
          status={isActive ? ReportStatus.Pending : ReportStatus.Done}
          coloring
        />
        {isActive ? (
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
        )}{" "}
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
            router.push(`/users/${reporterId}`);
          }}
        >
          <Avatar
            src={reporterAvatar}
            alt={reporter}
            className='size-10'
          />
        </button>
        <button
          onClick={() => {
            router.push(`/users/${reporterId}`);
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
