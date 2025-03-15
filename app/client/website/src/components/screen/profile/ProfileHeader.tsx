import { BanIcon, RestoreIcon } from "@/components/shared/icons";
import { UserProfileType } from "@/types/user";
import Image from "next/image";
import Status from "@/components/ui/Status";

type ProfileHeaderProps = {
  user: UserProfileType;
  onRestore?: (userId: string) => Promise<void>;
  isRestoring?: boolean;
};

export default function ProfileHeader({ user }: ProfileHeaderProps) {
  return (
    <div className="bg-white_black100 overflow-hidden rounded-xl border border-gray-200 shadow-sm dark:border-gray-600">
      <div className="flex flex-col items-center justify-between gap-4 p-6 sm:flex-row">
        <div className="flex items-center gap-5">
          <div className="relative size-24 overflow-hidden rounded-full bg-orange-100">
            {user.avatarUrl ? (
              <Image src={user.avatarUrl} alt={user.accountUsername} fill className="object-cover" />
            ) : (
              <div className="flex size-full items-center justify-center bg-orange-200 font-bold text-3xl text-orange-600">
                {user.accountUsername.charAt(0)}
              </div>
            )}
          </div>

          <div>
            <h1 className="h3-semibold text-black_white">{user.accountUsername}</h1>
            <div className="flex items-center gap-2">
              <p className="text-gray-600">User</p>
              <Status isActive={user.isAccountActive }/>
            </div>
          </div>
        </div>

        {user.isAccountActive ? (
           <button className="text-white_black flex items-center gap-2 rounded-lg bg-red-500 px-4 py-2 transition hover:bg-red-600">
            <BanIcon />
            Ban
         </button>
        ) : (
          <button className="text-white_black flex items-center gap-2 rounded-lg bg-green-600 px-4 py-2 transition hover:bg-green-700">
            <RestoreIcon />
            Restore
        </button>
        )}
      </div>
    </div>
  );
}
