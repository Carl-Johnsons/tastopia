import { useTranslations } from "next-intl";
import * as z from "zod";

export const validCategories = ["All", "DishType", "Ingredient"];
export const validVietnameseCategories = ["Tất cả", "Loại món ăn", "Nguyên liệu"];

type TFunction = ReturnType<typeof useTranslations<"administerTags">>;

export const getTagSchema = (t: TFunction, language: string) => {
  const CreateTagSchema = z.object({
    code: z
      .string()
      .nonempty(t("form.errors.codeRequired"))
      .max(50, t("form.errors.codeMaxLength")),

    vi: z
      .string()
      .nonempty(t("form.errors.nameRequired"))
      .max(50, t("form.errors.nameMaxLength")),

    en: z
      .string()
      .nonempty(t("form.errors.nameRequired"))
      .max(50, t("form.errors.nameMaxLength")),

    category: z
      .string()
      .nonempty(t("form.errors.categoryRequired"))
      .max(20, t("form.errors.categoryMaxLength"))
      .refine(
        val => validVietnameseCategories.includes(val) || validCategories.includes(val),
        {
          message: t("form.errors.categoryInvalid", {
            categories:
              language === "vi"
                ? validVietnameseCategories.join(", ")
                : validCategories.join(", ")
          })
        }
      ),

    tagImage: z.any()
  });

  const UpdateTagSchema = CreateTagSchema.extend({
    status: z
      .string()
      .nonempty(t("form.errors.statusRequired"))
      .refine(val => ["Pending", "Active", "Inactive"].includes(val), {
        message: t("form.errors.statusInvalid", { statuses: "Pending, Active, Inactive" })
      })
  });

  return { CreateTagSchema, UpdateTagSchema };
};

function checkFileType(file: File) {
  if (file?.name) {
    const fileType = file.name.split(".").pop();
    if (fileType && ["gif", "png", "jpg"].includes(fileType)) return true;
  }
  return false;
}

export const fileSchema = z.object({});
