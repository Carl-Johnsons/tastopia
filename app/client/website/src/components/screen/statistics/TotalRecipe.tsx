"use client";

import * as React from "react";
import { Label, Pie, PieChart } from "recharts";
import { ChartConfig, ChartContainer } from "@/components/ui/chart";
import { useTranslations } from "next-intl";

const chartConfig = {
  chrome: {
    label: "Recipe",
    color: "hsl(var(--chart-1))"
  }
} satisfies ChartConfig;

export default function TotalRecipe({ totalRecipe }: { totalRecipe: number }) {
  const t = useTranslations("statistic.platformOverview");
  const chartData = [{ name: "recipe", value: totalRecipe, fill: "var(--color-chrome)" }];
  return (
    <div className='flex-center mx-auto flex-col'>
      <h3 className='base-bold text-black_white'>
        {t("totalRecipes", { count: totalRecipe })}
      </h3>

      <ChartContainer
        config={chartConfig}
        className='mx-auto aspect-square h-[250px]'
      >
        <PieChart>
          <Pie
            data={chartData}
            dataKey='value'
            nameKey='name'
            innerRadius={60}
            strokeWidth={5}
          >
            <Label
              content={({ viewBox }) => {
                if (viewBox && "cx" in viewBox && "cy" in viewBox) {
                  return (
                    <text
                      x={viewBox.cx}
                      y={viewBox.cy}
                      textAnchor='middle'
                      dominantBaseline='middle'
                    >
                      <tspan
                        x={viewBox.cx}
                        y={viewBox.cy}
                        className='fill-foreground text-3xl font-bold'
                      >
                        {totalRecipe.toLocaleString()}
                      </tspan>
                      <tspan
                        x={viewBox.cx}
                        y={(viewBox.cy || 0) + 24}
                        className='fill-muted-foreground'
                      >
                        {t("recipes", { count: totalRecipe })}
                      </tspan>
                    </text>
                  );
                }
              }}
            />
          </Pie>
        </PieChart>
      </ChartContainer>
    </div>
  );
}
