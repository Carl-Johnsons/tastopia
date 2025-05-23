import * as yup from "yup";

const imageFileSchema = yup.object().shape({
  uri: yup.string().default(""),
  type: yup.string().default(""),
  name: yup.string().default("")
});

const updateImageSchema = yup.object().shape({
  defaultImages: yup.array().of(yup.string().url().required()).nullable(),
  additionalImages: yup.array().of(imageFileSchema).default([]),
  deleteUrls: yup.array().of(yup.string().url().required()).default([])
});

const schema = yup.object().shape({
  title: yup.string().required("validation.title").max(50, "validation.title.maxLength"),
  description: yup
    .string()
    .required("validation.description")
    .max(500, "validation.description.maxLength"),
  serves: yup
    .number()
    .typeError("validation.serves.typeError")
    .required("validation.serves.required")
    .positive("validation.serves.positive")
    .integer("validation.serves.integer"),
  cookTime: yup
    .string()
    .required("validation.cookTime")
    .max(50, "validation.cookTime.maxLength"),
  ingredients: yup
    .array()
    .of(
      yup.object().shape({
        key: yup.string().required(),
        value: yup.string().default("")
      })
    )
    .default([]),
  steps: yup
    .array()
    .of(
      yup.object().shape({
        key: yup.string().required(),
        content: yup.string().default(""),
        images: updateImageSchema.required()
      })
    )
    .default([]),
  tags: yup.array().of(yup.string().required())
});

type ImageFileType = yup.InferType<typeof imageFileSchema>;

type UpdateRecipeFormValue = {
  ingredients: {
    key: string;
    value: string;
  }[];
  steps: {
    images: {
      defaultImages?: string[] | null | undefined;
      additionalImages: {
        type: string;
        name: string;
        uri: string;
      }[];
      deleteUrls: string[];
    };
    key: string;
    content: string;
  }[];
  title: string;
  description: string;
  serves: number;
  cookTime: string;
  tags?: string[];
};

type FormUpdateRecipeType = yup.InferType<typeof schema>;

export { schema, FormUpdateRecipeType, UpdateRecipeFormValue };
