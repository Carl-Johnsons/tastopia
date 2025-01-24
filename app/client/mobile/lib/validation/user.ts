import { object, string } from "yup";

export const updateUserSchema = object({
  displayName: string().max(50, "max").required("required"),
  bio: string().max(160, "max").nullable(),
  gender: string(),
  username: string()
    .required("required")
    .matches(/^[a-zA-Z0-9_.-]+$/, "matches")
    .min(6, "min")
    .max(30, "max"),
  avatar: object(),
  background: object()
});
