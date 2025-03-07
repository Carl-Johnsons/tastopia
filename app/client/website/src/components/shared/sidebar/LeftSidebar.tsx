"use client";
import React, { useState, useEffect } from "react";
import Image from "next/image";
import Link from "next/link";
import { usePathname, useRouter } from "next/navigation";
import { sidebarLinks } from "@/constants/nav";

const LeftSidebar = () => {
  const router = useRouter(); // Initialize router
  const pathname = usePathname();
  const [openDropdowns, setOpenDropdowns] = useState<string[]>([]);

  useEffect(() => {
    const initialOpenDropdowns = sidebarLinks
      .filter((link) => link.children && link.children.some((child) => pathname === child.route))
      .map((link) => link.route);

    setOpenDropdowns(initialOpenDropdowns);
  }, [pathname]);

  const toggleDropdown = (route: string) => {
    setOpenDropdowns((prev) => (prev.includes(route) ? prev.filter((item) => item !== route) : [...prev, route]));
  };

  const handleNavigation = (link: any) => {
    if (link.children) {
      toggleDropdown(link.route);
    } else {
      router.push(link.route);
    }
  };

  return (
    <section className="bg-white_black100 light-border custom-scrollbar sticky right-0 top-0 flex h-screen flex-col justify-between overflow-y-auto border-l p-2 pt-28 shadow-lg shadow-gray-300 max-sm:hidden dark:shadow-none">
      <div className="flex flex-col">
        <div className="flex h-full flex-col gap-6">
          {sidebarLinks.map((link) => {
            const isActive = pathname === link.route;
            const isDropdownOpen = openDropdowns.includes(link.route);

            return (
              <div key={link.route} className="flex flex-col">
                <div
                  onClick={() => handleNavigation(link)}
                  className={`flex cursor-pointer items-center rounded-lg p-3 ${
                    isActive && !link.children ? "bg-primary" : "text-black_white"
                  }`}
                >
                  <div className="size-6">
                    <Image src={link.imgURL} alt={link.label} width={20} height={20} className="dark:invert" />
                  </div>
                  <span className="mx-2 flex-1">{link.label}</span>
                  {link.children && (
                    <svg
                      className={`size-5 transition-transform ${isDropdownOpen ? "rotate-180" : ""}`}
                      fill="none"
                      stroke="currentColor"
                      viewBox="0 0 24 24"
                    >
                      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 9l-7 7-7-7" />
                    </svg>
                  )}
                </div>

                {link.children && isDropdownOpen && (
                  <div className="ml-8 flex flex-col">
                    {link.children.map((child) => {
                      const isChildItemActive = pathname === child.route;

                      return (
                        <Link
                          key={child.route}
                          href={child.route}
                          className={`rounded-lg px-4 py-2 ${isChildItemActive ? "bg-primary" : "text-black_white"}`}
                        >
                          {child.label}
                        </Link>
                      );
                    })}
                  </div>
                )}
              </div>
            );
          })}
        </div>
      </div>
    </section>
  );
};

export default LeftSidebar;
