"use client";

import { useEffect, useState } from "react";
import ProfileHeader from "@/components/screens/profile/ProfileHeader";
import ActivityFeed from "@/components/screens/profile/ActivityFeed";
import ProfileInfo from "@/components/screens/profile/ProfileInfo";
import ProfileSettings from "@/components/screens/profile/ProfileSettings";
import { UserProfileType } from "@/types/user";
import { useParams, useRouter } from "next/navigation";

export default function UserProfile() {
  const { id } = useParams<{ id: string }>();
  const router = useRouter();

  const [userData, setUserData] = useState<UserProfileType | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!id) return;

    const fetchUserData = async () => {
      try {
        const mockData = {
          id,
          name: "Tai Duc",
          username: "carl",
          email: "carl@gmail.com",
          phone: "0326344244",
          gender: "Male",
          dateOfBirth: "29/06/2003",
          address: "Can Tho",
          followers: 323,
          following: 20,
          recipes: 170,
          activeTime: "23h50m",
          status: "active",
          avatarUrl: "/assets/avatars/user-avatar.png",
          activities: [
            {
              type: "recipe",
              title: "Pizza mắm tôm",
              description: 'Created a new recipe "Pizza"',
              timestamp: "29/06/2024",
              time: "9:30 PM",
              timeAgo: "3 days ago",
              imageUrl: "/assets/recipes/pizza-mam-tom.jpg",
              likes: 25,
              userName: "carl",
            },
            {
              type: "ban",
              title: "User banned for multiple inappropriate recipes",
              description: "Account was banned.",
              timestamp: "29/06/2024",
              time: "9:30 PM",
              timeAgo: "Just now",
            },
            {
              type: "comment",
              title: "Lorem ipsum dolor sit amet...",
              description: "commented.",
              timestamp: "29/06/2024",
              time: "9:30 PM",
              timeAgo: "2 hours ago",
              content:
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed id ornare ex. Fusce consectetur tellus quis erat congue, in fermentum lectus mattis.",
              recipeTitle: "Hawaii Pizza",
              recipeOwner: "phenchula",
              recipeTimeAgo: "2 days ago",
              recipeLikes: 25,
              recipeImageUrl: "/assets/recipes/hawaii-pizza.jpg",
            },
          ],
        } as any;

        setUserData(mockData);
        setLoading(false);
      } catch (error) {
        console.error("Error fetching user data:", error);
        setLoading(false);
      }
    };

    fetchUserData();
  }, [id]);

  if (loading) {
    return (
      <div className="flex h-screen items-center justify-center">
        <div className="border-primary size-12 animate-spin rounded-full border-4 border-t-transparent"></div>
      </div>
    );
  }

  if (!userData) {
    return (
      <div className="flex h-screen flex-col items-center justify-center">
        <h1 className="text-2xl font-bold">User not found</h1>
        <button onClick={() => router.push("/users")} className="bg-primary hover:bg-primary/90 mt-4 rounded-lg px-4 py-2 text-white">
          Back to Users
        </button>
      </div>
    );
  }

  return (
    <div className="min-h-screen rounded-lg  p-2">
      <div className="mx-auto max-w-6xl">
        <ProfileHeader user={userData} />

        <div className="mt-6 flex flex-col-reverse gap-6 lg:flex-row">
          <div className="flex-1">{userData.activities && <ActivityFeed activities={userData.activities} />}</div>

          <div className="flex flex-col space-y-6 lg:w-1/3">
            <ProfileInfo user={userData} />
            <ProfileSettings />
          </div>
        </div>
      </div>
    </div>
  );
}
