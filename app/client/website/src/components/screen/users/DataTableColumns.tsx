import { IAdminGetUserResponse } from "@/generated/interfaces/user.interface";
import { Button } from "@/components/ui/button";
import { Ban, RotateCcw, Search } from "lucide-react";
import { useRouter } from "@/i18n/navigation";
import { useState } from "react";
import { adminBanUser } from "@/actions/user.action";
import { toast } from "react-toastify";
import Status from "@/components/ui/Status";

export const usersColumns = [
  {
    name: "Username",
    selector: (user: IAdminGetUserResponse) => user?.accountUsername,
    sortable: true
  },
  {
    name: "Name",
    selector: (user: IAdminGetUserResponse) => user?.displayName,
    sortable: true,
    hide: 1460
  },
  {
    name: "Gmail",
    selector: (user: IAdminGetUserResponse) => user?.accountEmail ?? "",
    hide: 1234,
    sortable: true
  },
  {
    name: "Phone number",
    selector: (user: IAdminGetUserResponse) => user?.accountPhoneNumber ?? "",
    hide: 1026,
    sortable: true
  },
  {
    name: "Date of birth",
    selector: (user: IAdminGetUserResponse) => user?.dob ?? "",
    hide: 1612,
    sortable: true
  },
  {
    name: "Status",
    hide: 600,
    sortable: true,
    cell: (user: IAdminGetUserResponse) => <Status isActive={user.isAccountActive} />
  },
  {
    name: "Address",
    selector: (user: IAdminGetUserResponse) => user?.address ?? "",
    sortable: true,
    hide: 1840
  },
  {
    name: "Action",
    ignoreRowClick: true,
    button: true,
    width: "300px"
  }
];

export const columnFieldMap: Record<string, keyof IAdminGetUserResponse> = {
  Username: "accountUsername",
  Name: "displayName",
  Gmail: "accountEmail",
  "Phone number": "accountPhoneNumber",
  "Date of birth": "dob",
  Status: "isAccountActive",
  Address: "address"
};

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
  const router = useRouter();
  const [active, setActive] = useState<boolean>(isActive);

  const handleDetailClick = () => {
    router.push(`/users/${accountId}`);
  };

  const handleToggleStatus = async () => {
    const result = await adminBanUser(accountId);
    if (result.userId) {
      const newStatus = result.isRestored;
      setActive(newStatus);
      onStatusUpdate(accountId, newStatus);

      if (result.isRestored) {
        toast.success("Restore user successfully!");
      } else {
        toast.success("Disable user successfully!");
      }
    } else {
      toast.error("Something went wrong!");
    }
  };

  return (
    <div className='flex gap-2'>
      <Button
        className='text-white_black bg-primary hover:bg-secondary'
        onClick={handleDetailClick}
      >
        <Search />
        <p className='mt-1 max-sm:hidden'>Detail</p>
      </Button>

      {active ? (
        <Button
          className='text-white_black bg-red hover:bg-red-600'
          onClick={handleToggleStatus}
        >
          <Ban />
          <p className='mt-1 max-sm:hidden'>Disable</p>
        </Button>
      ) : (
        <Button
          className='text-white_black bg-green hover:bg-green/90 dark:bg-green/80'
          onClick={handleToggleStatus}
        >
          <RotateCcw />
          <p className='mt-1 max-sm:hidden'>Restore</p>
        </Button>
      )}
    </div>
  );
};
