import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle
} from "@/components/ui/card";
import { TrendingUp } from "lucide-react";
import StackedAreaChart from "@/components/screen/statistics/StackedAreaChart";
import BarChart from "@/components/screen/statistics/BarChart";
import OnlineUserChart from "@/components/screen/statistics/OnlineUserChart";
import TotalRecipe from "@/components/screen/statistics/TotalRecipe";
import TotalOnlineUser from "@/components/screen/statistics/TotalOnlineUser";
import TotalUser from "@/components/screen/statistics/TotalUser";

export default function System() {
  return (
    <div className='flex size-full flex-col justify-center gap-4'>
      <Card className='flex flex-col'>
        <CardHeader className='items-center pb-0'>
          <CardTitle className='h2-bold text-black_white mb-8'>
            System information
          </CardTitle>
        </CardHeader>
        <CardContent className='flex-center'>
          <TotalRecipe />
          <TotalOnlineUser />
          <TotalUser />
        </CardContent>
      </Card>
      <OnlineUserChart />
      <StackedAreaChart />
      <BarChart />
    </div>
  );
}
