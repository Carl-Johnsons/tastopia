import ReportStatusText from "@/components/screen/report/common/StatusText";
import {
  ViewDetailButton,
  ReopenReportButton,
  MarkAsCompletedButton
} from "@/components/screen/report/recipe/Button";
import {
  DataTableContext,
  DataTableContextValue
} from "@/components/screen/report/recipe/Provider";
import Image from "@/components/shared/common/Image";
import { ReportStatus } from "@/constants/reports";
import { IAdminReportRecipeResponse } from "@/generated/interfaces/recipe.interface";
import { format } from "date-fns";
import { useTranslations } from "next-intl";
import { useContext, useMemo } from "react";
import { TableColumn } from "react-data-table-component";

export default function useReportRecipeTableColumns() {
  const t = useTranslations("administerReportRecipes.columns");

  const columns: TableColumn<IAdminReportRecipeResponse>[] = [
    {
      name: t("recipeName"),
      selector: row => row.recipeTitle,
      sortable: true,
      grow: 2
    },
    {
      name: t("recipeOwner"),
      selector: row => row.recipeOwnerUsername,
      hide: 1576,
      sortable: true
    },
    {
      name: t("recipeImage"),
      hide: 1368,
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
      hide: 1776,
      sortable: true
    },
    {
      name: t("reportReason"),
      hide: 1368,
      sortable: true,
      grow: 3,
      cell: ({ reportReason }) => {
        return (
          <span className='w-full overflow-hidden text-ellipsis text-nowrap text-sm'>
            {reportReason}
          </span>
        );
      }
    },
    {
      name: t("createdDate"),
      sortable: true,
      width: "140px",
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
      width: "130px",
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
      name: t("action"),
      center: true,
      width: "150px",
      cell: ({ recipeId, reportId, status }) => {
        return (
          <ActionButtons
            reportId={reportId}
            recipeId={recipeId}
            status={status as ReportStatus}
          />
        );
      }
    }
  ];

  const columnFieldMap: Record<string, keyof IAdminReportRecipeResponse> = useMemo(
    () => ({
      [t("recipeName")]: "recipeTitle",
      [t("recipeOwner")]: "recipeOwnerUsername",
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
  recipeId: string;
  reportId: string;
  status: ReportStatus;
};

const ActionButtons = ({ reportId, recipeId, status }: ActionButtonsProps) => {
  const { onChangeActive } = useContext(DataTableContext) as DataTableContextValue;
  const t = useTranslations("administerReportRecipes.tooltip");

  return (
    <div className='flex gap-2'>
      <ViewDetailButton
        title={t("viewDetail")}
        targetId={recipeId}
      />
      {status === ReportStatus.Done ? (
        <ReopenReportButton
          title={t("restoreReport")}
          targetId={reportId}
          onSuccess={() => {
            onChangeActive({ reportId, value: true });
          }}
        />
      ) : (
        <MarkAsCompletedButton
          title={t("markAsCompleted")}
          targetId={reportId}
          onSuccess={() => {
            onChangeActive({ reportId, value: false });
          }}
        />
      )}
    </div>
  );
};
