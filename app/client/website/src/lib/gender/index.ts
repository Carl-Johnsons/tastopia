export {
  normalizeGender,
  getGenderDisplayValue,
  isValidGender,
  getAllGenders
} from "./utils";

export type GenderTranslationFunction = (key: string) => string;

export interface GenderDisplayOptions {
  translationKey?: string;
  fallback?: string;
}
