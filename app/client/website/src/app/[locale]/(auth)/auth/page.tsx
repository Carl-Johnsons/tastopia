"use client";

import { useSession } from "next-auth/react";
import { handleSignIn, handleSignOut } from "@/actions/auth";

export default function Login() {
  const { data: session } = useSession();

  if (session) {
    return (
      <button
        onClick={() => {
          handleSignOut();
        }}
      >
        Sign out
      </button>
    );
  }
  return (
    <>
      Not signed in <br />{" "}
      <button
        onClick={() => {
          handleSignIn();
        }}
      >
        Sign in
      </button>
    </>
  );
}
