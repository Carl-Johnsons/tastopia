"use client";

import React, { useTransition } from "react";
import Image from "next/image";
import { useLocale, useTranslations } from "next-intl";
import {
  Menubar,
  MenubarContent,
  MenubarItem,
  MenubarMenu,
  MenubarTrigger
} from "@/components/ui/menubar";
import { languages, Language as LanguageEnum } from "@/constants/language";
import { usePathname } from "@/i18n/navigation";
import { useRouter } from "next/navigation";

const supportedLanguages = Object.values(LanguageEnum);

const Language = () => {
  const router = useRouter();
  const pathname = usePathname();
  const t = useTranslations("navbar");
  const currentLanguage = useLocale();
  const [isPending, startTransition] = useTransition();

  const handleChangeLanguage = (newLanguage: string) => {
    if (newLanguage === currentLanguage) return;

    const segments = pathname.split("/").filter(Boolean);

    if (supportedLanguages.includes(segments[0] as LanguageEnum)) {
      segments[0] = newLanguage;
    } else {
      segments.unshift(newLanguage);
    }

    startTransition(() => {
      localStorage.setItem("language", newLanguage);
      router.replace("/" + segments.join("/"));
    });
  };

  return (
    <Menubar className='relative border-none bg-transparent shadow-none'>
      <MenubarMenu>
        <MenubarTrigger className='cursor-pointer focus:bg-white data-[state=open]:bg-white dark:focus:bg-black-200 dark:data-[state=open]:bg-black-200'>
          <Image
            src={
              currentLanguage === LanguageEnum.VIETNAMESE
                ? "/assets/icons/vietnam_flag.svg"
                : "/assets/icons/united_kingdom_flag.svg"
            }
            alt='Flag'
            width={20}
            height={20}
            className='min-w-[20px]'
          />
        </MenubarTrigger>
        <MenubarContent className='absolute -right-12 mt-3 min-w-[150px] rounded border bg-white py-2 dark:border-black-400 dark:bg-black-300'>
          {languages.map(language => (
            <MenubarItem
              key={language.code}
              onClick={() => handleChangeLanguage(language.code)}
              className='flex cursor-pointer items-center gap-4 px-2.5 py-2 hover:bg-white-800 dark:focus:bg-black-400'
            >
              <Image
                src={language.icon}
                alt={language.code}
                width={16}
                height={16}
              />
              <p
                className={`body-semibold ${
                  currentLanguage === language.code ? "text-primary" : "text-black_white"
                }`}
              >
                {t(language.code)}
              </p>
            </MenubarItem>
          ))}
        </MenubarContent>
      </MenubarMenu>
    </Menubar>
  );
};

export default Language;
