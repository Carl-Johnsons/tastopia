import { ClockIcon } from "lucide-react";
import { IStep } from "../../../../../../mobile/generated/interfaces/recipe.interface";
import Image from "@/components/shared/common/Image";

type IngredientProps = {
  ingredient: string[];
  serves?: number;
};

type InstructionProps = {
  steps: IStep[];
  cookTime?: string;
};

type ContentProps = IngredientProps & InstructionProps;

export default function Content({ ingredient, serves, steps, cookTime }: ContentProps) {
  return (
    <div className='grid gap-6 lg:grid-cols-[300px_1fr]'>
      <Ingredient
        ingredient={ingredient}
        serves={serves}
      />
      <Instruction
        steps={steps}
        cookTime={cookTime}
      />
    </div>
  );
}

const Ingredient = ({ ingredient, serves = 0 }: IngredientProps) => {
  return (
    <div className='flex flex-col gap-3'>
      <h2 className='text-black_white text-2xl font-semibold'>Ingredient</h2>
      <span className='text-sm text-gray-600'>
        For {serves} Serving{serves > 2 && "s"}
      </span>
      {ingredient.map((item, index) => (
        <div key={item}>
          {index !== 0 && <div className='border-black_white border-t border-dashed' />}
          <span className='text-black_white'>{item}</span>
        </div>
      ))}
    </div>
  );
};

const Instruction = ({ steps, cookTime = "Not specified" }: InstructionProps) => {
  return (
    <div className='flex flex-col gap-3'>
      <h2 className='text-black_white text-2xl font-semibold'>Instruction</h2>
      <div className='flex items-center gap-2'>
        <ClockIcon className='size-6 text-gray-600' />
        <span className='text-sm text-gray-600'>Cook Time: {cookTime}</span>
      </div>
      <div className='flex flex-col gap-5'>
        {steps.map(({ id, content, ordinalNumber, attachedImageUrls }) => (
          <div key={id}>
            <span className='text-black_white flex items-center gap-2'>
              <span className='text-white_black flex-center flex size-6 rounded-full bg-primary p-2'>
                {ordinalNumber}
              </span>
              {content}
            </span>

            <div className='flex gap-3'>
              {attachedImageUrls?.map((url, index) => (
                <Image
                  key={index + url}
                  src={url}
                  alt={`Instrunciton image ${index + 1}`}
                  fill
                  className='h-[160px] w-[260] rounded-lg'
                />
              ))}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};
