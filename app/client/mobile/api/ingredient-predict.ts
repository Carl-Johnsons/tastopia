import { protectedAxiosInstance } from "@/constants/host";
import { useMutation } from "react-query";

interface Props {
  file?: Blob;
}

const useIngredientPredictMutation = () => {
  return useMutation({
    mutationFn: async ({ file }: Props) => {
      const formData = new FormData();
      formData.append("file", file as unknown as Blob);

      const url = "/api/ingredient-predict";
      const { data } = await protectedAxiosInstance.request({
        url: url,
        method: "POST",
        headers: {
          "Content-Type": "multipart/form-data",
          accept: "application/json"
        },
        data: formData
      });
      return data as IngredientPredictResponse;
    }
  });
};

const useIngredientPredictBoxMutation = () => {
  return useMutation({
    mutationFn: async ({ file }: Props) => {
      const formData = new FormData();
      formData.append("file", file as unknown as Blob);

      const url = "/api/ingredient-predict/box";
      const { data } = await protectedAxiosInstance.request({
        url: url,
        method: "POST",
        headers: {
          "Content-Type": "multipart/form-data",
          accept: "application/json"
        },
        data: formData
      });
      return data as IngredientPredictBoxResponse;
    }
  });
};

export { useIngredientPredictMutation, useIngredientPredictBoxMutation };
