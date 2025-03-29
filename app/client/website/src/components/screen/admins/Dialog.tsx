"use client";

import InteractiveButton from "@/components/shared/common/Button";
import {
  Dialog,
  DialogClose,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger
} from "@/components/ui/dialog";
import useWindowDimensions from "@/hooks/useWindowDimensions";
import { DialogProps } from "@radix-ui/react-dialog";
import { Edit, Plus, X } from "lucide-react";
import { useTranslations } from "next-intl";
import AdminForm from "./Form";
import { useAdminForm } from "@/hooks/form";
import { closeForm, saveAdminData, useSelectAdmin } from "@/slices/admin.slice";
import { useCallback, useMemo } from "react";
import { useAppDispatch } from "@/store/hooks";
import { Button } from "@/components/ui/button";

type Props = DialogProps & {
  buttonClassName?: string;
  /** Callback that is triggered when the Dialog button is clicked. */
  onClick?: () => void;
};

const AdminDialog = ({ buttonClassName, onClick, ...props }: Props) => {
  const { formType, targetId, isFormOpen } = useSelectAdmin();
  const { height } = useWindowDimensions();
  const { form, submitForm, isSubmitting } = useAdminForm({
    formType,
    targetId
  });
  const dispatch = useAppDispatch();
  const tTooltip = useTranslations("administerAdmins.tooltip");
  const tForm = useTranslations("administerAdmins.form");
  const PADDING_Y = 50;

  const isCreate = useMemo(() => formType === "create", [formType]);

  const onClose = useCallback(() => {
    dispatch(closeForm());
  }, [dispatch]);

  const setOpen = useCallback(
    (isOpen: boolean) => {
      dispatch(saveAdminData({ isFormOpen: isOpen }));
    },
    [dispatch]
  );

  return (
    <Dialog
      {...props}
      open={isFormOpen}
      onOpenChange={setOpen}
    >
      <DialogTrigger asChild>
        <InteractiveButton
          onClick={onClick}
          title={tTooltip("create")}
          icon={<Plus className='text-white_black' />}
          className={buttonClassName}
        />
      </DialogTrigger>
      <DialogContent
        className='bg-white_black200 overflow-y-scroll sm:max-w-[525px] [&>button]:hidden'
        style={{ maxHeight: height - 2 * PADDING_Y }}
      >
        <DialogHeader>
          <div className='flex items-center justify-between'>
            <DialogTitle className='text-black_white'>
              {isCreate ? tForm("createAdmin") : tForm("updateAdmin")}
            </DialogTitle>
            <DialogClose
              asChild
              className='text-black_white'
              onClick={onClose}
            >
              <Button className='size-8 rounded-full bg-gray-400 opacity-50 hover:opacity-100 dark:bg-gray-700'>
                <X className='text-black_white' />
              </Button>
            </DialogClose>
          </div>
        </DialogHeader>
        <AdminForm form={form} />
        <DialogFooter>
          <InteractiveButton
            title={isCreate ? tTooltip("create") : tTooltip("update")}
            icon={
              isCreate ? (
                <Plus className='text-white_black' />
              ) : (
                <Edit className='text-white_black' />
              )
            }
            onClick={submitForm}
            isLoading={isSubmitting}
            className='text-white_black rounded-full bg-primary hover:bg-secondary'
          />
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
};

export default AdminDialog;
