declare module "*.jpg";
declare module "*.jpeg";
declare module "*.png";

type ImageFileType = {
  uri: string;
  type: string;
  name: string;
};

type ImageFileObject = {
  id: string;
  previewPath: string;
  file?: ImageFileType;
};
