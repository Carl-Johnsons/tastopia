import * as yup from "yup";

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
      value: yup.string()
    })
  )
});

type FormCreateRecipeType = yup.InferType<typeof schema>;

export { schema, FormCreateRecipeType };
