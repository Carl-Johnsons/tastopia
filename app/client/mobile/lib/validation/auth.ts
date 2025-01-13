import { InferType, lazy, object, ref, string } from "yup";

export const passwordSchema = string()
  .required("Please enter password.")
  .min(6, "Password must be at least 6 characters long.")
  .matches(/[A-Z]/, "Password must contain at least one uppercase letter.")
  .matches(/[a-z]/, "Password must contain at least one lowercase letter.")
  .matches(/\d/, "Password must contain at least one digit.")
  .matches(/[^A-Za-z0-9]/, "Password must contain at least one special character.");

export const loginSchema = object({
  identifier: string().required("Please enter email or phone number."),
  password: string().required("Please enter password.")
});

export const registerSchema = object({
  fullName: string().required("Please enter your full name."),
  identifier: lazy(value => {
    const emailRegex = /[a-zA-Z@]/;
    const phoneNumberRegex = /^\d+$/;

    if (typeof value !== "string") {
      return string().required("Please enter email or phone number.");
    }

    if (emailRegex.test(value)) {
      return string().email("Please enter a valid email address.");
    } else if (phoneNumberRegex.test(value)) {
      return string().matches(
        /^(?:(?:\+|00)84|0)(3[2-9]|5[2|5-9]|7[0|6-9]|8[1-9]|9[0-9])[0-9]{7}$/,
        "Please enter a valid phone number."
      );
    }

    return string().required("Please enter email or phone number.");
  }),
  password: passwordSchema,
  rePassword: string()
    .oneOf([ref("password")], "Passwords must match.")
    .required("Please confirm your password.")
});

export const verifySchema = object({
  OTP: string().length(6, "Please complete the OTP.")
});
