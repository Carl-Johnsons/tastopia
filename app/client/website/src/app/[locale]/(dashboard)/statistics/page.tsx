import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import TotalRecipe from "@/components/screen/statistics/TotalRecipe";
import TotalOnlineUser from "@/components/screen/statistics/TotalOnlineUser";
import TotalUser from "@/components/screen/statistics/TotalUser";
import {
  getRecipeRanking,
  getRecipeStatistic,
  getTotalRecipes
} from "@/actions/recipe.action";
import { getAccountStatistic, getTotalUsers } from "@/actions/user.action";
import { getLocale, getTranslations } from "next-intl/server";
import { RecipeChart } from "@/components/screen/statistics/RecipeChart";
import { RecipeRanking } from "@/components/screen/statistics/RecipeRanking";
import { getTagRanking } from "@/actions/tag.action";
import { TagRanking } from "@/components/screen/statistics/TagRanking";
import { AccountChart } from "@/components/screen/statistics/AccountChart";
import { redirect } from "@/i18n/navigation";

export default async function System() {
  try {
    const t = await getTranslations("statistic");
    const totalRecipe = await getTotalRecipes();
    const totalUser = await getTotalUsers();
    const recipeRankingList = await getRecipeRanking();
    const tagRankingList = await getTagRanking();
    const recipeStatistic = await getRecipeStatistic();
    const accountStatistic = await getAccountStatistic();

    return (
      <div className='flex size-full flex-col justify-center gap-4'>
        {/* Overview */}
        <Card className='flex flex-col'>
          <CardHeader className='items-center pb-0'>
            <CardTitle className='h2-bold text-black_white mb-8'>
              {t("platformOverview.title")}
            </CardTitle>
          </CardHeader>
          <CardContent className='flex-center flex-wrap'>
            {totalRecipe !== 0 && <TotalRecipe totalRecipe={totalRecipe} />}
            <TotalOnlineUser />
            {totalUser !== 0 && <TotalUser totalUser={totalUser} />}
          </CardContent>
        </Card>

        {/* Chart */}
        <RecipeChart chartData={recipeStatistic} />
        <AccountChart chartData={accountStatistic} />

        {/* Ranking */}
        <RecipeRanking chartData={recipeRankingList} />
        <TagRanking chartData={tagRankingList} />
      </div>
    );
  } catch (error) {
    const locale = await getLocale();
    redirect({
      href: {
        pathname: "/auth"
      },
      locale
    });
  }
}
