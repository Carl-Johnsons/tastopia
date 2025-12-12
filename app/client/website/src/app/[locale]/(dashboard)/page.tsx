import { auth } from "@/auth";
import { Roles } from "@/constants/role";
import { redirect } from "@/i18n/navigation";
import { jwtDecode, JwtPayload } from "jwt-decode";
import { getLocale } from "next-intl/server";

type CustomJwtPayload = JwtPayload & {
  role: Roles;
}

export default async function AdminPage() {
  const session = await auth();
  const decodedToken = session ? jwtDecode(session.accessToken) as CustomJwtPayload : null;
  const role = decodedToken ? decodedToken.role : null;
  const locale = await getLocale();
  const isSuperAdmin = role === Roles.SUPER_ADMIN;

  if (isSuperAdmin) {
    return redirect({
      href: {
        pathname: "/statistics"
      },
      locale
    });
  }

  redirect({
    href: {
      pathname: "/recipes"
    },
    locale
  });

  return null;
}
