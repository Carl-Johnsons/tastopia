import { SidebarLink } from "@/types/link";

export const sidebarLinks: SidebarLink[] = [
  {
    imgURL: "/assets/icons/chart-dots-2.svg",
    route: "/statistics",
    label: "statistics"
  },
  {
    imgURL: "/assets/icons/grill.svg",
    route: "/recipes",
    label: "administerRecipes"
  },
  {
    imgURL: "/assets/icons/avocado.svg",
    route: "/tags",
    label: "administerTags"
  },
  {
    imgURL: "/assets/icons/user.svg",
    route: "/users",
    label: "administerUsers"
  },
  {
    imgURL: "/assets/icons/message-report.svg",
    route: "/reports/recipes",
    label: "administerReports",
    children: [
      {
        route: "/reports/recipes",
        label: "administerReportsRecipe"
      },
      {
        route: "/reports/comments",
        label: "administerReportsComment"
      },
      {
        route: "/reports/users",
        label: "administerReportsUser"
      }
    ]
  },
  {
    imgURL: "/assets/icons/shield-minus.svg",
    route: "/admins",
    label: "administerAdmins"
  },
  {
    imgURL: "/assets/icons/activity.svg",
    route: "/activity-log",
    label: "activityLog"
  }
];
