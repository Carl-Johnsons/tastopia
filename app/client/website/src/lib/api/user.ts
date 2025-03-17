import { axiosInstance } from "@/constants/host";
import { loginSchema } from "@/schemas/auth";
import { InferType } from "yup";
import { useMutation } from "@tanstack/react-query";

export type LoginParams = InferType<typeof loginSchema>;
export enum IDENTIFIER_TYPE {
  EMAIL,
  PHONE_NUMBER
}

export const useLogin = () => {
  return useMutation<LoginResponse, Error, LoginParams>({
    mutationKey: ["login"],
    mutationFn: async inputs => {
      const body = new URLSearchParams({
        client_id: CLIENT_ID,
        scope: SCOPE,
        grant_type: "password",
        username: inputs.identifier,
        password: inputs.password
      }).toString();

      try {
        const { data } = await axiosInstance.post<LoginResponse>("/connect/token", body, {
          headers: { "Content-Type": "application/x-www-form-urlencoded" }
        });

        return data;
      } catch (error) {
        console.debug("useLogin", stringify(error));

        if (error instanceof AxiosError && error.status === 400) {
          throw new Error("Wrong email, phone number or password.");
        }

        throw new Error("An error has occurred.");
      }
    }
  });
};

