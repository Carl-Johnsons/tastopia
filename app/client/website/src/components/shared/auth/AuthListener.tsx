"use server";

import { useEffect } from "react";
import { useSelectUser } from "@/slices/user.slice";

const AuthListener = async () => {
  const user = useSelectUser();

  useEffect(() => {
    console.log("AuthListener: User changed", user);
  }, [user]);

  return null;
};

export default AuthListener;
