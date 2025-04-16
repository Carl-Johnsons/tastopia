import { Button } from "@/components/ui/button";
import { Pencil } from "lucide-react";
import { Tag, TagStatus } from "@/types/tag";
import { format } from "date-fns";
import TagStatusComponent from "@/components/ui/ReportStatus";
import Image from "@/components/shared/common/Image";
import { useTags } from "./TagsContext";
import { useTranslations } from "next-intl";

export const tagsColumns = (t: any, currentLanguage: string) => [
  {
    name: t("columns.code"),
    selector: (tag: Tag) => tag?.code,
    sortable: true
  },
  {
    name: t("columns.value"),
    selector: (tag: Tag) => (currentLanguage === "vi" ? tag.vi : tag.en),
    sortable: true,
    hide: 1460
  },
  {
    name: t("columns.category"),
    selector: (tag: Tag) => tag?.category ?? "",
    hide: 1234,
    sortable: true
  },
  {
    name: t("columns.createDate"),
    selector: (tag: Tag) => format(new Date(tag?.createdAt), "dd/MM/yyyy") ?? "",
    hide: 1612,
    sortable: true
  },
  {
    name: t("columns.status"),
    hide: 600,
    sortable: true,
    cell: (tag: Tag) => <TagStatusComponent status={tag.status} />
  },
  {
    name: t("columns.image"),
    hide: 1368,
    width: "160px",
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
    name: t("columns.action"),
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

type ActionButtonsProps = {
  id: string;
  status: TagStatus;
};
export const ActionButtons = ({ id, status }: ActionButtonsProps) => {
  const t = useTranslations("administerTags.actions");
  const { setTagIdToUpdate, setOpenUpdateDialog } = useTags();

  const handleOpenUpdateModel = () => {
    setTagIdToUpdate(id);
    setOpenUpdateDialog(true);
  };

  return (
    <Button
      className='text-white_black bg-primary hover:bg-secondary'
      onClick={handleOpenUpdateModel}
    >
      <Pencil />
      <p className='mt-1 max-sm:hidden'>{t("update")}</p>
    </Button>
  );
};
