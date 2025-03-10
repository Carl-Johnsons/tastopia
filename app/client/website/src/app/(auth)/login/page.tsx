"use client";

import LoginForm from "@/components/screen/login/LoginForm";
import {
  CLIENT_BASE_URL,
  DUENDE_IDENTITY_PROVIDER_NAME,
  DUENDE_IDS6_ISSUER,
} from "@/constants/api";
import { signIn, signOut, useSession } from "next-auth/react";
import { useRouter } from "next/navigation";
import { useCallback } from "react";

export default function Login() {
  const { data: session } = useSession();
  const idToken = session?.idToken;
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
      <button onClick={() => signIn(DUENDE_IDENTITY_PROVIDER_NAME)}>
        Sign in
      </button>
    </>
  );

  return (
    <div className="grid grid-cols-2 h-screen bg-gray-100">
      <div className="bg-black">
        {/* <Image */}
        {/* src="/assets/images/auth-figure.png" */}
        {/* alt="Banner figure" */}
        {/* quality={100} */}
        {/* fill */}
        {/* /> */}
      </div>
      <div className="flex flex-center">
        <div className="border bg-white">
          <Header />
          <LoginForm />
        </div>
      </div>
    </div>
  );
}

const Header = () => {
  return (
    <>
      <h1>
        Welcome to
        <br />
        <span>Tastopia dashboard</span>
      </h1>
    </>
  );
};
