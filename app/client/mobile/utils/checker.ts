import { EMAIL_REGEX, PHONE_NUMBER_REGEX } from "@/constants/regex";
import { IDENTIFIER_TYPE } from "../api/user";

export const getIdentifierType = (value: string) => {
  if (EMAIL_REGEX.test(value)) {
    return IDENTIFIER_TYPE.EMAIL;
  } else if (PHONE_NUMBER_REGEX.test(value)) {
    return IDENTIFIER_TYPE.PHONE_NUMBER;
  }

  return null;
};
