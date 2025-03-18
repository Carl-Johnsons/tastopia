import { RecipeError } from "@/generated/enums/recipe.error.enum";
import { IErrorResponseDTO as IBaseErrorResponseDTO } from "@/generated/interfaces/common.interface";

export type GetRecipeCommentResponse = {
  paginatedData: CommentType[];
  metadata: {
    hasNextPage: boolean;
    totalPage: number;
  };
};

export type RecipeReport = {
  id: number;
  recipeName: string;
  recipeOwner: string;
  recipeImageUrl: string;
  reporter: string;
  reporterAvatar: string;
  reportReason: string;
  reportCodes: string[];
  createdDate: string;
  status: "PENDING" | "DONE";
};

export interface IErrorResponseDTO extends IBaseErrorResponseDTO {
  message: RecipeError 
};
