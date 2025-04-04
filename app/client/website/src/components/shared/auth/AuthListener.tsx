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
  const { data: user } = useGetCurrentAdminDetail();

  const clearData = useCallback(() => {
    dispatch(clearAuthData());
    dispatch(clearUserData());
  }, [dispatch]);

  useEffect(() => {
    if (!session) return clearData();
    if (user) dispatch(saveUserData({ ...user }));

    const accessToken = session.accessToken as string;
    const idToken = session.idToken as string;
    const decodedToken = jwtDecode(accessToken) as any;

    dispatch(saveAuthData({ accessToken, idToken, role: decodedToken.role as Roles }));
  }, [session, dispatch, clearData, user]);

  return null;
};

export default AuthListener;
