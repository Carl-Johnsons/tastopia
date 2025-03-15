import { IAdminGetUserResponse } from '@/generated/interfaces/user.interface';
import { Button } from '@/components/ui/button';
import { Ban, Eye, RotateCcw } from 'lucide-react';
import { useRouter } from 'next/navigation';
import { useState } from 'react';
import { adminBanUser } from '@/actions/user.action';
import { Bounce, toast } from 'react-toastify';

export const usersColumns = [
  {
    name: "Username",
    selector: (user: IAdminGetUserResponse) => user?.accountUsername,
    width: '160px',
    sortable: true,

  },
  {
    name: "Name",
    selector: (user: IAdminGetUserResponse) => user?.displayName,
    width: '220px',
    sortable: true,
  },
  {
    name: "Gmail",
    selector: (user: IAdminGetUserResponse) => user?.accountEmail ?? "",
    width: '220px',
  },
  {
    name: "Phone number",
    selector: (user: IAdminGetUserResponse) => user?.accountPhoneNumber ?? "",
    width: '150px',
  },
  {
    name: "Date of birth",
    selector: (user: IAdminGetUserResponse) => user?.dob ?? "",
    width: '140px',
  },
  {
    name: "Status",
    selector: (user: IAdminGetUserResponse) => user?.isAccountActive,
    width: '90px',
  },
  {
    name: "Address",
    selector: (user: IAdminGetUserResponse) => user?.address ?? "",
    width: '240px',
    sortable: true,
  },
  {
    name: "Action",
    cell: (user: IAdminGetUserResponse) => <ActionButtons accountId={user.accountId} isActive={user.isAccountActive} />,
    ignoreRowClick: true,
    button: true,
  },
];

export const columnFieldMap: Record<string, keyof IAdminGetUserResponse> = {
  Username: "accountUsername",
  Name: "displayName",
  Gmail: "accountEmail",
  "Phone number": "accountPhoneNumber",
  "Date of birth": "dob",
  Status: "isAccountActive",
  Address: "address",
};

type ActionButtonsProps = {
  accountId: string;
  isActive: boolean;
}

const ActionButtons = ({ accountId, isActive = true }: ActionButtonsProps) => {
  const router = useRouter();
  const [active, setActive] = useState<boolean>(isActive);
  
  const handleDetailClick = () => {
    router.push(`/users/${accountId}`);
  };

  const handleToggleStatus = async () => {
    const result = await adminBanUser(accountId)
    if(result.userId) {
      setActive(result.isRestored)

      if(result.isRestored) {
        toast.success('Restore user successfully!');
      } else {
        toast.success('Disable user successfully!');
      }
    } else {
      toast.error('Something went wrong!');
    }

   
  };

  return (
    <div className="flex gap-2">
      <Button 
        className="rounded-full bg-primary text-white hover:bg-primary/90 dark:bg-primary/80"
        onClick={handleDetailClick}
      >
        <Eye className="mr-2 size-4" /> Detail
      </Button>
      
      {active ? (
        <Button 
          className="rounded-full bg-red text-white hover:bg-red/90 dark:bg-red/80"
          onClick={handleToggleStatus}
        >
          <Ban className="mr-2 size-4" /> Disable
        </Button>
      ) : (
        <Button 
          className="rounded-full bg-green text-white hover:bg-green/90 dark:bg-green/80"
          onClick={handleToggleStatus}
        >
          <RotateCcw className="mr-2 size-4" /> Restore
        </Button>
      )}
    </div>
  );
};