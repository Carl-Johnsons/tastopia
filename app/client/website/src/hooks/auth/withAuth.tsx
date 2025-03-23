import { useRouter } from "@/i18n/navigation";
import { Roles } from "@/constants/role";
import { useEffect, ComponentType } from "react";
import { useSelectRole } from "@/slices/auth.slice";

const withAuth = <P extends object>(
  WrappedComponent: ComponentType<P>,
  allowedRoles: Roles[]
) => {
  const ProtectedComponent = (props: P) => {
    const role = useSelectRole();

    const router = useRouter();

    useEffect(() => {
      if (!role || !allowedRoles.includes(role)) {
        router.replace("/unauthorized");
      }
    }, [role, router]);

    return role && allowedRoles.includes(role) ? <WrappedComponent {...props} /> : null;
  };

  return ProtectedComponent;
};

export default withAuth;
