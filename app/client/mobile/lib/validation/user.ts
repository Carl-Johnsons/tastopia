import { object, string } from "yup";

export const updateUserSchema = object({
  displayName: string().max(50, "Display name cannot exceed 50 characters."),
  bio: string().max(160, "Bio cannot exceed 160 characters."),
  gender: string().oneOf(["male", "female"], "Gender must be 'male' or 'female'."),
  username: string()
    .required("Username is required.")
    .matches(
      /^[a-zA-Z0-9_.-]+$/,
      "Username can only contain letters, numbers, underscores, hyphens, and periods."
    )
    .min(6, "Username must be at least 6 characters long.")
    .max(30, "Username cannot exceed 30 characters."),
  avatar: object(),
  background: object()
});
