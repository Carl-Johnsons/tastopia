"use client";

import { useCallback, useEffect } from "react";
import { useSession } from "next-auth/react";
import { useAppDispatch } from "@/store/hooks";
import { clearAuthData } from "@/slices/auth.slice";
import { clearUserData } from "@/slices/user.slice";
import { clientAxiosInstance } from "@/constants/clientHost";

const AuthListener = () => {
  const { data: session } = useSession();
  const dispatch = useAppDispatch();

  const clearData = useCallback(() => {
    dispatch(clearAuthData());
    dispatch(clearUserData());
  }, []);

  useEffect(() => {
    if (!session) return clearData();

    const accessToken = session.accessToken as string;
    const idToken = session.idToken as string;

    clientAxiosInstance.post("/api/auth/cookie", {
      accessToken,
      idToken,
    });
  }, [session]);

  return null;
};

export default AuthListener;
