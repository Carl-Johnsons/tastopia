"use client";

import React, { useState } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage
} from "@/components/ui/form";
import { z } from "zod";
import { getTagSchema, validVietnameseCategories } from "@/schemas/tag";
import { FORM_TYPE } from "@/constants/form";
import { toast } from "react-toastify";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue
} from "@/components/ui/select";
import { Plus } from "lucide-react";
import { createTag, updateTag } from "@/actions/tag.action";
import { useTags } from "./TagsContext";
import { useLocale, useTranslations } from "next-intl";
import { useInvalidateAdmin } from "@/hooks/query";
import { useErrorHandler } from "@/hooks/error/useErrorHanler";
import { Tag } from "@/types/tag";
import { FormImageUpload } from "../admins/Form";
import { ImageFieldType } from "@/types/admin";

type FormProps = {
  type: string;
};

const mapCategoryByLocale = (category: string, language: string) => {
  const viMap: Record<string, string> = {
    "Tất cả": "All",
    "Loại món ăn": "DishType",
    "Nguyên liệu": "Ingredient"
  };

  const enMap: Record<string, string> = {
    All: "Tất cả",
    DishType: "Loại món ăn",
    Ingredient: "Nguyên liệu"
  };

  return language === "vi" ? viMap[category] : enMap[category];
};

