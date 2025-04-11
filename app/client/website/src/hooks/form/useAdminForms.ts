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
import { BaseSyntheticEvent, useCallback, useEffect, useMemo } from "react";
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
import { useErrorHandler } from "../error/useErrorHanler";

type FormType = "create" | "update";

type UseAdminFormParams = {
  formType: FormType;
  targetId?: string;
};

export const useAdminForm = ({ formType, targetId }: UseAdminFormParams) => {
  const tNotification = useTranslations("administerAdmins.notifications");
  const tForm = useTranslations("administerAdmins.form");
  const { invalidateCurrentAdminActivities } = useInvalidateAdmin();
  const { withBareErrorHandler } = useErrorHandler();

  const queryClient = useQueryClient();
  const dispatch = useAppDispatch();
  const { isSelf } = useSelectAdmin();

  const schema = useMemo(() => {
    return formType === "create"
      ? getCreateAdminSchema(tForm)
      : getUpdateAdminSchema(tForm);
  }, [formType, tForm]);

  const currentUserId = useSelectUserId();
  const { data: admin, isLoading } = useGetAdminById(
    targetId ?? (currentUserId as string)
  );

  const defaultValues: Partial<CreateAdminFormFields> | undefined = useMemo(() => {
    if (formType === "create") {
      return undefined;
    }

    const images: ImageListType = [{ dataURL: admin?.avatarUrl ?? "" }];
    const parsedDate = admin?.dob
      ? format(new Date(admin?.dob), "dd/MM/yyyy")
      : undefined;

    const valueNew = {
      name: admin?.displayName,
      gmail: admin?.email,
      phone: admin?.phoneNumber,
      dob: parsedDate,
      gender: admin?.gender ? (admin?.gender as Gender) : undefined,
      address: admin?.address,
      status: admin?.isActive ? BinaryStatus.Active : BinaryStatus.Inactive,
      avatarFile: images
    };

    return valueNew;
  }, [admin, formType]);

  const isUpdate = useMemo(() => formType === "update", [formType]);
  const form = useForm<CreateAdminFormFields | UpdateAdminFormFields>({
    resolver: zodResolver(schema as any),
    values: defaultValues as any
  });

  const onSuccess = useCallback(async () => {
    await queryClient.invalidateQueries({ queryKey: ["admins"] });

    dispatch(closeForm());
    invalidateCurrentAdminActivities();
  }, [dispatch, invalidateCurrentAdminActivities, queryClient]);

  const onSubmit = useCallback(
    async (data: CreateAdminFormFields | UpdateAdminFormFields) => {
      try {
        dispatch(saveAdminData({ isFormSubmiting: true }));
        const formData = new FormData();

        Object.entries(data).forEach(([key, value]) => {
          const isValueExists = !!value;
          if (!isValueExists) return;

          if (isUpdate) {
            const isValueModified = !!(form.formState.dirtyFields as any)[key];
            if (!isValueModified) return;
          }

          if (key === "avatarFile" && !!value) {
            formData.append(key, (value as ImageFieldType)?.at(0)?.file as Blob);
          } else if (key === "dob") {
            const parsedDate = parse(value as string, "dd/MM/yyyy", new Date());
            formData.append(key, parsedDate.toISOString());
          } else {
            formData.append(key, value as string);
          }
        });

        if (formData.values().toArray().length === 0) return;
        console.debug("submitForm for", formType, "with values", formData);

        if (formType === "create") {
          await withBareErrorHandler(() => createAdmin(formData), {
            onSuccess: async () => {
              toast.success(tNotification("create.success"));
              await onSuccess();
            }
          });
        } else {
          formData.append("accountId", targetId ?? "");

          await withBareErrorHandler(() => updateAdmin(formData), {
            onSuccess: async () => {
              toast.success(tNotification("update.success"));

              if (isSelf) {
                await queryClient.invalidateQueries({ queryKey: ["currentAdmin"] });
              }

              await queryClient.invalidateQueries({ queryKey: ["admin", targetId] });
              await onSuccess();
            }
          });
        }
      } catch (error) {
        console.log(error);
        toast.error(tNotification("error"));
      } finally {
        dispatch(saveAdminData({ isFormSubmiting: false }));
      }
    },
    [
      onSuccess,
      withBareErrorHandler,
      form.formState.dirtyFields,
      isUpdate,
      formType,
      tNotification,
      targetId,
      queryClient,
      isSelf,
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
    if (isLoading && !admin) {
      dispatch(saveAdminData({ isFormLoading: true }));
    } else {
      dispatch(saveAdminData({ isFormLoading: false }));
    }
  }, [isLoading, admin, dispatch]);

  return { submitForm, form };
};
