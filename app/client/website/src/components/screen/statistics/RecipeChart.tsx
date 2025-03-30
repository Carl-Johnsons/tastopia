"use client";

import * as React from "react";
import { Area, AreaChart, CartesianGrid, XAxis, YAxis } from "recharts";

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
  ChartLegend,
  ChartLegendContent,
  ChartTooltip,
  ChartTooltipContent
} from "@/components/ui/chart";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue
} from "@/components/ui/select";
import { useState } from "react";
import { StatisticDateItem } from "@/types/statistic";
import { useLocale, useTranslations } from "next-intl";

const chartConfig = {
  recipes: {
    label: "Recipes"
  },
  recipe: {
    label: "Recipe",
    color: "hsl(var(--chart-1))"
  }
} satisfies ChartConfig;

type RecipeChartProps = {
  chartData: StatisticDateItem[];
};

export function RecipeChart({ chartData }: RecipeChartProps) {
  const currentLanguage = useLocale();
  const t = useTranslations("statistic.charts.recipeStatistic");
  const [timeRange, setTimeRange] = useState("90d");

  const filteredData = chartData.filter(item => {
    const date = new Date(item.date);
    const referenceDate = new Date().toISOString().split("T")[0];
    let daysToSubtract = 90;
    if (timeRange === "360d") {
      daysToSubtract = 360;
    } else if (timeRange === "90d") {
      daysToSubtract = 90;
    } else if (timeRange === "30d") {
      daysToSubtract = 30;
    } else if (timeRange === "7d") {
      daysToSubtract = 7;
    } else if (timeRange === "1d") {
      daysToSubtract = 1;
    }
    const startDate = new Date(referenceDate);
    startDate.setDate(startDate.getDate() - daysToSubtract);
    return date >= startDate;
  });

  return (
    <Card>
      <CardHeader className='flex items-center gap-2 space-y-0 border-b py-5 sm:flex-row'>
        <div className='grid flex-1 gap-1 text-center sm:text-left'>
          <CardTitle className='text-black_white'>{t("title")}</CardTitle>
          <CardDescription>{t("description")}</CardDescription>
        </div>
        <Select
          value={timeRange}
          onValueChange={setTimeRange}
        >
          <SelectTrigger
            className='text-black_white w-[160px] rounded-lg sm:ml-auto'
            aria-label='Select a value'
          >
            <SelectValue placeholder={t("filter.placeholder")} />
          </SelectTrigger>
          <SelectContent className='bg-white_black100 text-black_white rounded-xl'>
            <SelectItem
              value='1d'
              className='rounded-lg'
            >
              {t("filter.options.1d")}
            </SelectItem>
            <SelectItem
              value='7d'
              className='rounded-lg'
            >
              {t("filter.options.7d")}
            </SelectItem>
            <SelectItem
              value='30d'
              className='rounded-lg'
            >
              {t("filter.options.30d")}
            </SelectItem>
            <SelectItem
              value='90d'
              className='rounded-lg'
            >
              {t("filter.options.90d")}
            </SelectItem>
            <SelectItem
              value='360d'
              className='rounded-lg'
            >
              {t("filter.options.360d")}
            </SelectItem>
          </SelectContent>
        </Select>
      </CardHeader>
      <CardContent className='px-2 pt-4 sm:px-6 sm:pt-6'>
        <ChartContainer
          config={chartConfig}
          className='aspect-auto h-[250px] w-full'
        >
          <AreaChart data={filteredData}>
            <defs>
              <linearGradient
                id='fillRecipe'
                x1='0'
                y1='0'
                x2='0'
                y2='1'
              >
                <stop
                  offset='5%'
                  stopColor='var(--color-recipe)'
                  stopOpacity={0.8}
                />
                <stop
                  offset='95%'
                  stopColor='var(--color-recipe)'
                  stopOpacity={0.1}
                />
              </linearGradient>
            </defs>
            <CartesianGrid vertical={false} />
            <YAxis
              axisLine={false}
              tickLine={false}
              tickMargin={8}
              width={40}
            />
            <XAxis
              dataKey='date'
              tickLine={false}
              axisLine={false}
              tickMargin={8}
              minTickGap={32}
              tickFormatter={value => {
                const date = new Date(value);
                return date.toLocaleDateString(currentLanguage, {
                  month: "short",
                  day: "numeric"
                });
              }}
            />
            <ChartTooltip
              cursor={false}
              content={
                <ChartTooltipContent
                  className='text-black_white'
                  labelFormatter={value => {
                    return new Date(value).toLocaleDateString(currentLanguage, {
                      month: "short",
                      day: "numeric"
                    });
                  }}
                  customLabel={t("recipeCount")}
                  indicator='dot'
                />
              }
            />
            <Area
              dataKey='number'
              type='monotone'
              fill='url(#fillRecipe)'
              stroke='var(--color-recipe)'
              stackId='a'
            />
          </AreaChart>
        </ChartContainer>
      </CardContent>
    </Card>
  );
}
