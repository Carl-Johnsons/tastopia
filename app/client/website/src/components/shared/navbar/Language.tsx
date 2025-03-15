"use client";
import React from "react";
import Image from "next/image";
import { Menubar, MenubarContent, MenubarItem, MenubarMenu, MenubarTrigger } from "@/components/ui/menubar";
import { languages, Language as LanguageEnum } from "@/constants/language";
import { useTranslation } from "react-i18next";
import i18next from "@/i18n/i18next";

const Language = () => {
  const currentLanguage = i18next.languages[0]
  const {t} = useTranslation("navbar")
  const handleChangeLanguage = (language: string) => {
    i18next.changeLanguage(language);
  };

  return (
    <Menubar className="relative border-none bg-transparent shadow-none">
      <MenubarMenu>
        <MenubarTrigger className="cursor-pointer focus:bg-white data-[state=open]:bg-white dark:focus:bg-black-200 dark:data-[state=open]:bg-black-200">
          {currentLanguage === LanguageEnum.VIETNAMESE ? (
            <Image src={"/assets/icons/vietnam_flag.svg"} alt="Vietnam flag" width={20} height={20} className="min-w-[20px]" />
          ) : (
            <Image src={"/assets/icons/united_kingdom_flag.svg"} alt="English flag" width={20} height={20} className="min-w-[20px]" />
          )}
        </MenubarTrigger>
 <MenubarContent className="absolute -right-12 mt-3 min-w-[150px] rounded border bg-white py-2 dark:border-black-400 dark:bg-black-300">
          {languages.map((language) => {
            return (
              <MenubarItem
                key={language.code}
                onClick={() => {
                  handleChangeLanguage(language.code)
                  localStorage.language = language.code;
                }}
   className="flex cursor-pointer items-center gap-4  px-2.5 py-2 hover:bg-white-800 dark:focus:bg-black-400"
              >
                <Image src={language.icon} alt={language.code} width={16} height={16} />
                <p className={`body-semibold ${currentLanguage === language.code ? "text-primary" : "text-black_white"}`}>
                  {t(language.code)}
                </p>
              </MenubarItem>
            );
          })}
        </MenubarContent>
      </MenubarMenu>
    </Menubar>
  );
};

export default Language;
