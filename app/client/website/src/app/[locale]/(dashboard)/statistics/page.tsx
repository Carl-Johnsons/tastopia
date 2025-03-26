import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import StackedAreaChart from "@/components/screen/statistics/StackedAreaChart";
import BarChart from "@/components/screen/statistics/BarChart";
import OnlineUserChart from "@/components/screen/statistics/OnlineUserChart";
import TotalRecipe from "@/components/screen/statistics/TotalRecipe";
import TotalOnlineUser from "@/components/screen/statistics/TotalOnlineUser";
import TotalUser from "@/components/screen/statistics/TotalUser";
import { getTotalRecipes } from "@/actions/recipe.action";
import { getTotalUsers } from "@/actions/user.action";
import { getTranslations } from "next-intl/server";

export default async function System() {
  const t = await getTranslations("statistic");
  const totalRecipe = await getTotalRecipes();
  const totalUser = await getTotalUsers();

  return (
    <div className='flex size-full flex-col justify-center gap-4'>
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
      <OnlineUserChart />
      <StackedAreaChart />
      <BarChart />
    </div>
  );
}
