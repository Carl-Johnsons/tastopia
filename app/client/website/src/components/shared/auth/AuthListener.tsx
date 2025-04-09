"use client";

import { useCallback, useEffect } from "react";
import { useSession } from "next-auth/react";
import { useAppDispatch } from "@/store/hooks";
import { clearAuthData, saveAuthData } from "@/slices/auth.slice";
import { clearUserData, saveUserData } from "@/slices/user.slice";
import { jwtDecode } from "jwt-decode";
import { Roles } from "@/constants/role";
import { useGetCurrentAdminDetail } from "@/api/admin";

const AuthListener = () => {
  const { data: session } = useSession();
  const dispatch = useAppDispatch();
  const { data: user, refetch: refetchCurrentAdmin } = useGetCurrentAdminDetail({
    enabled: false
  });
  const clearData = useCallback(() => {
    dispatch(clearAuthData());
    dispatch(clearUserData());
  }, [dispatch]);

  useEffect(() => {
    const handle = async () => {
      if (!session) return clearData();
      await refetchCurrentAdmin();
      if (user) dispatch(saveUserData({ ...user }));

      const accessToken = session.accessToken as string;
      const idToken = session.idToken as string;
      const decodedToken = jwtDecode(accessToken) as any;

      dispatch(saveAuthData({ accessToken, idToken, role: decodedToken.role as Roles }));
    };
    handle();
  }, [session, dispatch, clearData, user, refetchCurrentAdmin]);

  return null;
};

export default AuthListener;
