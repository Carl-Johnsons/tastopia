"use client";

import { DUENDE_IDENTITY_PROVIDER_NAME } from "@/constants/api";
import { signIn } from "next-auth/react";

export default function LoginForm() {
  const handleSignIn = () => {
    console.log("SignIn to", DUENDE_IDENTITY_PROVIDER_NAME);
    signIn(DUENDE_IDENTITY_PROVIDER_NAME);
  };

  return (
    <div className='rounded-lg border border-gray-200 p-1 hover:bg-gray-200'>
      <button onClick={handleSignIn}>
        Sign in with <span className='text-primary'>Tastopia account</span>
      </button>
    </div>
  );
}
