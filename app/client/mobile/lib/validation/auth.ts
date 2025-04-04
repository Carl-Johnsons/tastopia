import { IDENTIFIER_TYPE } from "@/api/user";
import { EMAIL_REGEX, PHONE_NUMBER_REGEX } from "@/constants/regex";
import { lazy, object, ref, string } from "yup";

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

export const forgotPasswordSchema = object({
  identifier: lazy(value => {
    if (typeof value !== "string") {
      return string().required("Please enter email or phone number.");
    }

    if (EMAIL_REGEX.test(value)) {
      return string().email("Please enter a valid email address.");
    } else if (PHONE_NUMBER_REGEX.test(value)) {
      return string().matches(
        /^(?:(?:\+|00)84|0)(3[2-9]|5[2|5-9]|7[0|6-9]|8[1-9]|9[0-9])[0-9]{7}$/,
        "Please enter a valid phone number."
      );
    }

    return string().required("Please enter email or phone number.");
  })
});

export const changePasswordSchema = object({
  password: passwordSchema,
  rePassword: string()
    .oneOf([ref("password")], "Passwords must match.")
    .required("Please confirm your password.")
});

export const registerSchema = object({
  fullName: string().required("Please enter your full name."),
  identifier: lazy(value => {
    if (typeof value !== "string") {
      return string().required("Please enter email or phone number.");
    }

    if (EMAIL_REGEX.test(value)) {
      return string().email("Please enter a valid email address.");
    } else if (PHONE_NUMBER_REGEX.test(value)) {
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

export const getVerifyIdentifierUpdateSchema = (type: IDENTIFIER_TYPE) =>
  object({
    OTP: string().length(6, "Please complete the OTP."),
    identifier: lazy(value => {
      const identifierType = type === IDENTIFIER_TYPE.EMAIL ? "email" : "phone number";

      if (typeof value !== "string") {
        return string().required(`Please enter ${identifierType}.`);
      }

      switch (type) {
        case IDENTIFIER_TYPE.EMAIL:
          return string().email("Please enter a valid email address.");
        case IDENTIFIER_TYPE.PHONE_NUMBER:
          return string().matches(
            /^(?:(?:\+|00)84|0)(3[2-9]|5[2|5-9]|7[0|6-9]|8[1-9]|9[0-9])[0-9]{7}$/,
            "Please enter a valid phone number."
          );
      }
    })
  });
