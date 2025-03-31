"use client";

import { createAdmin, updateAdmin } from "@/actions/admin.action";
import { Gender } from "@/constants/gender";
import { BinaryStatus } from "@/constants/status";
import { getCreateAdminSchema, getUpdateAdminSchema } from "@/schemas/admin";
import {
  CreateAdminFormFields,
  ImageFieldType,
  UpdateAdminFormFields
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
import { closeForm, saveAdminData, useSelectAdmin } from "@/slices/admin.slice";
import { ImageListType } from "react-images-uploading";
import { format, parse } from "date-fns";

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
  const { isSelf } = useSelectAdmin();

  const schema = useMemo(() => {
    return formType === "create"
      ? getCreateAdminSchema(tForm)
      : getUpdateAdminSchema(tForm);
  }, [formType, tForm]);

  const currentUserId = useSelectUserId();
  const { data: fetchedAmin, isLoading } = useGetAdminById(
    targetId ?? (currentUserId as string)
  );
  const [admin, setAdmin] = useState(fetchedAmin);

  useEffect(() => {
    if (isLoading || !fetchedAmin) {
      dispatch(saveAdminData({ isFormLoading: true }));
    } else {
      dispatch(saveAdminData({ isFormLoading: false }));
    }
  }, [isLoading, fetchedAmin, dispatch]);

  const defaultValues: Partial<CreateAdminFormFields> = useMemo(() => {
    if (formType === "create") {
      return {
        name: undefined,
        gmail: undefined,
        phone: undefined,
        dob: undefined,
        gender: undefined,
        address: undefined,
        avatarFile: undefined,
        password: undefined
      };
    }

    const images: ImageListType = [{ dataURL: admin?.avatarUrl ?? "" }];
    const parsedDate = admin?.dob
      ? format(new Date(admin?.dob), "dd/MM/yyyy")
      : undefined;

    return {
      name: admin?.displayName,
      gmail: admin?.email,
      phone: admin?.phoneNumber,
      dob: parsedDate,
      gender: admin?.gender ? (admin?.gender as Gender) : undefined,
      address: admin?.address,
      status: admin?.isActive ? BinaryStatus.Active : BinaryStatus.Inactive,
      avatarFile: images
    };
  }, [admin, formType]);

  const [isSubmitting, setIsSubmitting] = useState(false);

  const isUpdate = useMemo(() => formType === "update", [formType]);
  const form = useForm<CreateAdminFormFields | UpdateAdminFormFields>({
    resolver: zodResolver(schema as any),
    values: defaultValues as any
  });

  const onSubmit = useCallback(
    async (data: CreateAdminFormFields | UpdateAdminFormFields) => {
      setIsSubmitting(true);

      try {
        const formData = new FormData();

        Object.entries(data).forEach(([key, value]) => {
          if (isUpdate) {
            const isValueModified = !!(form.formState.dirtyFields as any)[key];
            if (!isValueModified) return;
          }

          if (key === "avatarFile" && !!value) {
            formData.append(key, (value as ImageFieldType)?.at(0)?.file as Blob);
          } else if (key === "dob") {
            const parsedDate = parse(value as string, "dd/MM/yyyy", new Date());
            formData.append(key, parsedDate.toISOString());
          } else if (value !== undefined) {
            formData.append(key, value as string);
          }
        });

        if (formData.values().toArray().length === 0) return;

        if (formType === "create") {
          await createAdmin(formData);
          toast.success(tNotification("create.success"));
        } else {
          formData.append("accountId", targetId ?? "");

          await updateAdmin(formData);
          toast.success(tNotification("update.success"));
          await queryClient.invalidateQueries({ queryKey: ["admin", targetId] });

          if (isSelf) {
            await queryClient.invalidateQueries({ queryKey: ["admin", targetId] });
          }
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
      form.formState.dirtyFields,
      isUpdate,
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
    if (fetchedAmin) {
      setAdmin(fetchedAmin);
    }
  }, [fetchedAmin]);

  return { submitForm, form, isSubmitting };
};
