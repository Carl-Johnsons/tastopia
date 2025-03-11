"use server";

import { signIn, signOut } from "@/auth";
import {
  CLIENT_BASE_URL,
  DUENDE_IDENTITY_PROVIDER_NAME,
  DUENDE_IDS6_ISSUER,
} from "@/constants/api";
import {
  deleteAllAuthCookies,
  getAuthCookie,
} from "@/utils/auth";
import { redirect } from "next/navigation";

export const handleSignIn = async () => {
  await signIn(DUENDE_IDENTITY_PROVIDER_NAME);
};

export const handleSignOut = async () => {
  await signOut({ 
    redirect: false
  });

  const idToken = getAuthCookie("idToken");
  const logoutUrl = `${DUENDE_IDS6_ISSUER}/connect/endsession?id_token_hint=${idToken}&post_logout_redirect_uri=${encodeURIComponent(CLIENT_BASE_URL as string)}`;

  deleteAllAuthCookies();
  redirect(logoutUrl);
};
