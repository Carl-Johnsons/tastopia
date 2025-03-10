"use client";

import { CLIENT_BASE_URL, DUENDE_IDS6_ISSUER } from "@/constants/api";
import { useSelectIdToken } from "@/slices/auth.slice";
import { signOut } from "next-auth/react";
import { useRouter } from "next/navigation";
import { useCallback } from "react";

const LogoutButton = () => {
  const idToken = useSelectIdToken();
  const router = useRouter();

  const logout = useCallback(async () => {
    console.log("Signing out...");

    await signOut({
      redirect: false,
    });
    console.log("Signing out from Duende IdentityServer...");

    if (idToken) {
      const logoutUrl = `${DUENDE_IDS6_ISSUER}/connect/endsession?id_token_hint=${idToken}&post_logout_redirect_uri=${encodeURIComponent(
        CLIENT_BASE_URL as string,
      )}`;

      console.log("Redirecting to:", logoutUrl);
      router.replace(logoutUrl);
    } else {
      console.error(
        "No ID token found for the user. Unable to log out from Duende IdentityServer.",
      );
    }
  }, [idToken]);

  return (
    <button onClick={logout} className="flex items-center gap-2">
      <span className="text-black_white">Sign out</span>
    </button>
  );
};

export default LogoutButton;