const TagForm = ({ type }: FormProps) => {
  const {
    createTagInContext,
    updateTagInContext,
    setOpenCreateDialog,
    setOpenUpdateDialog,
    getTagToUpdate
  } = useTags();
  const currentLanguage = useLocale();
  const t = useTranslations("administerTags");
  const { invalidateCurrentAdminActivities } = useInvalidateAdmin();

  const tag = getTagToUpdate();
  const isUpdate = type === FORM_TYPE.UPDATE;

  const [isSubmitting, setIsSubmitting] = useState(false);

  const { withBareErrorHandler } = useErrorHandler();

  const { CreateTagSchema, UpdateTagSchema } = getTagSchema(t, currentLanguage);
  const schema = isUpdate ? UpdateTagSchema : CreateTagSchema;

  type TagFormValues = z.infer<typeof CreateTagSchema> | z.infer<typeof UpdateTagSchema>;

  const defaultValues = {
    code: isUpdate ? tag?.code || "" : "",
    vi: isUpdate ? tag?.vi || "" : "",
    en: isUpdate ? tag?.en || "" : "",
    category: isUpdate ? tag?.category || "" : "",
    tagImage: isUpdate ? [{ dataURL: tag?.imageUrl ?? "" }] : undefined,
    ...(isUpdate && { status: tag?.status || "" })
  };

  const form = useForm<TagFormValues>({
    resolver: zodResolver(schema),
    defaultValues: defaultValues as any
  });

  async function onSubmit(values: TagFormValues) {
    setIsSubmitting(true);

    const formData = new FormData();

    Object.entries(values).forEach(([key, value]) => {
      const isValueExists = !!value;
      if (!isValueExists) return;

      if (key === "tagImage") {
        formData.append(key, (value as ImageFieldType)?.at(0)?.file as Blob);
      }

      if (key === "category") {
        formData.append(
          "category",
          validVietnameseCategories.includes(values.category)
            ? mapCategoryByLocale(values.category, "vi")
            : values.category
        );
      } else {
        formData.append(key, value as string);
      }
    });

    if (isUpdate) {
      formData.append("tagId", tag?.id ?? "");
      if ("status" in values) {
        formData.append("status", values.status);
      }

      await withBareErrorHandler<Tag>(() => updateTag(formData), {
        onSuccess: data => {
          /** Need to map to Vietnamese because data return is English */
          updateTagInContext({
            ...data,
            category:
              currentLanguage === "vi"
                ? mapCategoryByLocale(data.category, "en")
                : data.category
          });
          setOpenUpdateDialog(false);
          toast.success(t("notifications.updateSuccess"));
          invalidateCurrentAdminActivities();
        }
      });
    } else {
      await withBareErrorHandler<Tag>(() => createTag(formData), {
        onSuccess: data => {
          createTagInContext(data);
          setOpenCreateDialog(false);
          toast.success(t("notifications.createSuccess"));
          invalidateCurrentAdminActivities();
        }
      });
    }

    setIsSubmitting(false);
  }

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className='flex w-full flex-col gap-10'
      >
        <FormField
          control={form.control}
          name='code'
          render={({ field }) => (
            <FormItem className='flex w-full flex-col'>
              <FormLabel className='paragraph-semibold text-black_white'>
                {t("form.code.label")} <span className='text-red-600'>*</span>
              </FormLabel>
              <FormControl>
                <Input
                  placeholder={t("form.code.placeholder")}
                  className='no-focus paragraph-regular light-border text-black_white min-h-[36px] border'
                  {...field}
                />
              </FormControl>
              <FormMessage className='text-red-600' />
            </FormItem>
          )}
        />

        <FormField
          control={form.control}
          name='vi'
          render={({ field }) => (
            <FormItem className='flex w-full flex-col'>
              <FormLabel className='paragraph-semibold text-black_white'>
                {t("form.valueVi.label")} <span className='text-red-600'>*</span>
              </FormLabel>
              <FormControl>
                <Input
                  placeholder={t("form.valueVi.placeholder")}
                  className='no-focus paragraph-regular light-border text-black_white min-h-[36px] border'
                  {...field}
                />
              </FormControl>
              <FormMessage className='text-red-600' />
            </FormItem>
          )}
        />

        <FormField
          control={form.control}
          name='en'
          render={({ field }) => (
            <FormItem className='flex w-full flex-col'>
              <FormLabel className='paragraph-semibold text-black_white'>
                {t("form.valueEn.label")} <span className='text-red-600'>*</span>
              </FormLabel>
              <FormControl>
                <Input
                  placeholder={t("form.valueEn.placeholder")}
                  className='no-focus paragraph-regular light-border text-black_white min-h-[36px] border'
                  {...field}
                />
              </FormControl>
              <FormMessage className='text-red-600' />
            </FormItem>
          )}
        />

        <div className='flex flex-row gap-2'>
          <div className='flex-1'>
            <FormField
              control={form.control}
              name='category'
              render={({ field }) => (
                <FormItem className='relative'>
                  <FormLabel className='paragraph-semibold text-black_white'>
                    {t("form.category.label")} <span className='text-red-600'>*</span>
                  </FormLabel>
                  <Select
                    onValueChange={field.onChange}
                    defaultValue={
                      currentLanguage === "en"
                        ? field.value
                        : mapCategoryByLocale(field.value, currentLanguage)
                    }
                  >
                    <FormControl>
                      <SelectTrigger className='bg-white_black100 text-black_white'>
                        <SelectValue placeholder={t("form.category.placeholder")} />
                      </SelectTrigger>
                    </FormControl>
                    <SelectContent className='bg-white_black100 text-black_white'>
                      <SelectItem value='All'>
                        {t("form.category.options.all")}
                      </SelectItem>
                      <SelectItem value='DishType'>
                        {t("form.category.options.dishType")}
                      </SelectItem>
                      <SelectItem value='Ingredient'>
                        {t("form.category.options.ingredient")}
                      </SelectItem>
                    </SelectContent>
                  </Select>
                  <FormMessage className='text-red-600' />
                </FormItem>
              )}
            />
          </div>

          {isUpdate && (
            <div className='flex-1'>
              <FormField
                control={form.control}
                name='status'
                render={({ field }) => (
                  <FormItem className='relative'>
                    <FormLabel className='paragraph-semibold text-black_white'>
                      {t("form.status.label")} <span className='text-red-600'>*</span>
                    </FormLabel>
                    <Select
                      onValueChange={field.onChange}
                      defaultValue={field.value}
                    >
                      <FormControl>
                        <SelectTrigger className='bg-white_black100 text-black_white'>
                          <SelectValue placeholder='Select status' />
                        </SelectTrigger>
                      </FormControl>
                      <SelectContent className='bg-white_black100 text-black_white'>
                        <SelectItem value='Pending'>
                          {t("form.status.options.pending")}
                        </SelectItem>
                        <SelectItem value='Active'>
                          {t("form.status.options.active")}
                        </SelectItem>
                        <SelectItem value='Inactive'>
                          {t("form.status.options.inactive")}
                        </SelectItem>
                      </SelectContent>
                    </Select>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
          )}
        </div>

        <FormField
          control={form.control}
          name='tagImage'
          render={({ field }) => {
            return (
              <FormImageUpload
                field={field}
                label={t("form.image.label")}
                isLoading={false}
              />
            );
          }}
        />

        <div className='flex justify-end'>
          <Button
            type='submit'
            className='text-white_black w-fit cursor-pointer bg-primary hover:bg-secondary focus:ring'
            disabled={isSubmitting}
          >
            <Plus />
            {isSubmitting
              ? isUpdate
                ? t("form.updating")
                : t("form.creating")
              : isUpdate
                ? t("actions.update")
                : t("actions.create")}
          </Button>
        </div>
      </form>
    </Form>
  );
};

export default TagForm;
