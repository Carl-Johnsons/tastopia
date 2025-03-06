"use client";

import React from "react";
import Image from "next/image";
import Link from "next/link";
import { usePathname } from "next/navigation";
import { sidebarLinks } from "@/constants/nav";
import { Sheet, SheetClose, SheetContent, SheetTrigger } from "@/components/ui/sheet";

export const NavContent = () => {
  const pathname = usePathname();
  return (
    <section className="flex h-full flex-col gap-6 pt-16">
      {sidebarLinks.map((link) => {
        const isActive = (pathname === link.route && link.route.length > 1) || pathname === link.route;

        return (
          <SheetClose asChild key={link.route}>
            <Link
              href={link.route}
              className={`${isActive ? "bg-primary-500 text-light-900" : "text-dark300_light900 hover:background-light800_dark400"} flex items-center justify-start gap-4 rounded-lg p-4 transition duration-300 hover:-translate-y-1`}
            >
              {link.imgURL && (
                <Image src={link.imgURL} alt={link.label} width={20} height={20} className={`${isActive ? "" : "invert-colors"}`} />
              )}
              <p className={`${isActive ? "base-bold" : "base-medium"}`}>{link.label}</p>
            </Link>
          </SheetClose>
        );
      })}
    </section>
  );
};

const MobileNav = () => {
  return (
    <Sheet>
      <SheetTrigger asChild>
        <Image src="/assets/icons/hamburger.svg" alt="Menu" width={36} height={36} className="sm:hidden" />
      </SheetTrigger>
      <SheetContent side={"left"} className="background-light900_dark200 flex flex-col border-none">
        <Link href={"/"} className="flex items-center gap-2">
          <Image src="/assets/images/logo.png" alt="Tastopia Icon" width={23} height={23}></Image>
          <p className="h2-bold text-dark100_light900 font-spaceGrotesk">Tastopia</p>
        </Link>

        <div>
          <SheetClose asChild>
            <NavContent />
          </SheetClose>
        </div>
      </SheetContent>
    </Sheet>
  );
};

export default MobileNav;
