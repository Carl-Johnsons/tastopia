import ReportStatusText from "@/components/screen/report/common/StatusText";
import {
  ViewDetailButton,
  ReopenReportButton,
  MarkAsCompletedButton
} from "@/components/screen/report/comment/Button";
import {
  DataTableContext,
  DataTableContextValue
} from "@/components/screen/report/comment/Provider";
import Image from "@/components/shared/common/Image";
import { ReportStatus } from "@/constants/reports";
import { IAdminReportCommentResponse } from "@/generated/interfaces/recipe.interface";
import { format } from "date-fns";
import { useTranslations } from "next-intl";
import { useContext, useMemo } from "react";
import { TableColumn } from "react-data-table-component";

export default function useReportCommentTableColumns() {
  const t = useTranslations("administerReportComments.columns");

  const columns: TableColumn<IAdminReportCommentResponse>[] = [
    {
      name: t("commentOwner"),
      selector: row => row.commentOwnerUsername,
      hide: 952,
      width: "180px",
      sortable: true
    },
    {
      name: t("content"),
      sortable: true,
      minWidth: "200px",
      cell: ({ commentContent }) => <span className='py-2'>{commentContent}</span>
    },
    {
      name: t("recipeImage"),
      hide: 1668,
      width: "140px",
      center: true,
      cell: ({ recipeImageURL }) => (
        <div className='p-2'>
          <Image
            src={recipeImageURL}
            alt={""}
            width={90}
            height={50}
            className='h-[50px] w-[90px] rounded-lg object-cover'
          />
        </div>
      )
    },
    {
      name: t("reporter"),
      selector: row => row.reporterUsername,
      width: "160px",
      hide: 1776,
      sortable: true
    },
    {
      name: t("reportReason"),
      hide: 1368,
      selector: row => row.reportReason,
      sortable: true,
      wrap: true
    },
    {
      name: t("createdDate"),
      sortable: true,
      width: "160px",
      center: true,
      hide: 1476,
      cell: ({ createdAt }) => {
        return (
          <span className='text-ellipsis text-nowrap text-sm'>
            {format(new Date(createdAt), "dd/MM/yyyy")}
          </span>
        );
      }
    },
    {
      name: t("status"),
      sortable: true,
      width: "120px",
      center: true,
      hide: 500,
      selector: row => row.status,
      cell: ({ status }) => {
        return (
          <ReportStatusText
            status={status as ReportStatus}
            coloring
          />
        );
      }
    },
    {
      name: t("actions"),
      center: true,
      width: "150px",
      cell: ({ commentId, recipeId, reportId, status }) => {
        return (
          <ActionButtons
            reportId={reportId}
            recipeId={recipeId}
            targetId={commentId}
            status={status as ReportStatus}
          />
        );
      }
    }
  ];

  const columnFieldMap: Record<string, keyof IAdminReportCommentResponse> = useMemo(
    () => ({
      [t("commentOwner")]: "commentOwnerUsername",
      [t("content")]: "commentContent",
      [t("recipeImage")]: "recipeImageURL",
      [t("reporter")]: "reporterUsername",
      [t("reportReason")]: "reportReason",
      [t("createdDate")]: "createdAt",
      [t("status")]: "status"
    }),
    [t]
  );

  return { columns, columnFieldMap };
}

type ActionButtonsProps = {
  reportId: string;
  recipeId: string;
  targetId: string;
  status: ReportStatus;
};

const ActionButtons = ({ reportId, recipeId, targetId, status }: ActionButtonsProps) => {
  const { onChangeActive } = useContext(DataTableContext) as DataTableContextValue;
  const t = useTranslations("administerReportComments.tooltip");

  return (
    <div className='flex gap-2'>
      <ViewDetailButton
        recipeId={recipeId}
        targetId={targetId}
      />
      {status === ReportStatus.Done ? (
        <ReopenReportButton
          title={t("restoreReport")}
          targetId={reportId}
          commentId={targetId}
          onSuccess={() => {
            onChangeActive({ reportId, value: true });
          }}
        />
      ) : (
        <MarkAsCompletedButton
          title={t("markAsCompleted")}
          targetId={reportId}
          commentId={targetId}
          onSuccess={() => {
            onChangeActive({ reportId, value: false });
          }}
        />
      )}
    </div>
  );
};
