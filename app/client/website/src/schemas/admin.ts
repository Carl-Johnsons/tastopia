import { Gender } from "@/constants/gender";
import { ImageListType } from "react-images-uploading";
import * as z from "zod";

const MAX_FILE_SIZE = 15000000;
const PHONE_REGEX = /^(?:(?:\+|00)84|0)(3[2-9]|5[2|5-9]|7[0|6-9]|8[1-9]|9[0-9])[0-9]{7}$/;
const GENDER: Array<string> = [Gender.Male, Gender.Female];
const STATUS: Array<string> = ["Active", "Inactive"];
const IMAGE_TYPE = ["image/jpg", "image/jpeg", "image/png", "image/webp"];

const imageSchemma = (t: (key: string) => string) =>
  z.array(
    z.object({
      dataURL: z.string().optional(),
      file: z
        .object({
          size: z.number().max(MAX_FILE_SIZE, t("image.errors.maxSize")),
          type: z.string().refine(type => IMAGE_TYPE.includes(type), {
            message: t("image.errors.acceptType")
          })
        })
        .optional()
    }),
    {
      required_error: t("image.errors.required")
    }
  );

export const getCreateAdminSchema = (t: (key: string) => string) =>
  z.object({
    name: z
      .string({
        required_error: t("name.errors.required")
      })
      .max(50, t("name.errors.max")),
    gmail: z
      .string({ required_error: t("gmail.errors.required") })
      .email(t("gmail.errors.invalid")),
    phone: z
      .string({ required_error: t("phone.errors.required") })
      .refine(val => PHONE_REGEX.test(val), {
        message: t("phone.errors.invalid")
      }),
    password: z
      .string({ required_error: t("password.errors.required") })
      .min(6, t("password.errors.min"))
      .max(50, t("password.errors.max")),
    gender: z
      .string({ required_error: t("gender.errors.required") })
      .refine(val => GENDER.includes(val), {
        message: t("gender.errors.invalid")
      }),
    dateOfBirth: z.date({
      required_error: t("dateOfBirth.errors.required"),
      invalid_type_error: t("dateOfBirth.errors.invalid")
    }),
    address: z.string({ required_error: t("address.errors.required") }),
    status: z
      .string({ required_error: t("status.errors.required") })
      .refine(val => STATUS.includes(val), {
        message: t("status.errors.invalid")
      }),
    image: imageSchemma(t)
  });
