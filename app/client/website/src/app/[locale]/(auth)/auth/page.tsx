"use client";

import { useSession } from "next-auth/react";
import { handleSignOut } from "@/actions/auth";
import LoginForm from "@/components/screen/login/LoginForm";
import { useEffect, useState } from "react";
import Loader from "@/components/ui/Loader";

export default function Login() {
  const { data: session } = useSession();
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    if (session) {
      setIsLoading(false);
    }
  }, [session]);

  if (isLoading) {
    return (
      <div className='flex-center h-screen'>
        <Loader />
      </div>
    );
  }

  return (
    <div className='flex-center h-screen'>
      {session ? (
        <button
          className='rounded-lg border border-gray-200 p-1 hover:bg-gray-200'
          onClick={() => {
            handleSignOut();
          }}
        >
          Sign out of <span className='text-primary'>Tastopia</span>
        </button>
      ) : (
        <LoginForm />
      )}
    </div>
  );
}
