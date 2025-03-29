import { auth } from "@/auth";
import { Roles } from "@/constants/role";
import { redirect } from "@/i18n/navigation";
import { jwtDecode } from "jwt-decode";
import { getLocale } from "next-intl/server";

export default async function AdminPage() {
  const session = await auth();
  const decodedToken = jwtDecode(session?.accessToken as string) as any;
  const role = decodedToken.role as Roles;
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
