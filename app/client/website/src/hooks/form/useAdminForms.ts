"use client";

import { createAdmin, updateAdmin } from "@/actions/admin.action";
import { Gender } from "@/constants/gender";
import { BinaryStatus } from "@/constants/status";
import { getCreateAdminSchema, getUpdateAdminSchema } from "@/schemas/admin";
import {
  CreateAdminFormFields,
  CreateAdminSchema,
  ImageFieldType,
  UpdateAdminFormFields,
  UpdateAdminSchema
} from "@/types/admin";
import { zodResolver } from "@hookform/resolvers/zod";
import { useTranslations } from "next-intl";
import { BaseSyntheticEvent, useCallback, useEffect, useMemo, useState } from "react";
import { useForm } from "react-hook-form";
import { toast } from "react-toastify";
import { useInvalidateAdmin } from "../query";
import { useQueryClient } from "@tanstack/react-query";
import { useSelectUserId } from "@/slices/user.slice";
import { useGetAdminById } from "@/api/admin";
import { useAppDispatch } from "@/store/hooks";
import { closeForm } from "@/slices/admin.slice";
import { ImageListType } from "react-images-uploading";

type FormType = "create" | "update";

type UseAdminFormParams = {
  formType: FormType;
  targetId?: string;
};

export const useAdminForm = ({ formType, targetId }: UseAdminFormParams) => {
  const tNotification = useTranslations("administerAdmins.notifications");
  const tForm = useTranslations("administerAdmins.form");
  const { invalidateCurrentAdminActivities } = useInvalidateAdmin();
  const queryClient = useQueryClient();
  const dispatch = useAppDispatch();

  const schema = useMemo(() => {
    return formType === "create"
      ? getCreateAdminSchema(tForm)
      : getUpdateAdminSchema(tForm);
  }, [formType, tForm]);

  const currentUserId = useSelectUserId();
  const { data: fetchedAmin } = useGetAdminById(targetId ?? (currentUserId as string));
  const [admin, setAdmin] = useState(fetchedAmin);

  const defaultValues: UpdateAdminFormFields = useMemo(() => {
    const defaultCreateValues = {
      name: "Long",
      gmail: "sdfsdfsd@gmail.com",
      phone: "0987654321",
      password: "asdojasodij@02asokdj",
      gender: Gender.Male,
      dateOfBirth: new Date(),
      address: "Hanoi"
    };

    if (formType === "create") {
      return defaultCreateValues;
    }

    const images: ImageListType = [{ dataURL: admin?.avatarUrl ?? "" }];

    return {
      name: admin?.displayName,
      gmail: admin?.email,
      phone: admin?.phoneNumber,
      dateOfBirth: admin?.dob ? new Date(admin?.dob) : new Date(),
      gender: admin?.gender === Gender.Male ? Gender.Male : Gender.Female,
      address: admin?.address,
      status: admin?.isActive ? BinaryStatus.Active : BinaryStatus.Inactive,
      image: images
    };
  }, [admin, formType]);

  const [isSubmitting, setIsSubmitting] = useState(false);

  const form = useForm<CreateAdminFormFields | UpdateAdminFormFields>({
    resolver: zodResolver(schema),
    values: defaultValues
  });

  const onSubmit = useCallback(
    async (data: CreateAdminFormFields | UpdateAdminFormFields) => {
      setIsSubmitting(true);
      console.log("data", data);

      try {
        const formData = new FormData();

        Object.entries(data).forEach(([key, value]) => {
          if (key === "avatarFile" && !!value) {
            formData.append(key, (value as ImageFieldType)?.at(0)?.file as Blob);
          } else if (key === "dob" && value instanceof Date) {
            formData.append(key, value.toISOString());
          } else if (value !== undefined) {
            formData.append(key, value as string);
          }
        });

        console.log(`sending ${formType} admin request with data:`, formData);

        if (formType === "create") {
          await createAdmin(formData);
          toast.success(tNotification("create.success"));
        } else {
          formData.append("accountId", targetId ?? "");

          await updateAdmin(formData);
          toast.success(tNotification("update.success"));
          await queryClient.invalidateQueries({ queryKey: ["admin", targetId] });
        }

        dispatch(closeForm());
        invalidateCurrentAdminActivities();
      } catch (error) {
        console.error(error);

        if (error instanceof Error) {
          return toast.error(tNotification(`${formType}.${error.message}`));
        }

        return toast.error(tNotification("error"));
      } finally {
        setIsSubmitting(false);
      }
    },
    [
      formType,
      tNotification,
      invalidateCurrentAdminActivities,
      targetId,
      queryClient,
      dispatch
    ]
  );

  const submitForm = useCallback(
    (e?: BaseSyntheticEvent) => {
      form.handleSubmit(onSubmit)(e);
    },
    [onSubmit, form]
  );

  useEffect(() => {
    console.log("fetchedAmin", fetchedAmin);
    if (fetchedAmin) {
      setAdmin(fetchedAmin);
    }
  }, [fetchedAmin]);

  return { submitForm, form, isSubmitting };
};
