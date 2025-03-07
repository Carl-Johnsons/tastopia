import { SidebarLink } from "@/types/link";

export const sidebarLinks: SidebarLink[] = [
  {
    imgURL: "/assets/icons/chart-dots-2.svg",
    route: "/statistics/system",
    label: "Statistics",
    children: [
      {
        route: "/statistics/system",
        label: "System",
      },
      {
        route: "/statistics/income",
        label: "Income",
      },
    ],
  },
  {
    imgURL: "/assets/icons/grill.svg",
    route: "/recipes",
    label: "Administer Recipes",
  },
  {
    imgURL: "/assets/icons/avocado.svg",
    route: "/tags/all",
    label: "Administer Tags",
    children: [
      {
        route: "/tags/all",
        label: "All",
      },
      {
        route: "/tags/review",
        label: "Review",
      },
    ],
  },
  {
    imgURL: "/assets/icons/user.svg",
    route: "/users",
    label: "Administer Users",
  },
  {
    imgURL: "/assets/icons/message-report.svg",
    route: "/reports/recipes",
    label: "Administer Reports",
    children: [
      {
        route: "/reports/recipes",
        label: "Recipe",
      },
      {
        route: "/reports/comments",
        label: "Comment",
      },
      {
        route: "/reports/users",
        label: "User",
      },
    ],
  },
  {
    imgURL: "/assets/icons/shield-minus.svg",
    route: "/admins",
    label: "Administer Admins",
  },
  {
    imgURL: "/assets/icons/activity.svg",
    route: "/activity-log",
    label: "Activity Log",
  },
];
