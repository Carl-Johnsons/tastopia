type CreateIngredientType = {
  key: string;
  value: string;
};

type CreateStepType = {
  key: string;
  content: string;
  images: ImageFileType[];
};

type UpdateImage = {
  defaultImages?: string[]; // public url of images api return
  additionalImages?: ImageFileType[];
  deleteUrls?: string[];
};

type UpdateStepType = {
  key: string;
  content: string;
  images: UpdateImage;
};
