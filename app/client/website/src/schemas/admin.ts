import { Gender } from "@/constants/gender";
import { isValid, parse } from "date-fns";
import * as z from "zod";

export const MAX_FILE_SIZE = 15 * 1024 * 1024; // 15MB
export const PHONE_REGEX = /^0(3|5|7|8|9)+([0-9]{8})$/;
const DATE_REGEX = /\d{1,2}\/\d{1,2}\/\d\d\d\d/;
const GENDER: Array<string> = [Gender.Male, Gender.Female];
export const IMAGE_TYPE = ["image/jpg", "image/jpeg", "image/png", "image/webp"];

export const imageSchemma = (t: (key: string) => string) =>
  z
    .array(
      z.object({
        dataURL: z.string().optional(),
        file: z
          .any()
          .refine(image => image?.size <= MAX_FILE_SIZE, {
            message: t("image.errors.maxSize")
          })
          .refine(image => IMAGE_TYPE.includes(image?.type), {
            message: t("image.errors.acceptType")
          })
          .optional()
      }),
      {
        required_error: t("image.errors.required")
      }
    )
    .min(1, t("image.errors.required"));

export const getCreateAdminSchema = (t: (key: string) => string) =>
  z.object({
    name: z
      .string({
        required_error: t("name.errors.required")
      })
      .nonempty({
        message: t("name.errors.required")
      })
      .max(50, t("name.errors.max")),
    gmail: z
      .string({
        required_error: t("gmail.errors.required")
      })
      .nonempty({
        message: t("gmail.errors.required")
      })
      .email(t("gmail.errors.invalid")),
    phone: z
      .string({
        required_error: t("phone.errors.required")
      })
      .nonempty({
        message: t("phone.errors.required")
      })
      .refine(val => PHONE_REGEX.test(val), {
        message: t("phone.errors.invalid")
      }),
    gender: z
      .string({
        required_error: t("gender.errors.required")
      })
      .refine(val => GENDER.includes(val), {
        message: t("gender.errors.invalid")
      }),
    dob: z
      .string({
        required_error: t("dateOfBirth.errors.required")
      })
      .regex(DATE_REGEX, {
        message: t("dateOfBirth.errors.invalid")
      })
      .refine(
        val => {
          const parsedDate = parse(val, "dd/MM/yyyy", new Date());
          return isValid(parsedDate);
        },
        {
          message: t("dateOfBirth.errors.invalid")
        }
      )
      .refine(
        val => {
          const parsedDate = parse(val, "dd/MM/yyyy", new Date());
          return parsedDate >= new Date("1900-01-01");
        },
        {
          message: t("dateOfBirth.errors.min")
        }
      )
      .refine(
        val => {
          const parsedDate = parse(val, "dd/MM/yyyy", new Date());
          return parsedDate <= new Date();
        },
        {
          message: t("dateOfBirth.errors.max")
        }
      ),
    address: z
      .string({
        required_error: t("address.errors.required")
      })
      .nonempty({
        message: t("address.errors.required")
      }),
    avatarFile: imageSchemma(t)
  });

export const getUpdateAdminSchema = (t: (key: string) => string) => {
  return getCreateAdminSchema(t);
};
