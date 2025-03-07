import { BanIcon, RestoreIcon } from "@/components/shared/icons";
import { UserProfileType } from "@/types/user";
import Image from "next/image";

type ProfileHeaderProps = {
  user: UserProfileType;
  onRestore?: (userId: string) => Promise<void>;
  isRestoring?: boolean;
};

export default function ProfileHeader({ user }: ProfileHeaderProps) {
  return (
    <div className="bg-white_black100 overflow-hidden rounded-xl border border-blue-100 shadow-sm dark:border-gray-600">
      <div className="flex flex-col items-center justify-between gap-4 p-6 sm:flex-row">
        <div className="flex items-center gap-5">
          <div className="relative size-24 overflow-hidden rounded-full bg-orange-100">
            {user.avatarUrl ? (
              <Image src={"/assets/images/panda.png"} alt={user.name} fill className="object-cover" />
            ) : (
              <div className="flex size-full items-center justify-center bg-orange-200 text-3xl font-bold text-orange-600">
                {user.name.charAt(0)}
              </div>
            )}
          </div>

          <div>
            <h1 className="h3-semibold text-black_white">{user.name}</h1>
            <div className="flex items-center gap-2">
              <p className="text-gray-600">User</p>
              {user.status === "inactive" ? (
                <span className="flex items-center gap-1 text-sm text-red-500">
                  <span className="size-2 rounded-full bg-red-500"></span>
                  Inactive
                </span>
              ) : (
                <span className="flex items-center gap-1 text-sm text-green-500">
                  <span className="size-2 rounded-full bg-green-500"></span>
                  Active
                </span>
              )}
            </div>
          </div>
        </div>

        {user.status === "inactive" ? (
          <button className="text-white_black flex items-center gap-2 rounded-lg bg-green-600 px-4 py-2 transition hover:bg-green-700">
            <RestoreIcon />
            Restore
          </button>
        ) : (
          <button className="text-white_black flex items-center gap-2 rounded-lg bg-red-500 px-4 py-2 transition hover:bg-red-600">
            <BanIcon />
            Ban
          </button>
        )}
      </div>
    </div>
  );
}
