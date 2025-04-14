import { IDENTIFIER_TYPE } from "../api/user";

export const getIdentifierType = (value: string) => {
  const emailRegex = /[@]/;
  const phoneNumberRegex = /^\d+$/;

  if (emailRegex.test(value)) {
    return IDENTIFIER_TYPE.EMAIL;
  } else if (phoneNumberRegex.test(value)) {
    return IDENTIFIER_TYPE.PHONE_NUMBER;
  }

  return null;
};
