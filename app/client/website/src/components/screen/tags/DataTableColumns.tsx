import { Button } from "@/components/ui/button";
import { Pencil, Search } from "lucide-react";
import { useRouter } from "@/i18n/navigation";
import { useState } from "react";
import { adminBanUser } from "@/actions/user.action";
import { toast } from "react-toastify";
import { Tag, TagStatus } from "@/types/tag";
import { format } from "date-fns";
import TagStatusComponent from "@/components/ui/TagStatus";
import Image from "next/image";

export const tagsColumns = [
  {
    name: "Code",
    selector: (tag: Tag) => tag?.code,
    sortable: true
  },
  {
    name: "Ingredient name",
    selector: (tag: Tag) => tag?.value,
    sortable: true,
    hide: 1460
  },
  {
    name: "Category",
    selector: (tag: Tag) => tag?.category ?? "",
    hide: 1234,
    sortable: true
  },
  {
    name: "Created at",
    selector: (tag: Tag) => format(new Date(tag?.createdAt), "dd/MM/yyyy") ?? "",
    hide: 1612,
    sortable: true
  },
  {
    name: "Status",
    hide: 600,
    sortable: true,
    cell: (tag: Tag) => <TagStatusComponent status={tag.status} />
  },
  {
    name: "Ingredient Image",
    hide: 1368,
    width: "160px",
    center: true,
    cell: (tag: Tag) => (
      <div className='p-2'>
        <Image
          src={tag.imageUrl ?? "/assets/images/gallery.png"}
          alt={""}
          width={90}
          height={50}
          className='h-[50px] w-[90px] rounded-lg object-cover'
        />
      </div>
    )
  },
  {
    name: "Action",
    ignoreRowClick: true,
    button: true,
    cell: (tag: Tag) => (
      <ActionButtons
        id={tag.id}
        status={tag.status}
      />
    ),
    width: "300px"
  }
];

export const columnFieldMap: Record<string, keyof Tag> = {
  Code: "code",
  "Ingredient name": "value",
  Category: "category",
  "Created at": "createdAt",
  Status: "status"
};

type ActionButtonsProps = {
  id: string;
  status: TagStatus;
};
export const ActionButtons = ({ id, status }: ActionButtonsProps) => {
  const router = useRouter();
  const [active, setActive] = useState<TagStatus>(status);

  const handleDetailClick = () => {
    // router.push(`/users/${id}`);
  };

  const handleToggleStatus = async () => {
    const result = await adminBanUser(id);
    if (result.userId) {
      const newStatus = result.isRestored;
      setActive(newStatus);

      if (result.isRestored) {
        toast.success("Restore user successfully!");
      } else {
        toast.success("Disable user successfully!");
      }
    } else {
      toast.error("Something went wrong!");
    }
  };

  const handleOpenUpdateModel = () => {};

  return (
    <div className='flex gap-2'>
      <Button
        className='text-white_black bg-primary hover:bg-secondary'
        onClick={handleDetailClick}
      >
        <Search />
        <p className='mt-1 max-sm:hidden'>Detail</p>
      </Button>

      <Button
        className='text-white_black bg-primary hover:bg-secondary'
        onClick={handleOpenUpdateModel}
      >
        <Pencil />
        <p className='mt-1 max-sm:hidden'>Update</p>
      </Button>
    </div>
  );
};
