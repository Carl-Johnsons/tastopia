"use client";

import { useRouter } from "@/i18n/navigation";
import { useSelectUser } from "@/slices/user.slice";
import Avatar from "../common/Avatar";
import { useCallback } from "react";

const Profile = () => {
  const { avatarUrl, displayName, accountId } = useSelectUser();
  const router = useRouter();

  const handleClick = useCallback(() => {
    router.push("/admins/" + accountId);
  }, [accountId, router]);

  return (
    <Avatar
      src={avatarUrl ?? ""}
      alt={displayName ?? ""}
      onClick={handleClick}
    />
  );
};

export default Profile;
