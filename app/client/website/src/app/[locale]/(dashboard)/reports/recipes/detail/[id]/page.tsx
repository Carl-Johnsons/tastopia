"use client";

import { useGetRecipeReport } from "@/api/recipe";
import Loading from "@/app/[locale]/(dashboard)/users/[id]/loading";
import ReportList from "@/components/screen/report/common/ReportList";
import RecipeDetail from "@/components/screen/report/recipe/RecipeDetail";
import SomethingWentWrong from "@/components/shared/common/Error";
import { ReportType } from "@/generated/enums/recipe.enum";
import { Link } from "@/i18n/navigation";
import { ParamsProps } from "@/types/link";
import { ChevronRight } from "lucide-react";
import { useLocale } from "next-intl";

export default function Page({ params }: ParamsProps) {
  const recipeId = params.id;
  const lang = useLocale();

  const { data, isError, isLoading } = useGetRecipeReport({
    recipeId: params.id,
    options: {
      lang
    }
  });

  if (isError) return <SomethingWentWrong />;
  if (isLoading || !data) return <Loading />;

  return (
    <div className='flex flex-col gap-10'>
      <div className='flex gap-2'>
        <span className='text-gray-500'>Administer Reports</span>
        <ChevronRight className='text-black_white' />
        <Link href='/reports/recipes'>
          <span className='text-black_white'>Recipe</span>
        </Link>
        <ChevronRight className='text-black_white' />
        <span className='text-black_white'>{data.recipe.title}</span>
      </div>
      <div className='container grid gap-10 xl:grid-cols-[70%_30%] xl:gap-3'>
        <ReportList
          reports={data.reports}
          targetId={recipeId}
          recipeId={recipeId}
          reportType={ReportType.RECIPE}
          className='xl:col-start-2'
        />
        <RecipeDetail
          recipe={data.recipe}
          className='xl:col-start-1 xl:row-start-1'
        />
      </div>
    </div>
  );
}
