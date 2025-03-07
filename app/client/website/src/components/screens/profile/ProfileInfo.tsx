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
  TimeIcon,
  UserIcon,
} from "@/components/shared/icons";

export default function ProfileInfo({ user }: any) {
  const infoItems = [
    { icon: "user", label: "USERNAME", value: user.username },
    { icon: "email", label: "EMAIL", value: user.email },
    { icon: "phone", label: "PHONE", value: user.phone },
    { icon: "gender", label: "GENDER", value: user.gender },
    { icon: "calendar", label: "DATE OF BIRTH", value: user.dateOfBirth },
    { icon: "location", label: "ADDRESS", value: user.address },
    { icon: "followers", label: "FOLLOWERS", value: user.followers },
    { icon: "following", label: "FOLLOWINGS", value: user.following },
    { icon: "recipes", label: "RECIPES", value: user.recipes },
    { icon: "time", label: "ACTIVE TIME", value: user.activeTime },
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
      case "calendar":
        return <CalendarIcon />;
      case "location":
        return <LocationIcon />;
      case "followers":
        return <FollowersIcon />;
      case "following":
        return <FollowingIcon />;
      case "recipes":
        return <RecipesIcon />;
      case "time":
        return <TimeIcon />;
      default:
        return <InfoIcon />;
    }
  };

  return (
    <div className="bg-white_black100 rounded-xl border border-gray-200 p-6 shadow-sm dark:border-gray-600">
      <h2 className="h3-semibold text-black_white mb-6">Info</h2>

      <div className="space-y-5">
        {infoItems.map((item, index) => (
          <div key={index} className="flex items-start gap-4">
            <div className="flex size-8 shrink-0 items-center justify-center rounded-full text-orange-500">{getIcon(item.icon)}</div>

            <div className="flex-1">
              <p className="paragraph-regular text-black_white">{item.label}</p>
              <p className="paragraph-regular text-gray-500">{item.value}</p>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
