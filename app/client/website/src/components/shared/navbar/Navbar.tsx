import Image from "next/image";
import Link from "next/link";

import Theme from "./Theme";
import MobileNav from "./MobileNav";

const Navbar = () => {
  return (
    <nav className="flex-between background-light900_dark200 fixed z-50 w-full flex-wrap gap-5 p-6 shadow-light-300 dark:shadow-none sm:flex-nowrap sm:px-12">
      <Link href={"/"} className="group flex items-center justify-center gap-2">
        <Image src="/assets/icons/logo.svg" alt="Tastopia" width={36} height={36} className="group-hover:animate-spin" />
        <p className="h2-bold font-spaceGrotesk text-dark-100 dark:text-light-900 flex flex-nowrap gap-1 text-nowrap max-sm:hidden">
          <span>Tastopia</span>
        </p>
      </Link>

      <div className="flex-between gap-5">
        <Theme />
        <MobileNav />
      </div>
    </nav>
  );
};

export default Navbar;
