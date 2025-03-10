"use client";

import { useSelectUser } from "@/slices/user.slice";

const Income = () => {
  const { displayName } = useSelectUser();

  return (
    <div className="flex size-full justify-center">
      <div className="flex w-full flex-col gap-4">Hi, {displayName}</div>
    </div>
  );
};

export default Income;
