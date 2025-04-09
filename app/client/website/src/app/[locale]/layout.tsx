import type { Metadata } from "next";
import { Roboto } from "next/font/google";
import React from "react";

import "./globals.css";
import "../../styles/prism.css";
import Providers from "@/components/provider/Providers";
import AuthListener from "@/components/shared/auth/AuthListener";
import Protected from "@/components/shared/auth/Protected";
import { Roles } from "@/constants/role";
import { hasLocale, NextIntlClientProvider } from "next-intl";
import { routing } from "@/i18n/routing";
import { notFound } from "next/navigation";

const roboto = Roboto({
  subsets: ["latin"],
  weight: ["100", "300", "400", "500", "700", "900"],
  variable: "--font-inter"
});

export const metadata: Metadata = {
  title: "Tastopia",
  description: "About Tastopia",
  icons: {
    icon: "/assets/favicon.ico"
  },
  other: {
    "og:url": "https://tastopia.social:3000/",
    "og:type": "website",
    "og:title": "Tastopia",
    "og:image":
      "https://res.cloudinary.com/zuong/image/upload/v1743763092/favicon_grrhm0.png",
    "twitter:url": "https://tastopia.social:3000/",
    "twitter:type": "website",
    "twitter:title": "Tastopia",
    "twitter:image":
      "https://res.cloudinary.com/zuong/image/upload/v1743763092/favicon_grrhm0.png"
  }
};

export default async function RootLayout({
  children,
  params
}: Readonly<{
  children: React.ReactNode;
  params: Promise<{ locale: string }>;
}>) {
  const { locale } = await params;
  if (!hasLocale(routing.locales, locale)) {
    notFound();
  }

  return (
    <html lang={locale}>
      <body className={`${roboto.className}`}>
        <NextIntlClientProvider>
          <Providers>
            <AuthListener />
            <Protected allowedRoles={[Roles.SUPER_ADMIN, Roles.ADMIN]}>
              {children}
            </Protected>
          </Providers>
        </NextIntlClientProvider>
      </body>
    </html>
  );
}
