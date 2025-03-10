"use client";

import { DUENDE_IDENTITY_PROVIDER_NAME } from "@/constants/api";
import { Roles } from "@/constants/role";
import { useSelectRole } from "@/slices/auth.slice";
import { useSelectUser } from "@/slices/user.slice";
import { signIn, useSession } from "next-auth/react";
import { ReactNode, useCallback } from "react";

type Props = {
  children: ReactNode;
  allowedRoles: Roles[];
};

const Protected = ({ children, allowedRoles }: Props) => {
  const { data: session } = useSession();
  const role = useSelectRole();
  const { isAdmin } = useSelectUser();

  if (!session || (role && !allowedRoles.includes(role))) {
    return <Unauthenticated />;
  }

  if (!isAdmin) {
    return (
      <div className="h-screen flex-center">
        <span>Loading...</span>
      </div>
    );
  }

  return <>{children}</>;
};

const Unauthenticated = () => {
  const login = useCallback(async () => {
    signIn(DUENDE_IDENTITY_PROVIDER_NAME);
  }, [DUENDE_IDENTITY_PROVIDER_NAME]);

  return (
    <div className="h-screen flex-center">
      <span>
        Unauthenticated, please{" "}
        <button onClick={login} className="text-blue-500">
          <span>login</span>
        </button>{" "}
        to continue.
      </span>
    </div>
  );
};

export default Protected;
