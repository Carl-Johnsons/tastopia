import { getRecipeReportById } from "@/actions/recipe.action";
import RecipeDetail from "@/components/screen/report/recipe/RecipeDetail";
import ReportList from "@/components/screen/report/recipe/ReportList";
import { ParamsProps } from "@/types/link";
import { ChevronRight } from "lucide-react";

export default async function Page({ params }: ParamsProps) {
  try {
    const { recipe, reports } = await getRecipeReportById({ recipeId: params.id });

    return (
      <div className='flex flex-col gap-10'>
        <div className='flex gap-2'>
          <span className='text-gray-500'>Administer Reports</span>
          <ChevronRight className='text-black_white' />
          <span className='text-black_white'>Recipe</span>
          <ChevronRight className='text-black_white' />
          <span className='text-black_white'>Detail</span>
        </div>
        <div className='container grid lg:grid-cols-[70%_30%] gap-10 lg:gap-3'>
          <ReportList
            reports={reports}
            className='lg:col-start-2' 
          />
          <RecipeDetail
            recipe={recipe}
            className='lg:col-start-1 lg:row-start-1'
          />
        </div>
      </div>
    );
  } catch (error) {
    console.log(error);
    return <div>Something went wrong. :(</div>;
  }
}
