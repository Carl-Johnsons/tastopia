import { createNavigation } from "next-intl/navigation";
import { routing } from "./routing";

/** !!! Use these one instead of NextJS !!! */
export const { Link, redirect, usePathname, useRouter, getPathname } =
  createNavigation(routing);
