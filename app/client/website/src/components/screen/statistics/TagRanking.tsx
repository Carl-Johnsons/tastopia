"use client";

import { Bar, BarChart, LabelList, XAxis, YAxis } from "recharts";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle
} from "@/components/ui/card";
import {
  ChartConfig,
  ChartContainer,
  ChartTooltip,
  ChartTooltipContent
} from "@/components/ui/chart";
import { StatisticItem } from "@/types/statistic";
import { chartColors } from "@/constants/colors";
import { useTranslations } from "next-intl";

const chartConfig = {
  number: {
    label: "View",
    color: "hsl(var(--chart-1))"
  },
  label: {
    color: "hsl(var(--background))"
  }
} satisfies ChartConfig;

type TagRankingProps = {
  chartData: StatisticItem[];
};

export function TagRanking({ chartData }: TagRankingProps) {
  const t = useTranslations("statistic.ranking");
  const coloredChartData = chartData.map((item, index) => ({
    ...item,
    fill: chartColors[index % chartColors.length]
  }));

  return (
    <Card>
      <CardHeader>
        <CardTitle className='text-black_white'>{t("tagRanking")}</CardTitle>
        <CardDescription>{t("tagRankingDescription")}</CardDescription>
      </CardHeader>
      <CardContent>
        <ChartContainer config={chartConfig}>
          <BarChart
            accessibilityLayer
            data={coloredChartData}
            layout='vertical'
            margin={{
              right: 80
            }}
          >
            <YAxis
              dataKey='title'
              type='category'
              tickLine={false}
              tickMargin={10}
              axisLine={false}
              tickFormatter={value => value.slice(0, 3)}
              hide
            />
            <XAxis
              dataKey='number'
              type='number'
              hide
            />
            <ChartTooltip
              cursor={false}
              content={
                <ChartTooltipContent
                  className='text-black_white'
                  indicator='line'
                  customLabel={t("viewCount")}
                />
              }
            />
            <Bar
              dataKey='number'
              layout='vertical'
              fill='var(--color-number)'
              radius={4}
            >
              <LabelList
                dataKey='title'
                position='insideLeft'
                offset={8}
                className='fill-[--color-label]'
                fontSize={12}
              />
              <LabelList
                dataKey='number'
                position='right'
                offset={8}
                className='fill-foreground'
                fontSize={12}
              />
            </Bar>
          </BarChart>
        </ChartContainer>
      </CardContent>
    </Card>
  );
}
