"use client";

import { useGetRecipeReport } from "@/api/recipe";
import RecipeDetail from "@/components/screen/report/recipe/RecipeDetail";
import ReportList from "@/components/screen/report/recipe/ReportList";
import SomethingWentWrong from "@/components/shared/common/Error";
import { Link } from "@/i18n/navigation";
import { ParamsProps } from "@/types/link";
import { ChevronRight } from "lucide-react";
import { useEffect } from "react";
import Loader from "@/components/ui/Loader";

export default function Page({ params }: ParamsProps) {
  console.log("Params", params);
  const { data, isError, isLoading } = useGetRecipeReport({
    recipeId: params.id,
    options: {
      lang: "en"
    }
  });

  useEffect(() => {
    console.log("data", data);
  }, [data]);

  if (isError) return <SomethingWentWrong />;
  if (isLoading || !data) return <Loader />;
  const { recipe, reports } = data;

  return (
    <div className='flex flex-col gap-10'>
      <div className='flex gap-2'>
        <Link href='/recipes'>
          <span className='text-gray-500'>Administer Recipes</span>
        </Link>
        <ChevronRight className='text-black_white' />
        <span className='text-black_white'>{recipe.title}</span>
      </div>
      <div
        className={`container grid gap-10 ${reports.length > 0 ? "xl:grid-cols-[70%_30%] xl:gap-3" : ""}`}
      >
        {reports.length > 0 && (
          <ReportList
            recipeId={recipe.id}
            reports={reports}
            className='xl:col-start-2'
          />
        )}
        <RecipeDetail
          recipe={recipe}
          className='xl:col-start-1 xl:row-start-1'
        />
      </div>
    </div>
  );
}
