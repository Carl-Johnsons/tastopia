type IngredientStreamResponse = {
  classifications: {
    class: number;
    confidence: number;
    name: string;
  }[];
  boxes: number[][];
};
