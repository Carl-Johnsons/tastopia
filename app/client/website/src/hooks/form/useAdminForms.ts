import { createAdmin } from "@/actions/admin.action";
import { Gender } from "@/constants/gender";
import { BinaryStatus } from "@/constants/status";
import { getCreateAdminSchema } from "@/schemas/admin";
import { setAdminData } from "@/slices/admin.slice";
import { useAppDispatch } from "@/store/hooks";
import { CreateAdminFormFields } from "@/types/admin";
import { zodResolver } from "@hookform/resolvers/zod";
import { useTranslations } from "next-intl";
import { BaseSyntheticEvent, useCallback } from "react";
import { useForm } from "react-hook-form";
import { toast } from "react-toastify";

export const useCreateAdminForm = () => {
  const t = useTranslations("administerAdmins.form");
  const schema = getCreateAdminSchema(t);
  const dispatch = useAppDispatch();

  const form = useForm<CreateAdminFormFields>({
    resolver: zodResolver(schema),
    defaultValues: {
      name: "Long",
      gmail: "sdfsdfsd@gmail.com",
      phone: "0987654321",
      password: "asdojasodij@02asokdj",
      gender: Gender.Male,
      status: BinaryStatus.Active,
      dateOfBirth: new Date(),
      address: "Hanoi"
    }
  });

  const onSubmit = useCallback(
    async ({
      name,
      gmail,
      phone,
      password,
      gender,
      status,
      dateOfBirth,
      address,
      image
    }: CreateAdminFormFields) => {
      dispatch(setAdminData({ isLoading: true }));

      try {
        const formData = new FormData();

        formData.append("name", name);
        formData.append("gmail", gmail);
        formData.append("phone", phone);
        formData.append("password", password);
        formData.append("gender", gender);
        formData.append("status", status);
        formData.append("dateOfBirth", dateOfBirth.toISOString());
        formData.append("address", address);
        formData.append("image", image[0].file as Blob);

        console.log("sending create admin request with data:", formData);
        await createAdmin(formData);
        toast.success(t("notifications.create.success"));
      } catch (error) {
        console.error(error);

        if (error instanceof Error) {
          return toast.error(t(`notifications.create.${error.message}`));
        }

        toast.error(t("notifications.error"));
      } finally {
        dispatch(setAdminData({ isLoading: false }));
      }
    },
    [t, dispatch]
  );

  const submitForm = useCallback(
    (e?: BaseSyntheticEvent) => form.handleSubmit(onSubmit)(e),
    [form, onSubmit]
  );

  return { submitForm, form };
};
