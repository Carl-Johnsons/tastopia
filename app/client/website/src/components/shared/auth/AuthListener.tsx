"use client";

import { useCallback, useEffect } from "react";
import { useSession } from "next-auth/react";
import { useAppDispatch } from "@/store/hooks";
import { clearAuthData } from "@/slices/auth.slice";
import { clearUserData } from "@/slices/user.slice";

const AuthListener = () => {
  const { data: session } = useSession();
  const dispatch = useAppDispatch();

  const clearData = useCallback(() => {
    dispatch(clearAuthData());
    dispatch(clearUserData());
  }, [dispatch]);

  useEffect(() => {
    if (!session) return clearData();
  }, [session, clearData]);

  return null;
};

export default AuthListener;
