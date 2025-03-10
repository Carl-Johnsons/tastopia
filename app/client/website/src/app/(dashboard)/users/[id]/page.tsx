import { ParamsProps } from "@/types/link";
import { useGetUserById } from "@/actions/user.action";
import ProfileHeader from "@/components/screen/profile/ProfileHeader";
import ActivityFeed from "@/components/screen/profile/ActivityFeed";
import ProfileInfo from "@/components/screen/profile/ProfileInfo";
import ProfileSettings from "@/components/screen/profile/ProfileSettings";

export default async function UserProfile({ params }: ParamsProps) {
  const currentUser = await useGetUserById(params.id);

  const mockData = {
    id: params.id,
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

  if (!mockData) {
    return (
      <div className="flex h-screen flex-col items-center justify-center">
        <h1 className="font-bold text-2xl">User not found</h1>
      </div>
    );
  }

  return (
    <div className="min-h-screen rounded-lg  p-2">
      <div className="mx-auto max-w-6xl">
        <ProfileHeader user={currentUser} />

        <div className="mt-6 flex flex-col-reverse gap-6 lg:flex-row">
          <div className="flex-1">{mockData.activities && <ActivityFeed activities={mockData.activities} />}</div>

          <div className="flex flex-col space-y-6 lg:w-1/3">
            <ProfileInfo user={currentUser} />
            <ProfileSettings />
          </div>
        </div>
      </div>
    </div>
  );
}
