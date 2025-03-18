import StackedAreaChart from "@/components/screen/statistics/StackedAreaChart";
import BarChart from "@/components/screen/statistics/BarChart";
import OnlineUserChart from "@/components/screen/statistics/OnlineUserChart";

export default function System() {
  return (
    <div className='flex size-full flex-col justify-center gap-4'>
      <p>System</p>
      <OnlineUserChart />
      <StackedAreaChart />
      <BarChart />
    </div>
  );
}
