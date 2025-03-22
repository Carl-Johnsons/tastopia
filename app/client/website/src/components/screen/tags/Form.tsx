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
import Image from "next/image";
import { CreateTagSchema, UpdateTagSchema } from "@/schemas/tag";
import { FORM_TYPE } from "@/constants/form";
import { toast } from "react-toastify";
import { Button } from "@/components/ui/button";
import ImageUploading from "react-images-uploading";
import { Input } from "@/components/ui/input";
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger
} from "@/components/ui/alert-dialog";
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

type FormProps = {
  type: string;
};

const TagForm = ({ type }: FormProps) => {
  const {
    createTagInContext,
    updateTagInContext,
    setOpenCreateDialog,
    setOpenUpdateDialog,
    getTagToUpdate
  } = useTags();

  const tag = getTagToUpdate();
  const isUpdate = type === FORM_TYPE.UPDATE;

  const [isSubmitting, setIsSubmitting] = useState(false);
  const [image, setImage] = useState<any>(isUpdate ? [tag?.imageUrl] : undefined);
  const [isImageModified, setIsImageModified] = useState(false);

  const schema = isUpdate ? UpdateTagSchema : CreateTagSchema;

  const defaultValues = {
    code: isUpdate ? tag?.code || "" : "",
    value: isUpdate ? tag?.value || "" : "",
    category: isUpdate ? tag?.category || "" : "",
    tagImage: isUpdate ? tag?.imageUrl || "" : "",
    ...(isUpdate && { status: tag?.status || "" })
  };

  const form = useForm<z.infer<typeof schema>>({
    resolver: zodResolver(schema),
    defaultValues
  });

  const handleUploadImage = (imageList: any) => {
    setImage(imageList);
    setIsImageModified(true);
  };

  async function onSubmit(values: z.infer<typeof schema>) {
    if (image.length === 0) {
      toast.error("Image is required");
      return;
    }

    setIsSubmitting(true);

    try {
      const formData = new FormData();

      formData.append("code", values.code);
      formData.append("value", values.value);
      formData.append("category", values.category);

      if (isImageModified || !isUpdate) {
        formData.append("tagImage", image[0].file);
      }

      if (isUpdate) {
        formData.append("tagId", tag?.id ?? "");
        formData.append("status", values.status);

        const data = await updateTag(formData);
        updateTagInContext(data);
        setOpenUpdateDialog(false);
        toast.success("Tag updated successfully");
      } else {
        const data = await createTag(formData);
        createTagInContext(data);
        setOpenCreateDialog(false);
        toast.success("Tag created successfully");
      }
    } catch (error) {
      console.error(error);
      toast.error("Something went wrong. Please try again later.");
    } finally {
      setIsSubmitting(false);
    }
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
                Code <span className='text-red-600'>*</span>
              </FormLabel>
              <FormControl>
                <Input
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
          name='value'
          render={({ field }) => (
            <FormItem className='flex w-full flex-col'>
              <FormLabel className='paragraph-semibold text-black_white'>
                Ingredient name <span className='text-red-600'>*</span>
              </FormLabel>
              <FormControl>
                <Input
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
                    Category <span className='text-red-600'>*</span>
                  </FormLabel>
                  <Select
                    onValueChange={field.onChange}
                    defaultValue={field.value}
                  >
                    <FormControl>
                      <SelectTrigger className='bg-white_black100 text-black_white'>
                        <SelectValue placeholder='Select a category' />
                      </SelectTrigger>
                    </FormControl>
                    <SelectContent className='bg-white_black100 text-black_white'>
                      <SelectItem value='All'>All</SelectItem>
                      <SelectItem value='DishType'>Dish Type</SelectItem>
                      <SelectItem value='Ingredient'>Ingredient</SelectItem>
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
                      Status <span className='text-red-600'>*</span>
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
                        <SelectItem value='Pending'>Pending</SelectItem>
                        <SelectItem value='Active'>Active</SelectItem>
                        <SelectItem value='Inactive'>Inactive</SelectItem>
                      </SelectContent>
                    </Select>
                    <FormMessage />
                  </FormItem>
                )}
              />
            </div>
          )}
        </div>

        <FormItem className='relative flex w-full flex-col'>
          <FormLabel className='paragraph-semibold text-black_white'>
            Image <span className='text-red-600'>*</span>
          </FormLabel>
          <FormControl className='mt-3.5'>
            <ImageUploading
              multiple={false}
              value={image}
              onChange={handleUploadImage}
              maxNumber={1}
              dataURLKey='data_url'
              maxFileSize={15242880}
              acceptType={["jpg", "jpeg", "png", "webp"]}
            >
              {({
                imageList,
                onImageUpload,
                onImageRemoveAll,
                isDragging,
                dragProps,
                errors
              }) => (
                <>
                  <div
                    className={`flex flex-col items-center rounded-2xl ${isDragging ? "bg-gray-400" : ""}`}
                  >
                    {imageList.length === 0 ? (
                      <button
                        className='w-full rounded-md px-1 py-2'
                        onClick={onImageUpload}
                        {...dragProps}
                        type='button'
                      >
                        <div className='flex w-full items-center justify-center'>
                          <div className='flex flex-col items-center justify-center pb-6 pt-5'>
                            <svg
                              className='mb-4 size-8 text-gray-500 dark:text-gray-400'
                              aria-hidden='true'
                              xmlns='http://www.w3.org/2000/svg'
                              fill='none'
                              viewBox='0 0 20 16'
                            >
                              <path
                                stroke='currentColor'
                                strokeLinecap='round'
                                strokeLinejoin='round'
                                strokeWidth='2'
                                d='M13 13h3a3 3 0 0 0 0-6h-.025A5.56 5.56 0 0 0 16 6.5 5.5 5.5 0 0 0 5.207 5.021C5.137 5.017 5.071 5 5 5a4 4 0 0 0 0 8h2.167M10 15V6m0 0L8 8m2-2 2 2'
                              />
                            </svg>
                            <p className='mb-2 text-sm text-gray-500 dark:text-gray-400'>
                              <span className='font-semibold'>Click to upload</span> or
                              drag and drop
                            </p>
                            <p className='text-xs text-gray-500 dark:text-gray-400'>
                              PNG, JPG, JPEG or WEBP (MAX 15MB)
                            </p>
                          </div>
                        </div>
                      </button>
                    ) : (
                      <>
                        <div className='flex w-full flex-col items-center justify-center gap-4'>
                          {imageList.map((image, index) => (
                            <div
                              className='flex w-full items-center justify-center gap-4'
                              key={index}
                            >
                              <Image
                                src={isUpdate && !image.data_url ? image : image.data_url}
                                alt='uploaded image'
                                width={400}
                                height={400}
                                className='max-h-[400px] w-auto rounded-3xl'
                              />
                            </div>
                          ))}

                          <AlertDialog>
                            <AlertDialogTrigger>
                              <Button
                                type='button'
                                className='text-white_black w-fit cursor-pointer bg-primary hover:bg-secondary focus:ring'
                              >
                                Remove image
                              </Button>
                            </AlertDialogTrigger>
                            <AlertDialogContent className='bg-white_black'>
                              <AlertDialogHeader>
                                <AlertDialogTitle className='text-black_white'>
                                  Are you sure you want to delete this image?
                                </AlertDialogTitle>
                                <AlertDialogDescription className='text-black_white'>
                                  Once deleted, it can't be undone.
                                </AlertDialogDescription>
                              </AlertDialogHeader>
                              <AlertDialogFooter>
                                <AlertDialogCancel className='bg-white_black text-black_white hover:bg-gray-100 dark:hover:bg-black-100'>
                                  Cancel
                                </AlertDialogCancel>
                                <AlertDialogAction
                                  className='text-black_white border-none bg-primary hover:bg-secondary'
                                  onClick={() => {
                                    onImageRemoveAll();
                                    setIsImageModified(true);
                                  }}
                                >
                                  Delete
                                </AlertDialogAction>
                              </AlertDialogFooter>
                            </AlertDialogContent>
                          </AlertDialog>
                        </div>
                      </>
                    )}
                  </div>

                  {!errors && image?.length === 0 && (
                    <span className='body-medium text-red-600'>
                      Tag image is required
                    </span>
                  )}

                  {errors && (
                    <div>
                      {errors.maxNumber && (
                        <span className='body-medium text-red-600'>
                          Number of selected images exceed 1
                        </span>
                      )}
                      {errors.acceptType && (
                        <span className='body-medium text-red-600'>
                          Your selected file type is not allowed
                        </span>
                      )}
                      {errors.maxFileSize && (
                        <span className='body-medium text-red-600'>
                          Selected file size exceeds max file size (15MB)
                        </span>
                      )}
                    </div>
                  )}
                </>
              )}
            </ImageUploading>
          </FormControl>
        </FormItem>

        <div className='flex justify-end'>
          <Button
            type='submit'
            className='text-white_black w-fit cursor-pointer bg-primary hover:bg-secondary focus:ring'
            disabled={isSubmitting}
          >
            <Plus />
            {isSubmitting
              ? isUpdate
                ? "Updating..."
                : "Creating..."
              : isUpdate
                ? "Update"
                : "Create"}
          </Button>
        </div>
      </form>
    </Form>
  );
};

export default TagForm;
