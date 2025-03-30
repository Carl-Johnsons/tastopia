"use client";

import { useCallback, useEffect } from "react";
import { useSession } from "next-auth/react";
import { useAppDispatch } from "@/store/hooks";
import { clearAuthData, saveAuthData } from "@/slices/auth.slice";
import { clearUserData, saveUserData } from "@/slices/user.slice";
import { useGetCurrentUser } from "@/api/user";
import { jwtDecode } from "jwt-decode";
import { Roles } from "@/constants/role";

const AuthListener = () => {
  const { data: session } = useSession();
  const dispatch = useAppDispatch();
  const { refetch } = useGetCurrentUser();

  const clearData = useCallback(() => {
    dispatch(clearAuthData());
    dispatch(clearUserData());
  }, [dispatch]);

  const fetchUserDetails = useCallback(async () => {
    try {
      const { data } = await refetch();
      if (data) dispatch(saveUserData({ ...data }));
    } catch (error) {
      console.error(error);
    }
  }, [refetch, dispatch]);

  useEffect(() => {
    if (!session) return clearData();

    fetchUserDetails();
    const accessToken = session.accessToken as string;
    const idToken = session.idToken as string;
    const decodedToken = jwtDecode(accessToken) as any;

    dispatch(saveAuthData({ accessToken, idToken, role: decodedToken.role as Roles }));

    // clientAxiosInstance.post("/api/auth/cookie", {
    // accessToken,
    // idToken
    // });
  }, [session, dispatch, clearData, fetchUserDetails]);

  return null;
};

export default AuthListener;
