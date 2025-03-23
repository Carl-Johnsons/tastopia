"use client";

import { Button } from "@/components/ui/button";
import { useRouter } from "@/i18n/navigation";
import React from "react";

const NotFound = () => {
  const router = useRouter();

  return (
    <div className='flex-center flex h-screen w-screen flex-col gap-4'>
      <span>The content you are searching for does not exist.</span>
      <Button
        onClick={router.back}
        className='text-white hover:text-black'
      >
        Go back
      </Button>
    </div>
  );
};

export default NotFound;
