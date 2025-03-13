import { ActivityType } from "@/constants/activities";

export interface ActivityItem {
  id?: string;
  type: ActivityType;
  title: string;
  description: string;
  timeAgo: string;
  time: string;

  username: string;
  accountId: string;
  avtImageUrl: string;

  recipeId: string;
  recipeTitle: string;
  recipeAuthorUsername: string;
  recipeAuthorId: string;
  recipeImageUrl: string;
  recipeTimeAgo: string;
  recipeTime: string;
  recipeVoteDiff: number;

  commentId: string | null;
  commentContent: string | null;

  timestamp?: string;
  imageUrl?: string;
  likes?: number;
  userName?: string;
  content?: string;
  recipeOwner?: string;
  recipeLikes?: number;
}

export interface UserSettingType {
  settingId: string;
  accountId: string;
  code: string;
  description: string;
  dataType: "Boolean" | "String" | "Number";
  defaultValue: string;
  value: string;
}

export interface UserProfileType {
  id: string;
  name: string;
  accountUsername: string;
  email: string;
  phone: string;
  gender: string | null;
  dateOfBirth: string | null;
  address: string | null;
  followers: number;
  following: number;
  recipes: number;
  activeTime: string;
  status: "active" | "inactive" | "banned";
  avatarUrl: string;
  isCurrentUser: boolean;
  role: "User" | "Admin" | "Super Admin";
  settings: UserSettingType[];
}
