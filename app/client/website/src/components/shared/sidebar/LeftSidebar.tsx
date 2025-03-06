"use client";
import React from "react";
import Image from "next/image";
import Link from "next/link";
import { usePathname } from "next/navigation";

import { sidebarLinks } from "@/constants/nav";

const LeftSidebar = () => {
  const pathname = usePathname();
  console.log("pathname", pathname);

  return (
    <section className="background-light900_dark200 light-border custom-scrollbar sticky right-0 top-0 flex h-screen flex-col justify-between overflow-y-auto border-l p-6 pt-36 shadow-light-300 dark:shadow-none max-sm:hidden lg:w-[280px]">
      <div className="flex h-full flex-col gap-6">
        {sidebarLinks.map((link) => {
          const isActive = (pathname === link.route && link.route.length > 1) || pathname === link.route;

          return (
            <Link
              key={link.route}
              href={link.route}
              className={`${isActive ? "text-light-900 bg-primary" : "text-dark300_light900 hover:background-light800_dark400"} flex items-center justify-start gap-4 rounded-lg p-4 transition duration-300`}
            >
              {link.imgURL && (
                <Image src={link.imgURL} alt={link.label} width={20} height={20} className={`${isActive ? "" : "invert-colors"}`} />
              )}
              <p className={`${isActive ? "paragraph-semibold text-white_black" : "paragraph-medium text-black_white"} max-lg:hidden`}>
                {link.label}
              </p>
            </Link>
          );
        })}
      </div>
    </section>
  );
};

export default LeftSidebar;
