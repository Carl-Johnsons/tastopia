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
import SomethingWentWrong from "@/components/shared/common/Error";
import { redirect } from "@/i18n/navigation";
import { AxiosError } from "axios";

export default async function System() {
  try {
    const t = await getTranslations("statistic");

    const totalRecipeResponse = await getTotalRecipes();
    const recipeRankingListResponse = await getRecipeRanking();
    const recipeStatisticResponse = await getRecipeStatistic();
    const totalUserResponse = await getTotalUsers();
    const tagRankingResponse = await getTagRanking();
    const accountStatisticResponse = await getAccountStatistic();

    if (
      !totalRecipeResponse.ok ||
      !recipeRankingListResponse.ok ||
      !recipeStatisticResponse.ok ||
      !totalUserResponse.ok ||
      !tagRankingResponse.ok ||
      !accountStatisticResponse.ok
    ) {
      throw new Error("Failed to fetch data");
    }

    const totalRecipe = totalRecipeResponse.data;
    const recipeRankingList = recipeRankingListResponse.data;
    const recipeStatistic = recipeStatisticResponse.data;
    const totalUser = totalUserResponse.data;
    const tagRankingList = tagRankingResponse.data;
    const accountStatistic = accountStatisticResponse.data;

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
    if (error instanceof AxiosError && error.status === 403) {
      const locale = await getLocale();
      redirect({
        href: {
          pathname: "/auth"
        },
        locale
      });
    }

    return <SomethingWentWrong />;
  }
}
