import type { Metadata } from "next";
import { IBM_Plex_Mono } from "next/font/google";
import React from "react";

import "./globals.css";
import "../styles/prism.css";
import "animate.css";
import Providers from "@/components/provider/Providers";
import AuthListener from "@/components/shared/auth/AuthListener";
import Protected from "@/components/shared/auth/Protected";
import { Roles } from "@/constants/role";

const imbPlexMono = IBM_Plex_Mono({
  weight: ["400", "500", "600", "700"],
  style: ["normal", "italic"],
  subsets: ["latin", "vietnamese"],
  display: "swap",
});

export const metadata: Metadata = {
  title: "Tastopia",
  description: "About Tastopia",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body className={`${imbPlexMono.className}`}>
        <Providers>
          <AuthListener />
          <Protected allowedRoles={[Roles.SUPER_ADMIN, Roles.ADMIN]}>{children}</Protected>
        </Providers>
      </body>
    </html>
  );
}
