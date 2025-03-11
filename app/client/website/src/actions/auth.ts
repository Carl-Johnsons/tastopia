"use server";

import { signIn, signOut } from "@/auth";
import { DUENDE_IDENTITY_PROVIDER_NAME } from "@/constants/api";

export const handleSignIn = async () => {
  await signIn(DUENDE_IDENTITY_PROVIDER_NAME);
};

export const handleSignOut = async () => {
  await signOut();

  // const idToken = getAuthCookie("idToken");
  // const logoutUrl = `${DUENDE_IDS6_ISSUER}/connect/endsession?id_token_hint=${idToken}&post_logout_redirect_uri=${encodeURIComponent(CLIENT_BASE_URL as string)}`;

  // deleteAuthCookie("accessToken");
  // deleteAuthCookie("idToken");
  //
  // redirect(logoutUrl);
};
