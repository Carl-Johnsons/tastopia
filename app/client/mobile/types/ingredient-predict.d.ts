type IngredientStreamResponse = {
  classifications: {
    class: number;
    confidence: number;
    name: {
      en: string;
      vi: string;
    };
  }[];
  boxes: number[][];
};
