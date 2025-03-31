import {
  CalendarIcon,
  EmailIcon,
  GenderIcon,
  InfoIcon,
  LocationIcon,
  PhoneIcon,
  UserIcon
} from "@/components/shared/icons";
import { getTranslations } from "next-intl/server";
import { Gender } from "@/constants/gender";
import { ReactNode } from "react";
import { format } from "date-fns";
import { IAdminDetailResponse } from "@/generated/interfaces/user.interface";
import { UpdateAdminButton } from "./Button";
import AdminDialog from "./Dialog";

type Props = {
  admin: IAdminDetailResponse;
  self?: boolean;
  isLoading?: boolean;
};

export enum Icon {
  USER,
  DISPLAY_NAME,
  EMAIL,
  PHONE,
  CALENDAR,
  LOCATION,
  GENDER
}

export type InfoItem = {
  icon: Icon;
  label: string;
  value: string | null;
};

export default async function ProfileInfo({ admin, self }: Props) {
  const t = await getTranslations("administerAdmins.detail.info");
  const tTooltip = await getTranslations("administerAdmins.tooltip");

  const { username, displayName, email, phoneNumber, address, gender, accountId } =
    admin as IAdminDetailResponse;
  const dob = admin?.dob ? format(new Date(admin?.dob as string), "dd/MM/yyyy") : null;

  const infoItems: InfoItem[] = [
    { icon: Icon.USER, label: t("fields.username"), value: username },
    { icon: Icon.DISPLAY_NAME, label: t("fields.displayName"), value: displayName },
    { icon: Icon.EMAIL, label: t("fields.email"), value: email as string },
    { icon: Icon.PHONE, label: t("fields.phoneNumber"), value: phoneNumber as string },
    { icon: Icon.CALENDAR, label: t("fields.dob"), value: dob },
    { icon: Icon.LOCATION, label: t("fields.address"), value: address as string },
    {
      icon: Icon.GENDER,
      label: t("fields.gender"),
      value: gender
        ? Gender.Male
          ? t("fields.genders.male")
          : t("fields.genders.female")
        : null
    }
  ];

  const getIcon = (icon: Icon): ((props: { className?: string }) => ReactNode) => {
    switch (icon) {
      case Icon.USER:
        return UserIcon;
      case Icon.DISPLAY_NAME:
        return InfoIcon;
      case Icon.EMAIL:
        return EmailIcon;
      case Icon.PHONE:
        return PhoneIcon;
      case Icon.CALENDAR:
        return CalendarIcon;
      case Icon.LOCATION:
        return LocationIcon;
      case Icon.GENDER:
        return GenderIcon;
      default:
        throw new Error("Invalid icon type");
    }
  };

  return (
    <>
      <div className='bg-white_black100 rounded-xl border border-gray-200 p-6 shadow-sm dark:border-gray-600'>
        <h2 className='h3-semibold text-black_white mb-6'>{t("title")}</h2>

        <div className='space-y-5'>
          {infoItems.map(({ icon, label, value }, index) => {
            if (!value) return null;

            return (
              <div
                key={index}
                className='flex-center items-start gap-4'
              >
                <div className='flex-center size-8 shrink-0 rounded-full text-orange-500'>
                  {getIcon(icon)({
                    className: "size-4"
                  })}
                </div>

                <div className='flex-1'>
                  <p className='paragraph-regular text-black_white'>{label}</p>
                  <p className='paragraph-regular text-wrap break-all text-gray-500'>
                    {value}
                  </p>
                </div>
              </div>
            );
          })}
          {self && (
            <UpdateAdminButton
              title={tTooltip("update")}
              targetId={accountId}
              className='w-full bg-primary hover:bg-primary/90'
              noText
              toolTip
            />
          )}
        </div>
      </div>
      <AdminDialog hideTriggerButton />
    </>
  );
}
