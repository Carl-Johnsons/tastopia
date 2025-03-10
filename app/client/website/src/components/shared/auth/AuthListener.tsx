"use client";

import { useEffect } from "react";
import { useSession } from "next-auth/react";
import { useAppDispatch } from "@/store/hooks";
import { clearAuthData, saveAuthData } from "@/slices/auth.slice";
import useSyncUser from "@/hooks/auth/useSyncUser";
import { clearUserData, useSelectUser } from "@/slices/user.slice";

const AuthListener = () => {
  const { data: session } = useSession();
  const dispatch = useAppDispatch();
  const { fetch: fetchUser } = useSyncUser();
  const user = useSelectUser();

  useEffect(() => {
    if (session) {
      dispatch(
        saveAuthData({
          accessToken: session.accessToken as string,
          idToken: session.idToken as string,
        }),
      );

      fetchUser();
    } else {
      dispatch(clearAuthData());
      dispatch(clearUserData());
    }
  }, [session]);

  useEffect(() => {
    console.log("AuthListener: User changed", user);
  }, [user]);

  return null;
};

export default AuthListener;
