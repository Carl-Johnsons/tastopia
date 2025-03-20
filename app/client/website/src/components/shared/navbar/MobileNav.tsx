"use client";

import React, { useEffect, useState } from "react";
import Image from "next/image";
import { Link, usePathname, useRouter } from "@/i18n/navigation";
import { sidebarLinks } from "@/constants/nav";
import { Sheet, SheetClose, SheetContent, SheetTrigger } from "@/components/ui/sheet";
import { useTranslations } from "next-intl";

export const NavContent = () => {
  const router = useRouter();
  const pathname = usePathname();
  const t = useTranslations("navbar");
  const [openDropdowns, setOpenDropdowns] = useState<string[]>([]);

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
    <section className='flex h-full flex-col gap-6 pt-16'>
      {sidebarLinks.map(link => {
        const isActive = pathname === link.route || pathname.startsWith(link.route + "/");
        const isDropdownOpen = openDropdowns.includes(link.route);

        return (
          <div
            key={link.route}
            className='flex flex-col'
          >
            {link.children ? (
              <div
                onClick={() => handleNavigation(link)}
                className={`flex cursor-pointer items-center rounded-lg p-3`}
              >
                <div className='mr-2 size-6'>
                  <Image
                    src={link.imgURL}
                    alt={link.label}
                    width={20}
                    height={20}
                    className='dark:invert'
                  />
                </div>
                <span className='text-black_white flex-1'>{t(link.label)}</span>
                {link.children && (
                  <svg
                    className={`size-5 transition-transform dark:invert ${isDropdownOpen ? "rotate-180" : ""}`}
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
            ) : (
              <SheetClose asChild>
                <div
                  onClick={() => handleNavigation(link)}
                  className={`flex cursor-pointer items-center rounded-lg p-3 ${
                    isActive && !link.children ? "bg-primary" : "text-black_white"
                  }`}
                >
                  <div className='mr-2 size-6'>
                    <Image
                      src={link.imgURL}
                      alt={link.label}
                      width={20}
                      height={20}
                      className='dark:invert'
                    />
                  </div>
                  <span className='flex-1'>{t(link.label)}</span>
                  {link.children && (
                    <svg
                      className={`size-5 transition-transform ${isDropdownOpen ? "rotate-180" : ""}`}
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
              </SheetClose>
            )}

            {link.children && isDropdownOpen && (
              <div className='ml-8 flex flex-col'>
                {link.children.map(child => {
                  const isChildItemActive = pathname === child.route;

                  return (
                    <SheetClose
                      asChild
                      key={link.route}
                    >
                      <Link
                        key={child.route}
                        href={child.route}
                        className={`rounded-lg px-4 py-2 ${isChildItemActive ? "bg-primary" : "text-black_white"}`}
                      >
                        {t(child.label)}
                      </Link>
                    </SheetClose>
                  );
                })}
              </div>
            )}
          </div>
        );
      })}
    </section>
  );
};

const MobileNav = () => {
  return (
    <Sheet>
      <SheetTrigger asChild>
        <Image
          src='/assets/icons/menu-2.svg'
          alt='Menu'
          width={36}
          height={36}
          className='dark:invert md:hidden'
        />
      </SheetTrigger>
      <SheetContent
        side={"left"}
        className='bg-white_black100 flex flex-col border-none'
      >
        <Link
          href={"/"}
          className='flex items-center gap-2'
        >
          <Image
            src='/assets/images/logo.png'
            alt='Tastopia Icon'
            width={23}
            height={23}
          ></Image>
          <p className='h2-bold text-black_white'>Tastopia</p>
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
