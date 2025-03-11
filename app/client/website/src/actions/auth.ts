"use server";

import { auth, signIn, signOut } from "@/auth";
import {
  CLIENT_BASE_URL,
  DUENDE_IDENTITY_PROVIDER_NAME,
  DUENDE_IDS6_ISSUER,
} from "@/constants/api";
import {
  deleteAllAuthCookies,
} from "@/utils/auth";
import { redirect } from "next/navigation";

export const handleSignIn = async () => {
  await signIn(DUENDE_IDENTITY_PROVIDER_NAME);
};

export const handleSignOut = async () => {
  const session = await auth();
  const idToken = session?.idToken;
  const logoutUrl = `${DUENDE_IDS6_ISSUER}/connect/endsession?id_token_hint=${idToken}&post_logout_redirect_uri=${encodeURIComponent(CLIENT_BASE_URL as string)}`;

  await signOut({ 
    redirect: false
  });

  deleteAllAuthCookies();
  redirect(logoutUrl);
};
