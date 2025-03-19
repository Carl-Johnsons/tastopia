"use client";

import LoginForm from "@/components/screen/login/LoginForm";
import {
  CLIENT_BASE_URL,
  DUENDE_IDENTITY_PROVIDER_NAME,
  DUENDE_IDS6_ISSUER
} from "@/constants/api";
import { signIn, signOut, useSession } from "next-auth/react";
import { useRouter } from "@/i18n/navigation";
import { useCallback } from "react";

export default function Login() {
  const { data: session } = useSession();
  const idToken = session?.idToken;
  const router = useRouter();

  const logout = useCallback(async () => {
    console.log("Signing out...");

    await signOut({
      redirect: false
    });
    console.log("Signing out from Duende IdentityServer...");

    if (idToken) {
      const logoutUrl = `${DUENDE_IDS6_ISSUER}/connect/endsession?id_token_hint=${idToken}&post_logout_redirect_uri=${encodeURIComponent(
        CLIENT_BASE_URL as string
      )}`;

      console.log("Redirecting to:", logoutUrl);
      router.replace(logoutUrl);
    } else {
      console.error(
        "No ID token found for the user. Unable to log out from Duende IdentityServer."
      );
    }
  }, [idToken]);

  if (session) {
    return (
      <>
        <button
          onClick={() => {
            logout();
          }}
        >
          Sign out
        </button>
      </>
    );
  }
  return (
    <>
      Not signed in <br />{" "}
      <button onClick={() => signIn(DUENDE_IDENTITY_PROVIDER_NAME)}>Sign in</button>
    </>
  );
}
