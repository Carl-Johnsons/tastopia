export interface ActivityItem {
  id: string;
  type: "recipe" | "ban" | "comment" | "login" | "rating";
  title: string;
  description: string;
  timestamp: string;
  time: string;
  timeAgo: string;
  imageUrl: string;
  likes: number;
  userName: string;
  content: string;
  recipeTitle: string;
  recipeOwner: string;
  recipeTimeAgo: string;
  recipeLikes: number;
  recipeImageUrl: string;
}

export interface UserProfileType {
  id: string;
  name: string;
  username: string;
  email: string;
  phone: string;
  gender: string;
  dateOfBirth: string;
  address: string;
  followers: number;
  following: number;
  recipes: number;
  activeTime: string;
  status: "active" | "inactive" | "banned";
  avatarUrl: string;
  activities: ActivityItem[];
}

export interface UserSettings {
  theme: "Light" | "Dark" | "System";
  language: string;
}
