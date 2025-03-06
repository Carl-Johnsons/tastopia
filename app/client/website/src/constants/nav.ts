import { SidebarLink } from "@/types/link";

export const sidebarLinks: SidebarLink[] = [
  {
    imgURL: "/assets/icons/users.svg",
    route: "/statistics/system",
    label: "Statistic",
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
    imgURL: "/assets/icons/users.svg",
    route: "/recipes",
    label: "Administer Recipes",
  },

  {
    imgURL: "/assets/icons/users.svg",
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
    imgURL: "/assets/icons/users.svg",
    route: "/users",
    label: "Administer Users",
  },

  {
    imgURL: "/assets/icons/users.svg",
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
    imgURL: "/assets/icons/users.svg",
    route: "/admins",
    label: "Administer Admins",
  },

  {
    imgURL: "/assets/icons/users.svg",
    route: "/activity-log",
    label: "Activity Log",
  },
];
