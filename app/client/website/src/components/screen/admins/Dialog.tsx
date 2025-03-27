"use client";

import InteractiveButton from "@/components/shared/common/Button";
import {
  Dialog,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger
} from "@/components/ui/dialog";
import useWindowDimensions from "@/hooks/useWindowDimensions";
import { DialogProps } from "@radix-ui/react-dialog";
import { Edit, Plus } from "lucide-react";
import { useTranslations } from "next-intl";
import AdminForm from "./Form";
import { useCreateAdminForm } from "@/hooks/form";
import { useSelectAdmin } from "@/slices/admin.slice";

type Props = DialogProps & {
  type: "create" | "update";
  buttonClassName?: string;
};

const AdminDialog = ({ type, buttonClassName, ...props }: Props) => {
  const { form, submitForm } = useCreateAdminForm();
  const { isLoading } = useSelectAdmin();
  const tTooltip = useTranslations("administerAdmins.tooltip");
  const tForm = useTranslations("administerAdmins.form");
  const { height } = useWindowDimensions();
  const PADDING_Y = 50;

  return (
    <Dialog {...props}>
      <DialogTrigger asChild>
        <InteractiveButton
          title={type === "create" ? tTooltip("create") : "Update"}
          icon={
            type === "create" ? (
              <Plus className='text-white_black' />
            ) : (
              <Edit className='text-white_black' />
            )
          }
          className={buttonClassName}
        />
      </DialogTrigger>
      <DialogContent
        className='bg-white_black200 overflow-y-scroll sm:max-w-[525px]'
        style={{ maxHeight: height - 2 * PADDING_Y }}
      >
        <DialogHeader>
          <DialogTitle className='text-black_white'>{tForm("title")}</DialogTitle>
        </DialogHeader>
        <AdminForm
          type={type}
          form={form}
          onSubmit={submitForm}
        />
        <DialogFooter>
          <InteractiveButton
            title={type === "create" ? tTooltip("create") : "Update"}
            icon={
              type === "create" ? (
                <Plus className='text-white_black' />
              ) : (
                <Edit className='text-white_black' />
              )
            }
            onClick={submitForm}
            isLoading={isLoading}
            className='text-white_black rounded-full bg-primary hover:bg-secondary'
          />
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
};

export default AdminDialog;
