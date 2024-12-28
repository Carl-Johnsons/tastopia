import { z } from "zod";

export const loginWithEmailSchema = z.object({
  identifier: z.string().email("Please enter a valid email address."),
  password: z.string().min(1, "Please enter password.")
});

export const loginWithPhoneNumberSchema = z.object({
  identifier: z
    .string()
    .regex(
      /^(?:(?:\+|00)84|0)(3[2-9]|5[2|5-9]|7[0|6-9]|8[1-9]|9[0-9])[0-9]{7}$/,
      "Please enter a valid phone number."
    ),
  password: z.string().min(1, "Please enter password.")
});

export const registerWithEmailSchema = z.object({
  fullName: z.string().min(1, "Please enter your full name."),
  identifier: z.string().email("Please enter a valid email address."),
  password: z.string().min(6, "Password must be at least 6 characters.")
});

export const registerWithPhoneNumberSchema = z.object({
  fullName: z.string().min(1, "Please enter your full name."),
  identifier: z
    .string()
    .regex(
      /^(?:(?:\+|00)84|0)(3[2-9]|5[2|5-9]|7[0|6-9]|8[1-9]|9[0-9])[0-9]{7}$/,
      "Please enter a valid phone number."
    ),
  password: z.string().min(6, "Password must be at least 6 characters.")
});

export const verifySchema = z.object({
  OTP: z.string().length(6, "Please complete the OTP."),
  accessToken: z.string()
});
