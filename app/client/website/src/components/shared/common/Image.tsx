"use client";

import BaseImage, { ImageProps as BaseImageProps } from "next/image";
import { ImageOff } from "lucide-react";
import { useCallback, useMemo, useState } from "react";
import { Skeleton } from "@/components/ui/skeleton";

export type ImageProps = BaseImageProps & {
  wrapperClassName?: string;
  skeletonClassName?: string;
};

function Image({
  width,
  height,
  onError,
  wrapperClassName,
  skeletonClassName,
  ...props
}: ImageProps) {
  const [isLoading, setIsLoading] = useState(true);
  const [hasError, setHasError] = useState(false);

  const onLoadEnd = useCallback(() => {
    setIsLoading(false);
  }, []);

  if (hasError)
    return (
      <FallbackComponent
        width={width}
        height={height}
        fill={props.fill}
      />
    );

  return (
    <div className={`relative size-full ${wrapperClassName}`}>
      {isLoading && (
        <Skeleton
          className={`absolute inset-0 ${skeletonClassName}`}
          style={{ width, height }}
        />
      )}
      <BaseImage
        {...props}
        width={width}
        height={height}
        quality={100}
        onLoad={onLoadEnd}
        onError={err => {
          setHasError(true);
          onError && onError(err);
        }}
      />
    </div>
  );
}

type FallbackComponentProps = Pick<ImageProps, "width" | "height" | "fill">;

const FallbackComponent = ({ fill, ...props }: FallbackComponentProps) => {
  let width = props.width;
  let height = props.height;

  if (typeof width === "string") {
    width = parseInt(width);
  }

  if (typeof height === "string") {
    height = parseInt(height);
  }

  const isBig = useMemo(
    () => (!!width && width > 100) || (!!height && height > 100),
    [width, height]
  );

  return (
    <div
      className={`flex-center flex-col gap-5 rounded-md border border-gray-500 bg-transparent ${isBig && "p-5"} ${fill && "size-full"}`}
      style={{ width, height }}
      aria-label='Image failed to load'
    >
      <ImageOff className='size-1/2 text-gray-500' />
      {!fill && isBig && (
        <p className='text-center text-lg text-gray-500'>
          Oops! The image failed to load.
        </p>
      )}
    </div>
  );
};

export default Image;
