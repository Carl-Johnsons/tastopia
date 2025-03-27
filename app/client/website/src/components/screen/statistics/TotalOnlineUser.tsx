"use client";

import * as React from "react";
import { Label, Pie, PieChart } from "recharts";
import { ChartConfig, ChartContainer } from "@/components/ui/chart";
import { useSignalR } from "@/components/provider/SignalRProvider";
import { useTranslations } from "next-intl";

const chartConfig = {
  safari: {
    label: "Safari",
    color: "hsl(var(--chart-2))"
  }
} satisfies ChartConfig;

export default function TotalOnlineUser() {
  const t = useTranslations("statistic.platformOverview");
  const signalR = useSignalR();
  const chartData = [
    { name: "safari", value: signalR?.onlineUserCount, fill: "var(--color-safari)" }
  ];

  return (
    signalR?.onlineUserCount !== 0 && (
      <div className='flex-center mx-auto flex-col'>
        <h3 className='base-bold text-black_white'>
          {t("totalOnlineUsers", { count: signalR?.onlineUserCount ?? 0 })}
        </h3>

        <ChartContainer
          config={chartConfig}
          className='aspect-square h-[250px]'
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
                          {signalR?.onlineUserCount.toLocaleString()}
                        </tspan>
                        <tspan
                          x={viewBox.cx}
                          y={(viewBox.cy || 0) + 24}
                          className='fill-muted-foreground'
                        >
                          {t("onlineUsers", { count: signalR?.onlineUserCount ?? 0 })}
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
    )
  );
}
