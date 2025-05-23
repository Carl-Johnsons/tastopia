"use client";
import React, { useState, useEffect, useMemo } from "react";
import Image from "next/image";
import { Link, usePathname, useRouter } from "@/i18n/navigation";
import { sidebarLinks, superAdminSidebarLinks } from "@/constants/nav";
import { useTranslations } from "next-intl";
import { useSelectRole } from "@/slices/auth.slice";
import { Roles } from "@/constants/role";

const LeftSidebar = () => {
  const router = useRouter();
  const pathname = usePathname();
  const t = useTranslations("navbar");
  const [openDropdowns, setOpenDropdowns] = useState<string[]>([]);
  const role = useSelectRole();
  const isSuperAdmin = useMemo(() => role === Roles.SUPER_ADMIN, [role]);
  const links = useMemo(
    () => (isSuperAdmin ? superAdminSidebarLinks : sidebarLinks),
    [isSuperAdmin]
  );

  useEffect(() => {
    const initialOpenDropdowns = sidebarLinks
      .filter(
        link => link.children && link.children.some(child => pathname === child.route)
      )
      .map(link => link.route);

    setOpenDropdowns(initialOpenDropdowns);
  }, [pathname]);

  const toggleDropdown = (route: string) => {
    setOpenDropdowns(prev =>
      prev.includes(route) ? prev.filter(item => item !== route) : [...prev, route]
    );
  };

  const handleNavigation = (link: any) => {
    if (link.children) {
      toggleDropdown(link.route);
    } else {
      router.push(link.route);
    }
  };

  return (
    <section className='bg-white_black100 custom-scrollbar sticky right-0 top-0 flex h-screen min-w-[276px] flex-col justify-between overflow-y-auto p-2 pt-28 shadow-lg shadow-gray-300 dark:shadow-none max-md:hidden'>
      <div className='flex flex-col'>
        <div className='flex h-full flex-col gap-6'>
          {links.map(link => {
            const isActive =
              pathname === link.route || pathname.startsWith(link.route + "/");
            const isDropdownOpen = openDropdowns.includes(link.route);

            return (
              <div
                key={link.route}
                className='flex flex-col'
              >
                <div
                  onClick={() => handleNavigation(link)}
                  className={`flex cursor-pointer items-center rounded-lg p-3 ${
                    isActive && !link.children ? "bg-primary" : ""
                  }`}
                >
                  <div className='size-6'>
                    <Image
                      src={link.imgURL}
                      alt={link.label}
                      width={20}
                      height={20}
                      className={`${
                        isActive && !link.children
                          ? "invert dark:invert-0"
                          : "dark:invert"
                      }`}
                    />
                  </div>
                  <span
                    className={`mx-2 flex-1 ${
                      isActive && !link.children ? "text-white_black" : "text-black_white"
                    }`}
                  >
                    {t(link.label)}
                  </span>
                  {link.children && (
                    <svg
                      className={`text-black_white size-5 transition-transform ${isDropdownOpen ? "rotate-180" : ""}`}
                      fill='none'
                      stroke='currentColor'
                      viewBox='0 0 24 24'
                    >
                      <path
                        strokeLinecap='round'
                        strokeLinejoin='round'
                        strokeWidth={2}
                        d='M19 9l-7 7-7-7'
                      />
                    </svg>
                  )}
                </div>

                {link.children && isDropdownOpen && (
                  <div className='ml-8 flex flex-col'>
                    {link.children.map(child => {
                      const isChildItemActive = pathname === child.route;

                      return (
                        <Link
                          key={child.route}
                          href={child.route}
                          className={`rounded-lg px-4 py-2 text-sm ${isChildItemActive ? "bg-primary font-semibold" : "text-black_white"}`}
                        >
                          {t(child.label)}
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
