import { IAdminGetUserResponse } from "@/generated/interfaces/user.interface";
import { Button } from "@/components/ui/button";
import { Ban, RotateCcw, Search } from "lucide-react";
import { Link, useRouter } from "@/i18n/navigation";
import { useMemo, useState } from "react";
import { adminBanUser } from "@/actions/user.action";
import { toast } from "react-toastify";
import Status from "@/components/ui/Status";
import { useTranslations } from "next-intl";

export const usersColumns = (t: any) => [
  {
    name: t("columns.username"),
    selector: (user: IAdminGetUserResponse) => user?.accountUsername,
    sortable: true
  },
  {
    name: t("columns.name"),
    selector: (user: IAdminGetUserResponse) => user?.displayName,
    sortable: true,
    hide: 1460
  },
  {
    name: t("columns.gmail"),
    selector: (user: IAdminGetUserResponse) => user?.accountEmail ?? "",
    hide: 1234,
    sortable: true
  },
  {
    name: t("columns.phoneNumber"),
    selector: (user: IAdminGetUserResponse) => user?.accountPhoneNumber ?? "",
    hide: 1026,
    sortable: true
  },
  {
    name: t("columns.dateOfBirth"),
    selector: (user: IAdminGetUserResponse) => user?.dob ?? "",
    hide: 1612,
    sortable: true
  },
  {
    name: t("columns.status"),
    hide: 600,
    sortable: true,
    cell: (user: IAdminGetUserResponse) => <Status isActive={user.isAccountActive} />
  },
  {
    name: t("columns.address"),
    selector: (user: IAdminGetUserResponse) => user?.address ?? "",
    sortable: true,
    hide: 1840
  },
  {
    name: t("columns.action"),
    ignoreRowClick: true,
    button: true,
    width: "300px"
  }
];

type ActionButtonsProps = {
  accountId: string;
  isActive: boolean;
  onStatusUpdate: (accountId: string, isActive: boolean) => void;
};
export const ActionButtons = ({
  accountId,
  isActive,
  onStatusUpdate
}: ActionButtonsProps) => {
  const t = useTranslations("administerUsers");
  const router = useRouter();
  const [active, setActive] = useState<boolean>(isActive);
  const url = useMemo(() => `/users/${accountId}`, [accountId]);

  const handleDetailClick = () => {
    router.push(url);
  };

  const handleToggleStatus = async () => {
    const result = await adminBanUser(accountId);
    if (result.userId) {
      const newStatus = result.isRestored;
      setActive(newStatus);
      onStatusUpdate(accountId, newStatus);

      if (result.isRestored) {
        toast.success(t("notifications.restoreSuccess"));
      } else {
        toast.success(t("notifications.disableSuccess"));
      }
    } else {
      toast.error(t("notifications.error"));
    }
  };

  return (
    <div className='flex gap-2'>
      <Link href={url}>
        <Button
          className='text-white_black bg-primary hover:bg-secondary'
          onClick={handleDetailClick}
        >
          <Search />
          <p className='mt-1 max-sm:hidden'>{t("actions.detail")}</p>
        </Button>
      </Link>

      {active ? (
        <Button
          className='text-white_black bg-red hover:bg-red-600'
          onClick={handleToggleStatus}
        >
          <Ban />
          <p className='mt-1 max-sm:hidden'>{t("actions.disable")}</p>
        </Button>
      ) : (
        <Button
          className='text-white_black bg-green hover:bg-green/90 dark:bg-green/80'
          onClick={handleToggleStatus}
        >
          <RotateCcw />
          <p className='mt-1 max-sm:hidden'>{t("actions.restore")}</p>
        </Button>
      )}
    </div>
  );
};
