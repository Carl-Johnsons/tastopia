import { ROLE, selectRole } from "@/slices/auth.slice";
import { router } from "expo-router";
import { ReactNode } from "react";
import { Pressable } from "react-native";

type ProtectedProps = {
  children: ReactNode;

  /** Roles permitted to view the content. */
  roles?: ROLE[];

  /** Roles explicitly denied permission to view the content. */
  excludedRoles?: ROLE[];

  /** Forcing the content to display no matter what. */
  forceDisplay?: boolean;

  /**
   * Require login when the current user does not have enough
   * permission to view the content. Currently works with `forceDisplay`.
   */
  requiredLogin?: boolean;
};

export const Protected = ({
  roles,
  excludedRoles,
  children,
  forceDisplay,
  requiredLogin
}: ProtectedProps) => {
  const currentUserRole = selectRole() as ROLE;
  const isGranted = roles?.includes(currentUserRole);
  const isExcluded = excludedRoles?.includes(currentUserRole);
  const hasAccess = (roles && isGranted) || (excludedRoles && !isExcluded);

  const checkLogin = () => {
    !hasAccess && router.push("/login");
  };

  const content = (() => {
    if (requiredLogin) {
      return (
        <Pressable
          onPress={checkLogin}
          onStartShouldSetResponderCapture={() => true}
        >
          {children}
        </Pressable>
      );
    }

    return <>children</>;
  })();

  if (forceDisplay) return content;
  if (hasAccess) return content;

  return null;
};

export default Protected;
