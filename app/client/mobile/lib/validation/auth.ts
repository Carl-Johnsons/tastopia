import { z } from "zod";

export const loginWithEmailSchema = z.object({
  identifier: z.string().email("Please enter a valid email address."),
  password: z.string({ required_error: "Please enter password." })
});

export const loginWithPhoneNumberSchema = z.object({
  identifier: z
    .string()
    .regex(
      /^(?:(?:\+|00)84|0)(3[2-9]|5[2|5-9]|7[0|6-9]|8[1-9]|9[0-9])[0-9]{7}$/,
      "Please enter a valid phone number."
    ),
  password: z.string({ required_error: "Please enter password." })
});

const passwordSchema = z
  .string()
  .min(6, { message: "Password must be at least 6 characters long." })
  .regex(/[A-Z]/, { message: "Password must contain at least one uppercase letter." })
  .regex(/[a-z]/, { message: "Password must contain at least one lowercase letter." })
  .regex(/\d/, { message: "Password must contain at least one digit." })
  .regex(/[^A-Za-z0-9]/, {
    message: "Password must contain at least one special character."
  });

export const registerWithEmailSchema = z.object({
  fullName: z.string().min(1, "Please enter your full name."),
  identifier: z.string().email("Please enter a valid email address."),
  password: passwordSchema
});

export const registerWithPhoneNumberSchema = z.object({
  fullName: z.string().min(1, "Please enter your full name."),
  identifier: z
    .string()
    .regex(
      /^(?:(?:\+|00)84|0)(3[2-9]|5[2|5-9]|7[0|6-9]|8[1-9]|9[0-9])[0-9]{7}$/,
      "Please enter a valid phone number."
    ),
  password: passwordSchema
});

export const verifySchema = z.object({
  OTP: z.string().length(6, "Please complete the OTP.")
});
