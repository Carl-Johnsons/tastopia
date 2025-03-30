import {
  DisableAdminButton,
  RestoreAdminButton,
  UpdateAdminButton,
  ViewDetailButton
} from "@/components/screen/admins/Button";
import { useAdminsContext } from "@/components/screen/admins/Provider";
import { ItemStatusText } from "@/components/screen/report/common/StatusText";
import { IAdminResponse } from "@/generated/interfaces/user.interface";
import { format } from "date-fns";
import { useTranslations } from "next-intl";
import { useMemo } from "react";
import { TableColumn } from "react-data-table-component";

export default function useAdminTableColumns() {
  const t = useTranslations("administerAdmins.columns");

  const columns: TableColumn<IAdminResponse>[] = useMemo(
    () => [
      {
        name: t("username"),
        selector: row => row.userName,
        sortable: true,
        maxWidth: "200px"
      },
      {
        name: t("displayName"),
        selector: row => row.displayName,
        hide: 1576,
        sortable: true
      },
      {
        name: t("email"),
        selector: row => row.email as string,
        sortable: true,
        hide: 870
      },
      {
        name: t("phoneNumber"),
        selector: row => row.phoneNumber as string,
        sortable: true,
        hide: 1010
      },
      {
        name: t("dateOfBirth"),
        sortable: true,
        width: "140px",
        center: true,
        hide: 1476,
        cell: ({ dob }) => {
          return (
            <span className='text-ellipsis text-nowrap text-sm'>
              {format(new Date(dob as string), "dd/MM/yyyy")}
            </span>
          );
        }
      },
      {
        name: t("status"),
        sortable: true,
        cell: ({ isActive }) => {
          return (
            <ItemStatusText
              isActive={isActive}
              coloring
            />
          );
        }
      },
      {
        name: t("address"),
        hide: 1368,
        sortable: true,
        wrap: true,
        cell: ({ address }) => <span className='py-1'>{address}</span>
      },
      {
        name: t("actions"),
        center: true,
        width: "180px",
        cell: ({ accountId, isActive }) => {
          return (
            <ActionButtons
              id={accountId}
              isActive={isActive}
            />
          );
        }
      }
    ],
    [t]
  );

  const columnFieldMap: Record<string, keyof IAdminResponse> = useMemo(
    () => ({
      [t("username")]: "userName",
      [t("displayName")]: "displayName",
      [t("email")]: "email",
      [t("phoneNumber")]: "phoneNumber",
      [t("dateOfBirth")]: "dob",
      [t("status")]: "isActive",
      [t("address")]: "address"
    }),
    [t]
  );

  return { columns, columnFieldMap };
}

type ActionButtonsProps = {
  id: string;
  isActive: boolean;
};

const ActionButtons = ({ id, isActive }: ActionButtonsProps) => {
  const { onChangeActive } = useAdminsContext();
  const t = useTranslations("administerAdmins.tooltip");

  return (
    <div className='flex gap-2'>
      <ViewDetailButton
        title={t("viewDetail")}
        targetId={id}
      />
      {!isActive ? (
        <RestoreAdminButton
          title={t("restore")}
          targetId={id}
          noText
          toolTip
          onSuccess={() => {
            onChangeActive({ id, value: true });
          }}
        />
      ) : (
        <DisableAdminButton
          title={t("disable")}
          targetId={id}
          noText
          toolTip
          onSuccess={() => {
            onChangeActive({ id, value: false });
          }}
        />
      )}
      <UpdateAdminButton
        title={t("update")}
        targetId={id}
        noText
        toolTip
      />
    </div>
  );
};
