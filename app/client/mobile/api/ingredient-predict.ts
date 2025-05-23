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

      const url = "/api/ingredient-predict-v2";
      const { data } = await protectedAxiosInstance.request({
        url: url,
        method: "POST",
        headers: {
          "Content-Type": "multipart/form-data",
          accept: "application/json"
        },
        data: formData
      });
      return data as IngredientStreamResponse;
    }
  });
};

export { useIngredientPredictMutation };
