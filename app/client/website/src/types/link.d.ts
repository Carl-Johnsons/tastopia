export interface ParamsProps {
  params: { id: string };
}

export interface SidebarChildLink {
  route: string;
  label: string;
}

export interface SidebarLink {
  imgURL: string;
  route: string;
  label: string;
  children?: SidebarChildLink[];
}
