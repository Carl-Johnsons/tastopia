import { NextResponse } from "next/server";
import { checkReadiness } from "@/lib/health";

export const dynamic = "force-dynamic";

export function GET() {
  const report = checkReadiness();
  const statusCode = report.status === "Healthy" ? 200 : 503;

  return NextResponse.json(report, {
    status: statusCode,
    headers: {
      "Cache-Control": "no-store"
    }
  });
}


