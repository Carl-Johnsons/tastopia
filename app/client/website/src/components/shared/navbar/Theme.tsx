"use client";
import React from "react";
import Image from "next/image";
import { Menubar, MenubarContent, MenubarItem, MenubarMenu, MenubarTrigger } from "@/components/ui/menubar";
import { useTheme } from "@/context/ThemeProvider";
import { themes } from "@/constants/theme";

const Theme = () => {
  const { mode, setMode } = useTheme();

  return (
    <Menubar className="relative border-none bg-transparent shadow-none">
      <MenubarMenu>
        <MenubarTrigger className="cursor-pointer focus:bg-white data-[state=open]:bg-white dark:focus:bg-black-200 dark:data-[state=open]:bg-black-200">
          {mode === "light" ? (
            <Image src={"/assets/icons/sun.svg"} alt="sun" width={20} height={20} className="active-theme min-w-[20px]" />
          ) : (
            <Image src={"/assets/icons/moon.svg"} alt="moon" width={20} height={20} className="active-theme min-w-[20px]" />
          )}
        </MenubarTrigger>
        <MenubarContent className="absolute -right-12 mt-3 min-w-[120px] rounded border bg-white py-2 dark:border-black-400 dark:bg-black-300">
          {themes.map((theme) => {
            return (
              <MenubarItem
                key={theme.value}
                onClick={() => {
                  setMode(theme.value);

                  if (theme.value !== "system") {
                    localStorage.theme = theme.value;
                  } else {
                    localStorage.removeItem("theme");
                  }
                }}
                className="flex cursor-pointer items-center gap-4  px-2.5 py-2 hover:bg-white-800 dark:focus:bg-black-400"
              >
                <Image src={theme.icon} alt={theme.label} width={16} height={16} className={`${mode === theme.value && "active-theme"}`} />
                <p className={`body-semibold ${mode === theme.value ? "text-primary" : "text-black_white"}`}>
                  {theme.label}
                </p>
              </MenubarItem>
            );
          })}
        </MenubarContent>
      </MenubarMenu>
    </Menubar>
  );
};

export default Theme;
