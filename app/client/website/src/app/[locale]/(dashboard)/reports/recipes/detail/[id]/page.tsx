import { getRecipeReportById } from "@/actions/recipe.action";
import RecipeDetail from "@/components/screen/report/recipe/RecipeDetail";
import ReportList from "@/components/screen/report/recipe/ReportList";
import { Link } from "@/i18n/navigation";
import { ParamsProps } from "@/types/link";
import { ChevronRight } from "lucide-react";
import { getLocale } from "next-intl/server";

export default async function Page({ params }: ParamsProps) {
  try {
    const lang = await getLocale();
    const { recipe, reports } = await getRecipeReportById({
      recipeId: params.id,
      options: {
        lang
      }
    });

    return (
      <div className='flex flex-col gap-10'>
        <div className='flex gap-2'>
          <span className='text-gray-500'>Administer Reports</span>
          <ChevronRight className='text-black_white' />
          <Link href='/reports/recipes'>
            <span className='text-black_white'>Recipe</span>
          </Link>
          <ChevronRight className='text-black_white' />
          <span className='text-black_white'>{recipe.title}</span>
        </div>
        <div className='container grid gap-10 xl:grid-cols-[70%_30%] xl:gap-3'>
          <ReportList
            reports={reports}
            className='xl:col-start-2'
          />
          <RecipeDetail
            recipe={recipe}
            className='xl:col-start-1 xl:row-start-1'
          />
        </div>
      </div>
    );
  } catch (error) {
    console.log(error);
    return <div>Something went wrong. :(</div>;
  }
}
