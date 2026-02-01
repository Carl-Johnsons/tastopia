import { Gender } from "@/constants/gender";

/**
 * Normalizes gender value from API response to match the Gender enum
 * Handles case-insensitive comparison
 *
 * @param gender - Raw gender value from API (e.g., "male", "MALE", "female", "FEMALE")
 * @returns Normalized Gender enum value or undefined
 */
export const normalizeGender = (gender?: string): Gender | undefined => {
  if (!gender) return undefined;

  switch (gender.toUpperCase()) {
    case "MALE":
      return Gender.Male;
    case "FEMALE":
      return Gender.Female;
    default:
      return undefined;
  }
};

/**
 * Gets translated gender display value for UI components
 *
 * @param gender - Raw gender value from API
 * @param t - Translation function
 * @param translationKey - Base key for gender translations (default: "genders")
 * @returns Translated gender string or null if no gender provided
 */
export const getGenderDisplayValue = (
  gender: string | undefined,
  t: any,
  translationKey: string = "genders"
): string | null => {
  if (!gender) return null;

  switch (gender.toUpperCase()) {
    case "MALE":
      return t(`${translationKey}.male`);
    case "FEMALE":
      return t(`${translationKey}.female`);
    default:
      return gender;
  }
};

/**
 * Checks if a gender value is valid according to the Gender enum
 *
 * @param gender - Gender value to validate
 * @returns True if the gender is valid
 */
export const isValidGender = (gender?: string): boolean => {
  return normalizeGender(gender) !== undefined;
};

/**
 * Gets all available gender values as an array
 *
 * @returns Array of Gender enum values
 */
export const getAllGenders = (): Gender[] => {
  return Object.values(Gender);
};
