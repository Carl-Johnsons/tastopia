import { useTranslations } from "next-intl";
import BaseImageUploading, { ImageUploadingPropsType } from "react-images-uploading";
import { ExportInterface } from "react-images-uploading/dist/typings";
import Image, { ImageProps } from "./Image";
import { FC } from "react";
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
import { Button } from "@/components/ui/button";

type UploadButtonProps = Pick<ExportInterface, "onImageUpload" | "dragProps">;
type ImagePreviewProps = Pick<ExportInterface, "imageList" | "onImageRemoveAll"> & {
  /** Custom image component for image preview. */
  ImageComponent?: FC<Pick<ImageProps, "src" | "alt">>;
};
type ErrorsProps = Pick<ExportInterface, "errors" | "imageList">;
export type ImageUploadingProps = Omit<ImageUploadingPropsType, "maxNumber"> &
  Pick<ImagePreviewProps, "ImageComponent"> & {
    /** Custom upload button component. */
    UploadButtonComponent?: FC<UploadButtonProps>;
    /** Custom image preview component. */
    ImagePreviewComponent?: FC<ImagePreviewProps>;
    /** Custom errors component. */
    ErrorsComponent?: FC<ErrorsProps>;
  };

/**
 * A custom image uploading component.
 * Currently only support uploading one image at a time.
 */
export default function ImageUploading({
  UploadButtonComponent,
  ImagePreviewComponent,
  ErrorsComponent,
  ImageComponent,
  ...props
}: ImageUploadingProps) {
  return (
    <BaseImageUploading
      acceptType={["jpg", "jpeg", "png", "webp"]}
      maxNumber={1}
      {...props}
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
              UploadButtonComponent ? (
                <UploadButtonComponent
                  onImageUpload={onImageUpload}
                  dragProps={dragProps}
                />
              ) : (
                <UploadButton
                  onImageUpload={onImageUpload}
                  dragProps={dragProps}
                />
              )
            ) : ImagePreviewComponent ? (
              <ImagePreviewComponent
                imageList={imageList}
                onImageRemoveAll={onImageRemoveAll}
                ImageComponent={ImageComponent}
              />
            ) : (
              <ImagePreview
                imageList={imageList}
                onImageRemoveAll={onImageRemoveAll}
                ImageComponent={ImageComponent}
              />
            )}
          </div>

          {ErrorsComponent ? (
            <ErrorsComponent
              errors={errors}
              imageList={imageList}
            />
          ) : (
            <Errors
              errors={errors}
              imageList={imageList}
            />
          )}
        </>
      )}
    </BaseImageUploading>
  );
}

const UploadButton = ({ onImageUpload, dragProps }: UploadButtonProps) => {
  const t = useTranslations("administerAdmins.form.image.upload");

  return (
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
            <span className='font-semibold'>{t("clickToUpload")}</span> {t("dragAndDrop")}
          </p>
          <p className='text-xs text-gray-500 dark:text-gray-400'>{t("fileTypes")}</p>
        </div>
      </div>
    </button>
  );
};

const ImagePreview = ({
  imageList,
  onImageRemoveAll,
  ImageComponent
}: ImagePreviewProps) => {
  const t = useTranslations("administerAdmins.form.image.upload");

  return (
    <div className='flex-center w-full flex-col gap-4'>
      {imageList.map((image, index) => (
        <div
          className='flex-center w-full gap-4'
          key={index}
        >
          {ImageComponent ? (
            <ImageComponent
              src={image.data_url}
              alt='uploaded image'
            />
          ) : (
            <Image
              src={image.data_url}
              alt='uploaded image'
              width={400}
              height={400}
              className='max-h-[400px] w-auto rounded-3xl'
              wrapperClassName='w-fit'
            />
          )}
        </div>
      ))}

      <AlertDialog>
        <AlertDialogTrigger>
          <Button
            type='button'
            className='text-white_black w-fit cursor-pointer bg-primary hover:bg-secondary focus:ring'
          >
            {t("removeImage")}
          </Button>
        </AlertDialogTrigger>
        <AlertDialogContent className='bg-white_black'>
          <AlertDialogHeader>
            <AlertDialogTitle className='text-black_white'>
              {t("confirmDelete.title")}
            </AlertDialogTitle>
            <AlertDialogDescription className='text-black_white'>
              {t("confirmDelete.description")}
            </AlertDialogDescription>
          </AlertDialogHeader>
          <AlertDialogFooter>
            <AlertDialogCancel className='bg-white_black text-black_white hover:bg-gray-100 dark:hover:bg-black-100'>
              {t("confirmDelete.cancel")}
            </AlertDialogCancel>
            <AlertDialogAction
              className='text-black_white border-none bg-primary hover:bg-secondary'
              onClick={() => {
                onImageRemoveAll();
              }}
            >
              {t("confirmDelete.confirm")}
            </AlertDialogAction>
          </AlertDialogFooter>
        </AlertDialogContent>
      </AlertDialog>
    </div>
  );
};

const Errors = ({ errors, imageList }: ErrorsProps) => {
  const t = useTranslations("administerAdmins.form.image.errors");

  return (
    <div className='body-medium text-red-600'>
      {/* {!errors && imageList.length === 0 && <span>{t("required")}</span>} */}
      {errors && (
        <>
          {errors.maxNumber && <span>{t("maxNumber")}</span>}
          {errors.acceptType && <span>{t("acceptType")}</span>}
          {errors.maxFileSize && <span>{t("maxFileSize")}</span>}
        </>
      )}
    </div>
  );
};
