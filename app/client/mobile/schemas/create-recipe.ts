import * as yup from "yup";

const schema = yup.object().shape({
  title: yup.string().required("validation.title"),
  description: yup.string().required("validation.description"),
  serves: yup.string().required("validation.serves"),
  cookTime: yup.string().required("validation.cookTime")
});

type FormCreateRecipeType = yup.InferType<typeof schema>;

export { schema, FormCreateRecipeType };
