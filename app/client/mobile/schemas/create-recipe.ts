import * as yup from "yup";

const imageFileSchema = yup.object().shape({
  uri: yup.string().default(""),
  type: yup.string().default(""),
  name: yup.string().default("")
});

const schema = yup.object().shape({
  title: yup.string().required("validation.title"),
  description: yup.string().required("validation.description"),
  serves: yup
    .number()
    .typeError("validation.serves.typeError")
    .required("validation.serves.required")
    .positive("validation.serves.positive")
    .integer("validation.serves.integer"),
  cookTime: yup.string().required("validation.cookTime"),
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
  )
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
};

type FormCreateRecipeType = yup.InferType<typeof schema>;

export { schema, FormCreateRecipeType, CreateRecipeFormValue };
