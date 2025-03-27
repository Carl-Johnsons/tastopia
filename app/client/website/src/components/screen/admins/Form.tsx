"use client";

import React, { FC, useCallback, useEffect } from "react";
import { ControllerRenderProps, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { toast } from "react-toastify";
import ImageUploading from "@/components/shared/common/ImageUploading";
import { Input } from "@/components/ui/input";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue
} from "@/components/ui/select";
import { useTranslations } from "next-intl";
import { useAdminsContext } from "./Provider";
import { getCreateAdminSchema } from "@/schemas/admin";
import { CreateAdminFormFields } from "@/types/admin";
import { createAdmin } from "@/actions/admin.action";
import { Gender } from "@/constants/gender";
import { ItemStatusText } from "../report/common/StatusText";
import { Popover, PopoverContent, PopoverTrigger } from "@/components/ui/popover";
import { CalendarIcon } from "@/components/shared/icons";
import { format } from "date-fns";
import { Calendar } from "@/components/ui/calendar";
import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import Image from "@/components/shared/common/Image";

type Props = {
  type: "create" | "update";
};

type FormInputProps = {
  field: ControllerRenderProps<CreateAdminFormFields>;
  label: string;
  placeholder?: string;
};

type SelectItemData = {
  value: string;
  label: string;
};

type FormSelectProps = FormInputProps & {
  items: SelectItemData[];
  /** Custom select item child to render label */
  SelectItemChild?: FC<SelectItemData>;
};

type FormDatePickerProps = FormInputProps;
type FormImageUploadProps = Omit<FormInputProps, "placeholder">;

const AdminForm = ({ type }: Props) => {
  const { formStates } = useAdminsContext();
  const { setIsSubmitting, setSubmitForm } = formStates;
  const t = useTranslations("administerAdmins.form");
  const schema = getCreateAdminSchema(t);

  const form = useForm<CreateAdminFormFields>({
    resolver: zodResolver(schema),
    reValidateMode: "onSubmit"
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
      if (image.length === 0) {
        toast.error(t("form.image.required"));
        return;
      }

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
        formData.append("image", image[0].file);

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
        setIsSubmitting(false);
      }
    },
    [t, setIsSubmitting]
  );

  const submitForm = useCallback(() => form.handleSubmit(onSubmit), [form, onSubmit]);

  useEffect(() => {
    setSubmitForm(submitForm);
  }, [submitForm, setSubmitForm]);

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit(onSubmit)}
        className='flex w-full flex-col gap-10'
      >
        <FormField
          control={form.control}
          name='name'
          render={({ field }) => (
            <FormInput
              field={field}
              label={t("name.label")}
              placeholder={t("name.placeholder")}
            />
          )}
        />

        <FormField
          control={form.control}
          name='gmail'
          render={({ field }) => (
            <FormInput
              field={field}
              label={t("gmail.label")}
              placeholder={t("gmail.placeholder")}
            />
          )}
        />

        <FormField
          control={form.control}
          name='phone'
          render={({ field }) => (
            <FormInput
              label={t("phone.label")}
              placeholder={t("phone.placeholder")}
              field={field}
            />
          )}
        />

        <FormField
          control={form.control}
          name='password'
          render={({ field }) => (
            <FormInput
              field={field}
              label={t("password.label")}
              placeholder={t("password.placeholder")}
            />
          )}
        />

        <FormField
          control={form.control}
          name='gender'
          render={({ field }) => (
            <FormSelect
              field={field}
              label={t("gender.label")}
              placeholder={t("gender.placeholder")}
              items={[
                { value: Gender.Male, label: t("gender.options.male") },
                { value: Gender.Female, label: t("gender.options.female") }
              ]}
            />
          )}
        />

        <FormField
          control={form.control}
          name='address'
          render={({ field }) => (
            <FormInput
              field={field}
              label={t("address.label")}
              placeholder={t("address.placeholder")}
            />
          )}
        />

        <FormField
          control={form.control}
          name='dateOfBirth'
          render={({ field }) => (
            <FormDatePicker
              field={field}
              label={t("dateOfBirth.label")}
              placeholder={t("dateOfBirth.placeholder")}
            />
          )}
        />

        <FormField
          control={form.control}
          name='status'
          render={({ field }) => (
            <FormSelect
              field={field}
              label={t("status.label")}
              placeholder={t("status.placeholder")}
              items={[
                { value: "Active", label: t("status.options.active") },
                { value: "Inactive", label: t("status.options.inactive") }
              ]}
              SelectItemChild={({ value }) => (
                <ItemStatusText isActive={value === "active"} />
              )}
            />
          )}
        />

        <FormField
          control={form.control}
          name='image'
          render={({ field }) => (
            <FormImageUpload
              field={field}
              label={t("image.label")}
            />
          )}
        />
      </form>
    </Form>
  );
};

