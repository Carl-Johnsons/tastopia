"use client";

import * as React from "react";
import { Label, Pie, PieChart } from "recharts";
import { ChartConfig, ChartContainer } from "@/components/ui/chart";
const chartData = [{ browser: "chrome", visitors: 275, fill: "var(--color-chrome)" }];

const chartConfig = {
  visitors: {
    label: "Visitors"
  },
  chrome: {
    label: "Chrome",
    color: "hsl(var(--chart-1))"
  },
  safari: {
    label: "Safari",
    color: "hsl(var(--chart-2))"
  },
  firefox: {
    label: "Firefox",
    color: "hsl(var(--chart-3))"
  },
  edge: {
    label: "Edge",
    color: "hsl(var(--chart-4))"
  },
  other: {
    label: "Other",
    color: "hsl(var(--chart-5))"
  }
} satisfies ChartConfig;

export default function TotalRecipe() {
  const totalVisitors = React.useMemo(() => {
    return chartData.reduce((acc, curr) => acc + curr.visitors, 0);
  }, []);

  return (
    <div className='flex-center mx-auto flex-col'>
      <h3 className='base-bold text-black_white'>Total Recipe</h3>

      <ChartContainer
        config={chartConfig}
        className='mx-auto aspect-square h-[250px]'
      >
        <PieChart>
          <Pie
            data={chartData}
            dataKey='visitors'
            nameKey='browser'
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
                        {totalVisitors.toLocaleString()}
                      </tspan>
                      <tspan
                        x={viewBox.cx}
                        y={(viewBox.cy || 0) + 24}
                        className='fill-muted-foreground'
                      >
                        Recipes
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
