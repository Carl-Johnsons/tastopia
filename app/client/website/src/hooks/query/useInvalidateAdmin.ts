"use client";

import { useSelectUserId } from "@/slices/user.slice";
import { useQueryClient } from "@tanstack/react-query";
import { useCallback } from "react";

export const useInvalidateAdmin = () => {
  const queryClient = useQueryClient();
  const userId = useSelectUserId();

  const invalidateCurrentAdminActivities = useCallback(async () => {
    await queryClient.invalidateQueries({ queryKey: ["currentAdminActivities"] });
    await queryClient.invalidateQueries({ queryKey: ["adminActivities", userId] });
  }, [queryClient, userId]);

  return { invalidateCurrentAdminActivities };
};
