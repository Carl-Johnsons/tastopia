import { useMemo } from "react";
import { selectUserId } from "@/slices/user.slice";

const useIsOwner = (authorId: string) => {
  const currentUserId = selectUserId();

  return useMemo(() => currentUserId === authorId, [currentUserId, authorId]);
};

export default useIsOwner;
