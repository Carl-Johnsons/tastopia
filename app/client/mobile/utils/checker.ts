import { IDENTIFIER_TYPE } from "../api/user";

export const getIdentifierType = (value?: string) => {
  const emailRegex = /[a-zA-Z@]/;
  const phoneNumberRegex = /^\d+$/;

  if (!value) return null;

  if (emailRegex.test(value)) {
    return IDENTIFIER_TYPE.EMAIL;
  } else if (phoneNumberRegex.test(value)) {
    return IDENTIFIER_TYPE.PHONE_NUMBER;
  }

  return null;
};
