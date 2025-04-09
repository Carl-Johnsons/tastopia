type IngredientStreamResponse = {
  classifications: {
    class: number;
    confidence: number;
    code: string;
    name: {
      en: string;
      vi: string;
    };
  }[];
  boxes: number[][];
};
