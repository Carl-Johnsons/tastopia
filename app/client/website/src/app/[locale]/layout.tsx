import type { Metadata } from "next";
import localFont from "next/font/local";
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

const sofiaPro = localFont({
  src: [
    {
      path: "../../../public/fonts/Sofia-Pro-Black-Az.otf",
      weight: "900",
      style: "normal"
    },
    {
      path: "../../../public/fonts/Sofia-Pro-Black-Italic-Az.otf",
      weight: "900",
      style: "italic"
    },
    {
      path: "../../../public/fonts/Sofia-Pro-Bold-Az.otf",
      weight: "700",
      style: "normal"
    },
    {
      path: "../../../public/fonts/Sofia-Pro-Bold-Italic-Az.otf",
      weight: "700",
      style: "italic"
    },
    {
      path: "../../../public/fonts/Sofia-Pro-ExtraLight-Az.otf",
      weight: "200",
      style: "normal"
    },
    {
      path: "../../../public/fonts/Sofia-Pro-ExtraLight-Italic-Az.otf",
      weight: "200",
      style: "italic"
    },
    {
      path: "../../../public/fonts/Sofia-Pro-Light-Az.otf",
      weight: "300",
      style: "normal"
    },
    {
      path: "../../../public/fonts/Sofia-Pro-Light-Italic-Az.otf",
      weight: "300",
      style: "italic"
    },
    {
      path: "../../../public/fonts/Sofia-Pro-Medium-Az.otf",
      weight: "500",
      style: "normal"
    },
    {
      path: "../../../public/fonts/Sofia-Pro-Medium-Italic-Az.otf",
      weight: "500",
      style: "italic"
    },
    {
      path: "../../../public/fonts/Sofia-Pro-Regular-Az.otf",
      weight: "400",
      style: "normal"
    },
    {
      path: "../../../public/fonts/Sofia-Pro-Regular-Italic-Az.otf",
      weight: "400",
      style: "italic"
    },
    {
      path: "../../../public/fonts/Sofia-Pro-Semi-Bold-Az.otf",
      weight: "600",
      style: "normal"
    },
    {
      path: "../../../public/fonts/Sofia-Pro-Semi-Bold-Italic-Az.otf",
      weight: "600",
      style: "italic"
    },
    {
      path: "../../../public/fonts/Sofia-Pro-UltraLight-Az.otf",
      weight: "100",
      style: "normal"
    },
    {
      path: "../../../public/fonts/Sofia-Pro-UltraLight-Italic-Az.otf",
      weight: "100",
      style: "italic"
    }
  ],
  variable: "--font-sofia",
  display: "swap"
});

export const metadata: Metadata = {
  title: "Tastopia",
  description: "About Tastopia"
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
      <body className={`${sofiaPro.className}`}>
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
