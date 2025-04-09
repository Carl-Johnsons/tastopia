import { IDENTIFIER_TYPE } from "@/api/user";
import { useMemo } from "react";
import { useTranslation } from "react-i18next";
import { lazy, object, string } from "yup";

export default function useVerifyIdentifierSchema(type: IDENTIFIER_TYPE) {
  const { t: tOtp } = useTranslation("verifyUpdateIdentifier", { keyPrefix: "otp" });
  const { t: tErrOtp } = useTranslation("verifyUpdateIdentifier", {
    keyPrefix: "otp.form.errors"
  });

  const { t: tUpdate } = useTranslation("verifyUpdateIdentifier", {
    keyPrefix: "update"
  });
  const { t: tErrUpdate } = useTranslation("verifyUpdateIdentifier", {
    keyPrefix: "update.form.errors"
  });

  const schema = useMemo(
    () =>
      object({
        OTP: string().length(6, tErrOtp("length")),
        identifier: lazy(value => {
          const identifierType =
            type === IDENTIFIER_TYPE.EMAIL ? tOtp("email") : tOtp("phone");

          if (typeof value !== "string") {
            return string().required(`${tUpdate("pleaseEnter")} ${identifierType}.`);
          }

          switch (type) {
            case IDENTIFIER_TYPE.EMAIL:
              return string()
                .required(tErrUpdate("email.required"))
                .email(tErrUpdate("email.invalid"));
            case IDENTIFIER_TYPE.PHONE_NUMBER:
              return string()
                .required(tErrUpdate("phone.required"))
                .matches(
                  /^(?:(?:\+|00)84|0)(3[2-9]|5[2|5-9]|7[0|6-9]|8[1-9]|9[0-9])[0-9]{7}$/,
                  tErrUpdate("phone.invalid")
                );
          }
        })
      }),
    [type, tErrOtp, tErrUpdate, tOtp, tUpdate]
  );

  return { schema };
}
