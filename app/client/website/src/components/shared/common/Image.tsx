"use client";

import BaseImage, { ImageProps } from "next/image";
import { ImageOff } from "lucide-react";
import { useState } from "react";

export default function Image({
  className,
  width,
  height,
  onError,
  ...props
}: ImageProps) {
  const [hasError, setHasError] = useState(false);
  if (hasError) return <FallbackComponent />;

  return (
    <BaseImage
      {...props}
      quality={100}
      onError={err => {
        setHasError(true);
        onError && onError(err);
      }}
    />
  );
}

const FallbackComponent = () => (
  <div className='flex-center bg-white_black flex flex-col gap-5 rounded-md border border-transparent p-5 dark:border-gray-500'>
    <ImageOff className='size-5' />
    <p className='text-black_white text-sm'>Oops! The image failed to load.</p>
  </div>
);
