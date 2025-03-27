import { createAdmin } from "@/actions/admin.action";
import { Gender } from "@/constants/gender";
import { BinaryStatus } from "@/constants/status";
import { getCreateAdminSchema } from "@/schemas/admin";
import { CreateAdminFormFields } from "@/types/admin";
import { zodResolver } from "@hookform/resolvers/zod";
import { useTranslations } from "next-intl";
import { BaseSyntheticEvent, useCallback, useMemo, useState } from "react";
import { useForm } from "react-hook-form";
import { toast } from "react-toastify";

export const useCreateAdminForm = () => {
  const tNotification = useTranslations("administerAdmins.notifications");
  const tForm = useTranslations("administerAdmins.form");
  const schema = useMemo(() => getCreateAdminSchema(tForm), [tForm]);

  const [isSubmitting, setIsSubmitting] = useState(false);

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
      setIsSubmitting(true);

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
        toast.success(tNotification("create.success"));
      } catch (error) {
        console.error(error);

        if (error instanceof Error) {
          return toast.error(tNotification(`create.${error.message}`));
        }

        toast.error(tNotification("error"));
      } finally {
        setIsSubmitting(false);
      }
    },
    [tNotification]
  );

  const submitForm = useCallback(
    (e?: BaseSyntheticEvent) => form.handleSubmit(onSubmit)(e),
    [form, onSubmit]
  );

  return { submitForm, form, isSubmitting };
};
