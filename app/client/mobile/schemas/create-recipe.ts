import * as yup from "yup";
const imageFileSchema = yup.object().shape({
  uri: yup.string().default(""),
  type: yup.string().default(""),
  name: yup.string().default("")
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
  ingredients: yup.array().of(
    yup.object().shape({
      key: yup.string(),
      value: yup.string().default("")
    })
  ),
  steps: yup.array().of(
    yup.object().shape({
      key: yup.string().required(),
      content: yup.string().default(""),
      images: yup.array().of(imageFileSchema).default([])
    })
  ),
  tags: yup.array().of(yup.string().required()).nullable()
});

type CreateRecipeFormValue = {
  ingredients?:
    | {
        key?: string | undefined;
        value?: string | undefined;
      }[]
    | undefined;
  steps?:
    | {
        images?:
          | {
              uri?: string | undefined;
              type?: string | undefined;
              name?: string | undefined;
            }[]
          | undefined;
        key: string;
        content: string;
      }[]
    | undefined;
  title: string;
  description: string;
  serves: number;
  cookTime: string;
  tags: string[];
};

type FormCreateRecipeType = yup.InferType<typeof schema>;
type ImageFileType = yup.InferType<typeof imageFileSchema>;

export { schema, FormCreateRecipeType, CreateRecipeFormValue, ImageFileType };
