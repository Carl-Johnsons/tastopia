type IngredientPredictResponse = {
  classifications: {
    class: number;
    confidence: number;
    name: string;
  }[];
};

type IngredientPredictBoxResponse = {
  boxes: number[][];
};