const Askterisk = () => <span className='text-red-600'>*</span>;

const FormInput = ({ field, label, placeholder }: FormInputProps) => {
  return (
    <FormItem className='flex w-full flex-col'>
      <FormLabel className='paragraph-semibold text-black_white'>
        {label} <Askterisk />
      </FormLabel>
      <FormControl>
        <Input
          className='no-focus paragraph-regular light-border text-black_white min-h-[36px] border'
          placeholder={placeholder}
          {...field}
        />
      </FormControl>
      <FormMessage className='text-red-600' />
    </FormItem>
  );
};

const FormSelect = ({
  field,
  label,
  placeholder,
  items,
  SelectItemChild
}: FormSelectProps) => {
  const { onChange, value } = field;

  return (
    <FormItem className='relative'>
      <FormLabel className='paragraph-semibold text-black_white'>
        {label} <span className='text-red-600'>*</span>
      </FormLabel>

      <Select
        onValueChange={onChange}
        defaultValue={value}
      >
        <FormControl>
          <SelectTrigger className='bg-white_black200 text-black_white'>
            <SelectValue placeholder={placeholder} />
          </SelectTrigger>
        </FormControl>

        <SelectContent className='bg-white_black100 text-black_white'>
          {items.map(({ value, label }, index) => (
            <SelectItem
              key={index}
              value={value}
            >
              {SelectItemChild ? (
                <SelectItemChild
                  label={label}
                  value={value}
                />
              ) : (
                label
              )}
            </SelectItem>
          ))}
        </SelectContent>
      </Select>

      <FormMessage className='text-red-600' />
    </FormItem>
  );
};

const FormDatePicker = ({ field, label, placeholder }: FormDatePickerProps) => {
  const { value, onChange } = field;

  return (
    <FormItem className='flex w-full flex-col'>
      <FormLabel className='paragraph-semibold text-black_white'>
        {label} <Askterisk />
      </FormLabel>
      <Popover modal>
        <PopoverTrigger asChild>
          <FormControl>
            <Button
              variant={"outline"}
              className={cn("w-[240px] pl-0", !value && "text-muted-foreground")}
            >
              <div className='text-black_white flex w-full items-center gap-2 px-3'>
                <CalendarIcon className='size-4 text-primary' />
                {value ? format(value, "MM/dd/yyyy") : <span>{placeholder}</span>}
              </div>
            </Button>
          </FormControl>
        </PopoverTrigger>
        <PopoverContent
          className='bg-white_black200 border-black_white w-auto border p-0'
          align='start'
        >
          <Calendar
            mode='single'
            selected={value}
            onSelect={onChange}
            disabled={date => date > new Date() || date < new Date("1900-01-01")}
            className='text-black_white'
            initialFocus
          />
        </PopoverContent>
      </Popover>
      <FormMessage className='text-red-600' />
    </FormItem>
  );
};

const FormImageUpload = ({ label, field }: FormImageUploadProps) => {
  const { value, onChange } = field;

  return (
    <FormItem className='relative flex w-full flex-col'>
      <FormLabel className='paragraph-semibold text-black_white'>
        {label} <Askterisk />
      </FormLabel>
      <FormControl className='mt-3.5'>
        <ImageUploading
          multiple={false}
          value={value}
          onChange={onChange}
          dataURLKey='data_url'
          maxFileSize={15242880}
          acceptType={["jpg", "jpeg", "png", "webp"]}
          ImageComponent={({ src, alt }) => (
            <Image
              src={src}
              alt={alt}
              className='h-[200px] rounded-full'
              width={200}
              height={200}
              wrapperClassName='flex-center'
              skeletonClassName='rounded-full inset-auto'
            />
          )}
        />
      </FormControl>
      <FormMessage className='text-red-600' />
    </FormItem>
  );
};

export default AdminForm;
