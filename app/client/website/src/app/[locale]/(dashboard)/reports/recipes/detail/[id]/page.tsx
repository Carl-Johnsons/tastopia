"use client";

import { useGetRecipeReport } from "@/api/recipe";
import RecipeDetail from "@/components/screen/report/recipe/RecipeDetail";
import ReportList from "@/components/screen/report/recipe/ReportList";
import SomethingWentWrong from "@/components/shared/common/Error";
import Loader from "@/components/ui/Loader";
import { Link } from "@/i18n/navigation";
import { ParamsProps } from "@/types/link";
import { ChevronRight } from "lucide-react";
import { useLocale } from "next-intl";
import { useEffect } from "react";

export default function Page({ params }: ParamsProps) {
  const recipeId = params.id;
  const lang = useLocale();

  const { data, isError, isLoading } = useGetRecipeReport({
    recipeId: params.id,
    options: {
      lang
    }
  });

  useEffect(() => {
    console.log("data", data);
  }, [data]);

  if (isError) return <SomethingWentWrong />;
  if (isLoading || !data) return <Loader />;

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
          recipeId={recipeId}
          reports={data.reports}
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
