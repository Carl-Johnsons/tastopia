"use server";

import { auth, signOut } from "@/auth";
import { CLIENT_BASE_URL, DUENDE_IDS6_ISSUER } from "@/constants/api";
import { deleteAllAuthCookies } from "@/utils/auth";
import { redirect } from "next/navigation";

export const handleSignOut = async () => {
  const session = await auth();
  const idToken = session?.idToken;
  const logoutUrl = `${DUENDE_IDS6_ISSUER}/connect/endsession?id_token_hint=${idToken}&post_logout_redirect_uri=${encodeURIComponent(CLIENT_BASE_URL as string)}`;

  await signOut({ 
    redirect: false
  });

  await deleteAllAuthCookies();
  redirect(logoutUrl);
};
