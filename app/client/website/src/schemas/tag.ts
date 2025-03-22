import * as z from "zod";

const validCategories = ["All", "DishType", "Ingredient"];
const MAX_FILE_SIZE = 15000000;

export const CreateTagSchema = z.object({
  code: z
    .string()
    .nonempty("Code is required")
    .max(50, "Code must be 50 characters or less"),

  value: z
    .string()
    .nonempty("Ingredient name is required")
    .max(50, "Ingredient name must be 50 characters or less"),

  category: z
    .string()
    .nonempty("Category is required")
    .max(20, "Category must be 20 characters or less")
    .refine(val => validCategories.includes(val), {
      message: "Invalid category. Must be one of: " + validCategories.join(", ")
    }),

  tagImage: z.any()
});

export const UpdateTagSchema = CreateTagSchema.extend({
  status: z
    .string()
    .nonempty("Status is required")
    .refine(val => ["Pending", "Active", "Inactive"].includes(val), {
      message: "Invalid status. Must be one of: Pending, Active, Inactive"
    })
});

function checkFileType(file: File) {
  if (file?.name) {
    const fileType = file.name.split(".").pop();
    if (fileType && ["gif", "png", "jpg"].includes(fileType)) return true;
  }
  return false;
}

export const fileSchema = z.object({});
