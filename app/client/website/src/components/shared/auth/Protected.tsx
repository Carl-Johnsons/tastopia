"use server";

import { auth } from "@/auth";
import LoginForm from "@/components/screen/login/LoginForm";
import { Roles } from "@/constants/role";
import { ReactNode } from "react";

type Props = {
  children: ReactNode;
  allowedRoles: Roles[];
};

const Protected = async ({ children, allowedRoles }: Props) => {
  const session = await auth();
  console.log("session", session);

  // TODO: Check role
  if (!session) {
    return <Unauthenticated />;
  }

  return <>{children}</>;
};

const Unauthenticated = () => {
  return (
    <div className="h-screen flex-center flex-col gap-5">
      <p>Unauthenticated, please login to continue.</p>
      <LoginForm />
    </div>
  );
};

export default Protected;
