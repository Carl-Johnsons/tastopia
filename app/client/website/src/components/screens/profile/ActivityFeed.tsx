import Image from "next/image";
import { ActivityItem } from "@/types/user";
import { BanIcon, CommentIcon, DefaultIcon, RecipeIcon } from "@/components/shared/icons";

type ActivityFeedProps = {
  activities: ActivityItem[];
  isLoading?: boolean;
};

export default function ActivityFeed({ activities }: ActivityFeedProps) {
  const getActivityIcon = (type: ActivityItem["type"]) => {
    switch (type) {
      case "recipe":
        return (
          <div className="flex size-8 items-center justify-center rounded-full bg-green-100 text-green-600">
            <RecipeIcon />
          </div>
        );
      case "ban":
        return (
          <div className="flex size-8 items-center justify-center rounded-full bg-red-100 text-red-600">
            <BanIcon />
          </div>
        );
      case "comment":
        return (
          <div className="flex size-8 items-center justify-center rounded-full bg-blue-100 text-blue-600">
            <CommentIcon></CommentIcon>
          </div>
        );
      default:
        return (
          <div className="flex size-8 items-center justify-center rounded-full bg-gray-100 text-gray-600">
            <DefaultIcon />
          </div>
        );
    }
  };

  const getBgColor = (type: string) => {
    switch (type) {
      case "recipe":
        return "bg-green-50 border-green-200";
      case "ban":
        return "bg-red-50 border-red-200";
      case "comment":
        return "bg-blue-50 border-blue-200";
      default:
        return "bg-gray-50 border-gray-200";
    }
  };

  return (
    <div className="bg-white_black100 rounded-xl border border-gray-200 p-6 shadow-sm dark:border-gray-600">
      <h2 className="h3-semibold text-black_white mb-6">Latest Activity</h2>

      <div className="space-y-6">
        {activities.map((activity: ActivityItem, index: number) => (
          <div key={index} className="flex gap-4">
            <div className="shrink-0">
              <div className="size-10 overflow-hidden rounded-full bg-orange-100">
                <Image
                  src="/assets/images/panda.png"
                  alt={activity.userName || "User"}
                  width={40}
                  height={40}
                  className="size-full object-cover"
                />
              </div>
            </div>

            <div className="flex-1">
              <div className="flex flex-wrap items-center gap-2">
                <h3 className="base-semibold text-black_white">{activity.type === "ban" ? "System" : "Tai Duc"}</h3>
                <p className="text-sm text-gray-500">{activity.description}</p>
                <span className="text-sm text-gray-500">{activity.timeAgo}</span>
              </div>

              <div className={`mt-2 rounded-lg border p-4 ${getBgColor(activity.type)}`}>
                <div className="mb-3 flex items-center gap-2">
                  {getActivityIcon(activity.type)}
                  <h4 className="font-medium">
                    {activity.type === "recipe" ? "Create Recipe" : activity.type === "ban" ? "Disable User" : "Create comment"}
                  </h4>
                </div>

                <h3 className="mb-1 text-lg font-medium">{activity.title}</h3>

                <div className="flex items-center gap-2 text-sm text-gray-500">
                  <svg xmlns="http://www.w3.org/2000/svg" className="size-4" viewBox="0 0 20 20" fill="currentColor">
                    <path
                      fillRule="evenodd"
                      d="M10 18a8 8 0 100-16 8 8 0 000 16zm1-12a1 1 0 10-2 0v4a1 1 0 00.293.707l2.828 2.829a1 1 0 101.415-1.415L11 9.586V6z"
                      clipRule="evenodd"
                    />
                  </svg>
                  <span>
                    {activity.time} · {activity.timestamp}
                  </span>
                </div>

                {activity.type === "recipe" && (
                  <div className="mt-4">
                    <div className="mt-3 overflow-hidden rounded-lg">
                      <Image
                        src={"/assets/images/pizza.jpg"}
                        alt={activity.title}
                        width={600}
                        height={300}
                        className="h-auto w-full object-cover"
                      />
                    </div>
                  </div>
                )}

                {activity.type === "comment" && (
                  <>
                    <div className="mt-3 whitespace-pre-line text-gray-700">{activity.content}</div>

                    <div className="mt-4 rounded-lg border border-gray-200 p-4">
                      <div className="text-lg font-medium">{activity.recipeTitle}</div>
                      <div className="mt-1 text-sm text-gray-500">
                        @{activity.recipeOwner} · {activity.recipeTimeAgo}
                      </div>

                      <div className="mt-3 overflow-hidden rounded-lg">
                        <Image
                          src={"/assets/images/pizza.jpg"}
                          alt={activity.recipeTitle ?? "activity comment"}
                          width={600}
                          height={300}
                          className="h-auto w-full object-cover"
                        />
                      </div>
                    </div>
                  </>
                )}
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
