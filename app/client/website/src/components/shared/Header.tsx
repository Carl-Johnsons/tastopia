"use client";

import { navLinks } from "@/constants/nav";
import { useTheme } from "@/context/ThemeProvider";
import Image from "next/image";
import Link from "next/link";
import { usePathname } from "next/navigation";
import React from "react";

const Header = () => {
    const pathname = usePathname();
    const { mode, setMode } = useTheme();

    const handleChangeMode = () => {
        setMode(mode === "light" ? "dark" : "light");
        localStorage.theme = mode === "light" ? "dark" : "light";
    };
    return (
        <div>
            <Image
                src={"/assets/dynamic/sakura-pixel.gif"}
                alt="Background image"
                width={"600"}
                height={"100"}
                className="sm:rounded-md"
                unoptimized
            />

            <div className="flex-center my-4 gap-10">
                {/* {navLinks.map((link, index) => {
                    const isActive = (pathname.includes(link.route) && link.route.length > 1) || pathname === link.route;

                    return (
                        <Link
                            key={link.route}
                            href={link.route}
                            className={`${isActive ? "bg-primary-300" : "text-dark300_light900 hover:background-light800_dark400"} animate__animated animate__fadeIn rounded-md px-5 py-1.5`}
                        >
                            <p className={"h3-medium"}>{link.label}</p>
                        </Link>
                    );
                })}

                <button onClick={handleChangeMode}>
                    <Image src={"/assets/images/logo.png"} alt="toggle dark mode" width={"20"} height={"20"} className="sm:rounded-md" />
                </button> */}
            </div>

            <div className="background-dark100_light900 mb-[16px] h-px w-full rounded-md"></div>
        </div>
    );
};

export default Header;
