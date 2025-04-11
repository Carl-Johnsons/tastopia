"use client";

import Image from "next/image";
import { Link } from "@/i18n/navigation";

import Theme from "./Theme";
import MobileNav from "./MobileNav";
import Language from "./Language";
import Profile from "./Profile";

const Navbar = () => {
  return (
    <nav className='flex-between bg-white_black100 fixed z-50 w-full flex-wrap gap-5 p-6 shadow-lg shadow-gray-100 dark:shadow-none sm:flex-nowrap sm:px-12'>
      <Link
        href={"/"}
        className='group flex items-center justify-center gap-2'
      >
        <Image
          src='/assets/icons/logo.svg'
          alt='Tastopia'
          width={36}
          height={36}
        />
        <p className='h2-bold font-spaceGrotesk text-dark-100 dark:text-light-900 flex flex-nowrap gap-1 text-nowrap max-sm:hidden'>
          <span className='text-black_white'>Tastopia</span>
        </p>
      </Link>

      <div className='flex-between gap-1'>
        <Theme />
        <Language />
        <div className='pl-2 pr-4'>
          <Profile />
        </div>
        <MobileNav />
      </div>
    </nav>
  );
};

export default Navbar;
