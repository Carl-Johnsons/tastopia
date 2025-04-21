import {
  CalendarIcon,
  EmailIcon,
  FollowersIcon,
  FollowingIcon,
  GenderIcon,
  InfoIcon,
  LocationIcon,
  PhoneIcon,
  RecipesIcon,
  UserIcon
} from "@/components/shared/icons";
import { getTranslations } from "next-intl/server";

export default async function ProfileInfo({ user }: any) {
  const t = await getTranslations("userDetail.info");

  const infoItems = [
    { icon: "user", label: t("fields.username"), value: user.accountUsername },
    { icon: "email", label: t("fields.gmail"), value: user.accountEmail },
    { icon: "phone", label: t("fields.phoneNumber"), value: user.accountPhoneNumber },
    { icon: "gender", label: t("fields.gender"), value: user.gender },
    { icon: "location", label: t("fields.address"), value: user.address },
    { icon: "bio", label: t("fields.bio"), value: user.bio },
    { icon: "followers", label: t("fields.followers"), value: user.totalFollower ?? 0 },
    { icon: "following", label: t("fields.following"), value: user.totalFollowing ?? 0 },
    { icon: "recipes", label: t("fields.recipes"), value: user.totalRecipe ?? 0 }
  ];

  const getIcon = (type: string) => {
    switch (type) {
      case "user":
        return <UserIcon />;
      case "email":
        return <EmailIcon />;
      case "phone":
        return <PhoneIcon />;
      case "gender":
        return <GenderIcon />;
      case "location":
        return <LocationIcon />;
      case "followers":
        return <FollowersIcon />;
      case "following":
        return <FollowingIcon />;
      case "recipes":
        return <RecipesIcon />;
      default:
        return <InfoIcon />;
    }
  };

  return (
    <div className='bg-white_black100 rounded-xl border border-gray-200 p-6 shadow-sm dark:border-gray-600'>
      <h2 className='h3-semibold text-black_white mb-6'>{t("title")}</h2>

      <div className='space-y-5'>
        {infoItems.map((item, index) => (
          <div
            key={index}
            className='flex items-start gap-4'
          >
            <div className='flex size-8 shrink-0 items-center justify-center rounded-full text-orange-500'>
              {getIcon(item.icon)}
            </div>

            <div className='flex-1'>
              <p className='paragraph-regular text-black_white'>{item.label}</p>
              <p className='paragraph-regular text-gray-500'>{item.value}</p>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
