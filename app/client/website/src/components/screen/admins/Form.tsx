"use client";

import React, {
  FC,
  forwardRef,
  memo,
  useCallback,
  useEffect,
  useImperativeHandle,
  useMemo,
  useState
} from "react";
import { ControllerRenderProps } from "react-hook-form";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage
} from "@/components/ui/form";
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
import { Gender } from "@/constants/gender";
import { Popover, PopoverContent, PopoverTrigger } from "@/components/ui/popover";
import { CalendarIcon } from "@/components/shared/icons";
import { format, isValid, parse } from "date-fns";
import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import Image from "@/components/shared/common/Image";
import { ImageListType } from "react-images-uploading";
import { useSelectAdmin } from "@/slices/admin.slice";
import { Calendar } from "@/components/shared/common/Calendar";
import { useAdminForm } from "@/hooks/form";
import { Skeleton } from "@/components/ui/skeleton";

type FormInputProps = {
  field: ControllerRenderProps<any>;
  label: string;
  placeholder?: string;
  isLoading: boolean;
  required?: boolean;
  rounded?: boolean;
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

export type AdminFormRef = {
  submitForm: () => void;
};

const AdminForm = forwardRef<AdminFormRef>((_, ref) => {
  const { formType: type, targetId, isFormLoading } = useSelectAdmin();
  const { form, submitForm } = useAdminForm({
    formType: type,
    targetId
  });

  const t = useTranslations("administerAdmins.form");

  useImperativeHandle(
    ref,
    () => ({
      submitForm
    }),
    [submitForm]
  );

  return (
    <Form {...form}>
      <form className='flex w-full flex-col gap-4'>
        <FormField
          control={form.control}
          name='name'
          render={({ field }) => (
            <FormInput
              field={field}
              label={t("name.label")}
              placeholder={t("name.placeholder")}
              isLoading={isFormLoading}
              required
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
              isLoading={isFormLoading}
              required
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
              isLoading={isFormLoading}
              required
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
              isLoading={isFormLoading}
              required
            />
          )}
        />
        <FormField
          control={form.control}
          name='gender'
          render={({ field }) => (
            <div className='w-[240px]'>
              <FormSelect
                field={field}
                label={t("gender.label")}
                placeholder={t("gender.placeholder")}
                isLoading={isFormLoading}
                required
                items={[
                  { value: Gender.Male, label: t("gender.options.male") },
                  { value: Gender.Female, label: t("gender.options.female") }
                ]}
              />
            </div>
          )}
        />
        <FormField
          control={form.control}
          name='dob'
          render={({ field }) => (
            <div className='w-[240px]'>
              <FormDatePicker
                field={field}
                label={t("dateOfBirth.label")}
                placeholder={t("dateOfBirth.placeholder")}
                isLoading={isFormLoading}
                required
              />
            </div>
          )}
        />
        <FormField
          control={form.control}
          name='avatarFile'
          render={({ field }) => (
            <FormImageUpload
              field={field}
              label={t("image.label")}
              isLoading={isFormLoading}
              required
            />
          )}
        />
      </form>
    </Form>
  );
});

AdminForm.displayName = "AdminForm";

const Askterisk = () => <span className='text-red-600'>*</span>;

const FormInput = ({
  field,
  label,
  placeholder,
  isLoading,
  required
}: FormInputProps) => {
  const { value, ...fields } = field;

  return (
    <FormItem className='flex w-full flex-col'>
      {isLoading ? (
        <>
          <Skeleton className='h-4 w-20' />
          <Skeleton className='h-8 w-full' />
        </>
      ) : (
        <>
          <FormLabel className='paragraph-semibold text-black_white'>
            {label} {required && <Askterisk />}
          </FormLabel>
          <Input
            value={value as string}
            className='no-focus paragraph-regular light-border text-black_white min-h-[36px] border'
            placeholder={placeholder}
            {...fields}
          />
        </>
      )}
      <FormMessage className='text-red-600' />
    </FormItem>
  );
};

const FormSelect = ({
  field,
  label,
  placeholder,
  items,
  SelectItemChild,
  isLoading,
  required
}: FormSelectProps) => {
  const { onChange, value } = field;

  return (
    <FormItem className='relative'>
      {isLoading ? (
        <>
          <SkeletonControl className='h-4 w-20' />
          <SkeletonControl className='h-8 w-60' />
        </>
      ) : (
        <>
          <FormLabel className='paragraph-semibold text-black_white'>
            {label} {required && <Askterisk />}
          </FormLabel>

          <Select
            value={value}
            onValueChange={onChange}
          >
            <FormControl>
              <SelectTrigger className='bg-white_black200 light-border text-black_white'>
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
        </>
      )}

      <FormMessage className='text-red-600' />
    </FormItem>
  );
};

const FormDatePicker = ({
  field,
  label,
  placeholder,
  isLoading,
  required
}: FormDatePickerProps) => {
  const { value, onChange } = field;
  const parsedDate = useMemo(
    () => value && parse(value, "dd/MM/yyyy", new Date()),
    [value]
  );

  const [date, setDate] = useState<Date | undefined>(value && parsedDate);
  const [month, setMonth] = useState(date);

  const handleInputChange = useCallback(
    (e: React.ChangeEvent<HTMLInputElement>) => {
      const value = e.target.value;

      if (!value) {
        setDate(undefined);
        onChange("");
        return;
      }

      onChange(value);
      const parsedDate = parse(value, "dd/MM/yyyy", new Date());

      if (isValid(parsedDate)) {
        setDate(parsedDate);
        setMonth(parsedDate);
      } else {
        setDate(undefined);
      }
    },
    [onChange]
  );

  const handleDayPickerSelect = useCallback(
    (date: Date | undefined) => {
      if (!date) {
        setDate(undefined);
        onChange(undefined);
      } else {
        const formattedDate = format(date, "dd/MM/yyyy");

        setDate(date);
        setMonth(date);
        onChange(formattedDate);
      }
    },
    [onChange]
  );

  return (
    <FormItem className='flex w-full flex-col'>
      {isLoading ? (
        <Skeleton className='h-4 w-20' />
      ) : (
        <FormLabel className='paragraph-semibold text-black_white'>
          {label} {required && <Askterisk />}
        </FormLabel>
      )}

      <div className='flex gap-2'>
        {isLoading ? (
          <>
            <SkeletonControl className='h-8 w-48' />
            <SkeletonControl className='size-9' />
          </>
        ) : (
          <>
            <FormControl className='w-[90%]'>
              <Input
                value={value ?? ""}
                onChange={handleInputChange}
                className='no-focus paragraph-regular light-border text-black_white min-h-[36px] border'
                placeholder={placeholder}
              />
            </FormControl>

            <Popover modal>
              <PopoverTrigger asChild>
                <FormControl>
                  <Button
                    variant={"outline"}
                    className={cn(
                      "bg-white_black200 pl-0",
                      !value && "text-muted-foreground",
                      "light-border w-fit p-0"
                    )}
                  >
                    <div className='text-black_white flex items-center gap-2 px-3'>
                      <CalendarIcon className='size-4 text-primary' />
                    </div>
                  </Button>
                </FormControl>
              </PopoverTrigger>
              <PopoverContent
                className='bg-white_black200 border-black_white w-auto border'
                align='start'
              >
                <Calendar
                  mode='single'
                  selected={date}
                  onSelect={handleDayPickerSelect}
                  month={month}
                  onMonthChange={setMonth}
                  disabled={(date: Date) =>
                    date > new Date() || date < new Date("1900-01-01")
                  }
                  className='text-black_white'
                  autoFocus
                />
              </PopoverContent>
            </Popover>
          </>
        )}
      </div>
      <FormMessage className='text-red-600' />
    </FormItem>
  );
};

export const FormImageUpload = memo(
  ({ label, field, isLoading, required, rounded = true }: FormImageUploadProps) => {
    const { value, onChange } = field;

    return (
      <FormItem className='relative flex w-full flex-col'>
        {isLoading ? (
          <>
            <SkeletonControl className='h-4 w-20' />
            <div className='flex-center'>
              <SkeletonControl
                className={`size-[200px] ${rounded ? "rounded-full" : "h-[300px] w-[90%] rounded-sm"}`}
              />
            </div>
          </>
        ) : (
          <>
            <FormLabel className='paragraph-semibold text-black_white'>
              {label} {required && <Askterisk />}
            </FormLabel>
            <FormControl className='mt-3.5'>
              <ImageUploading
                multiple={false}
                value={value as ImageListType}
                onChange={onChange}
                maxFileSize={15242880}
                acceptType={["jpg", "jpeg", "png", "webp"]}
                ImageComponent={({ src, alt }) => (
                  <Image
                    src={src}
                    alt={alt}
                    className={`size-[200px] ${rounded ? "rounded-full" : "h-[300px] w-[90%] rounded-sm"}`}
                    width={200}
                    height={200}
                    wrapperClassName='flex-center'
                    skeletonClassName='rounded-full inset-auto'
                  />
                )}
              />
            </FormControl>
          </>
        )}

        <FormMessage className='text-red-600' />
      </FormItem>
    );
  }
);

FormImageUpload.displayName = "FormImageUpload";

const SkeletonControl = ({ className }: { className?: string }) => {
  return <Skeleton className={`h-8 w-full ${className}`} />;
};

export default AdminForm;
