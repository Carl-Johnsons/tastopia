import { ROLE, selectRole } from "@/slices/auth.slice";
import { router, useRouter } from "expo-router";
import { useEffect, useState } from "react";

/**
 * A hook that wraps around a function to litmit access to a predefined user
 * role.
 *
 * @param callback The callback function that is indented to be protected.
 * @param roles An array specifying the roles could access the function.
 * @returns A protected function that enforces role-based access control.
 */
export const useProtected = (callback: () => any, roles: Array<ROLE>) => {
  const currentUserRole = selectRole();
  const hasAccess = currentUserRole && roles.includes(currentUserRole);

  const protectedFn = () => {
    if (hasAccess) callback();
    else router.push("/login");
  };

  return protectedFn;
};

/**
 * A hook that wraps around a function to restrict access for specified user roles.
 *
 * @param callback - The function intended to be protected.
 * @param excludedRoles - An array specifying the roles that are not permitted to access the function.
 * @returns A protected function that enforces role-based access control.
 */
export const useProtectedExclude = (
  callback: () => any,
  excludedRoles: ROLE[]
): (() => void) => {
  const currentUserRole = selectRole();
  const isExcluded = currentUserRole && excludedRoles.includes(currentUserRole);

  const protectedFn = () => {
    if (!isExcluded) {
      callback();
    } else {
      router.push("/login");
    }
  };

  return protectedFn;
};

export const useRouteGuardExclude = (excludedRoles: ROLE[]) => {
  const router = useRouter();
  const currentUserRole = selectRole();
  const [isRedirecting, setIsRedirecting] = useState(false);

  useEffect(() => {
    const checkAccess = async () => {
      if (!isRedirecting && currentUserRole && excludedRoles.includes(currentUserRole)) {
        setIsRedirecting(true);
        router.push("/login");
      }
    };

    checkAccess();
  }, [currentUserRole]);

  return {
    hasAccess: !currentUserRole || !excludedRoles.includes(currentUserRole)
  };
};
export default useProtected;
