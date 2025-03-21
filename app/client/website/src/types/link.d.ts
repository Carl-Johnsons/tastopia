export interface BaseParam {
  id: string;
}

export interface ParamsProps {
  params: BaseParam;
}

export interface CommentDetailParamProps {
  params: BaseParam & {
    recipeId: string;
  };
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
