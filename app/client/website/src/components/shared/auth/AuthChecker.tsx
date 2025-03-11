"use client";

import { clientAxiosInstance } from "@/constants/clientHost";
import { ReactNode, useEffect, useState } from "react";

export default function AuthChecker({ children }: { children: ReactNode }) {
  const [authenticated, setAuthenticated] = useState(false);

  console.log("AuthChecker: Authenticated", authenticated);

  useEffect(() => {
    const interval = setInterval(async () => {
      const { status } = await clientAxiosInstance.get("/api/auth/cookie");

      if (status === 204) {
        clearInterval(interval);
        console.log("Authenticated");
        setAuthenticated(true);
      }
    }, 1000);

    console.log("Not authenticated");
    return () => clearInterval(interval);
  }, []);

  if (!authenticated) {
    return <div>Checking...</div>;
  }

  return (
    <div>
      <div>
        Session Token: {authenticated ? "Authenticated" : "Checking..."}
      </div>
      {children}
    </div>
  );
}
