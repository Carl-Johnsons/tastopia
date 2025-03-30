"use server";

import { auth } from "@/auth";
import { Roles } from "@/constants/role";
import { ReactNode } from "react";
import Unauthorized from "./Unauthorized";

type Props = {
  children: ReactNode;
  allowedRoles: Roles[];
};

const Protected = async ({ children, allowedRoles }: Props) => {
  const session = await auth();
  console.log("session", session);

  // TODO: Check roles
  if (!session) {
    return <Unauthorized />;
  }

  return <>{children}</>;
};

export default Protected;
