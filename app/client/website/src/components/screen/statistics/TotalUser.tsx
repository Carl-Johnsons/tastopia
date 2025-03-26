"use client";

import * as React from "react";
import { Label, Pie, PieChart } from "recharts";
import { ChartConfig, ChartContainer } from "@/components/ui/chart";
import { useTranslations } from "next-intl";

const chartConfig = {
  edge: {
    label: "Edge",
    color: "hsl(var(--chart-4))"
  }
} satisfies ChartConfig;

export default function TotalUser({ totalUser }: { totalUser: number }) {
  const t = useTranslations("statistic.platformOverview");
  const chartData = [{ name: "user", value: totalUser, fill: "var(--color-edge)" }];

  return (
    <div className='flex-center mx-auto flex-col'>
      <h3 className='base-bold text-black_white'>
        {t("totalUsers", { count: totalUser ?? 0 })}
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
                        {totalUser.toLocaleString()}
                      </tspan>
                      <tspan
                        x={viewBox.cx}
                        y={(viewBox.cy || 0) + 24}
                        className='fill-muted-foreground'
                      >
                        {t("users", { count: totalUser ?? 0 })}
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
