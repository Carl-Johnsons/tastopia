declare module "*.jpg";
declare module "*.jpeg";
declare module "*.png";

export type ImageFileType = {
  uri: string;
  type: string;
  name: string;
};

export type FileObject = {
  id: string;
  previewPath: string;
  file?: ImageFileType;
};
