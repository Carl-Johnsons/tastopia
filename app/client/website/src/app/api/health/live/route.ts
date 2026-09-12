import { NextResponse } from "next/server";
import { checkLiveness } from "@/lib/health";

export const dynamic = "force-dynamic";

export function GET() {
  const report = checkLiveness();

  return NextResponse.json(report, {
    status: 200,
    headers: {
      "Cache-Control": "no-store"
    }
  });
}


