import { IAdminActivityLogResponse } from "@/generated/interfaces/tracking.interface";
import { format } from "date-fns";
import { useTranslations } from "next-intl";
import { useMemo } from "react";
import { TableColumn } from "react-data-table-component";
import { useAdminActivityFeed } from "../screen/admin";

export default function useActivityLogTableColumns() {
  const t = useTranslations("activityLog.columns");
  const { getActivityTitle, getEntityTitle, getBgColor } = useAdminActivityFeed();

  const columns: TableColumn<IAdminActivityLogResponse>[] = useMemo(
    () => [
      {
        name: t("username"),
        selector: row => row.accountUsername ?? "",
        sortable: true,
        maxWidth: "200px"
      },
      {
        name: t("command"),
        sortable: true,
        width: "180px",
        cell: ({ activityType }) => {
          const title = getActivityTitle(activityType);
          const bgColor = getBgColor(activityType, true);

          return (
            <div className={`flex-center min-w-fit rounded-full border ${bgColor} p-2`}>
              <span className={`text-black_white min-w-fit text-xs font-semibold`}>
                {title}
              </span>
            </div>
          );
        }
      },
      {
        name: t("entity"),
        selector: row => getEntityTitle(row.entityType),
        sortable: true,
      },
      {
        name: t("description"),
        sortable: true,
        grow: 2,
        hide: 934,
        cell: ({ entityType, activityType }) => {
          const description = `${getActivityTitle(activityType)} ${getEntityTitle(entityType).toLowerCase()}`;

          return (
            <span className='w-full overflow-hidden text-ellipsis text-nowrap text-sm'>
              {description}
            </span>
          );
        }
      },
      {
        name: t("createdDate"),
        sortable: true,
        width: "200px",
        center: true,
        hide: 1476,
        cell: ({ createdAt }) => {
          return (
            <span className='text-ellipsis text-nowrap text-sm'>
              {format(new Date(createdAt as string), "dd/MM/yyyy")}
            </span>
          );
        }
      }
    ],
    [t, getActivityTitle, getEntityTitle, getBgColor]
  );

  const columnFieldMap: Record<string, keyof IAdminActivityLogResponse> = useMemo(
    () => ({
      [t("username")]: "accountUsername",
      [t("command")]: "activityType",
      [t("entity")]: "entityType",
      [t("createdDate")]: "createdAt"
    }),
    [t]
  );

  return { columns, columnFieldMap };
}
