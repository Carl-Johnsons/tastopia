"use client";

import { useRouter } from "@/i18n/navigation";
import { useSelectUser } from "@/slices/user.slice";
import Avatar from "../common/Avatar";
import { useCallback } from "react";

const Profile = () => {
  const { avatarUrl, displayName } = useSelectUser();
  const router = useRouter();

  const handleClick = useCallback(() => {
    router.push("/admins/me");
  }, [router]);

  return (
    <Avatar
      src={avatarUrl ?? ""}
      alt={displayName ?? ""}
      onClick={handleClick}
    />
  );
};

export default Profile;
