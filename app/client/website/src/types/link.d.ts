export interface SidebarLink {
  imgURL?: string;
  route: string;
  label: string;
  children?: SidebarLink[];
}
