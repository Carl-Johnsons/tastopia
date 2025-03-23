import { getAuthCookie } from "@/utils/auth";
import { NextRequest, NextResponse } from "next/server";

export async function GET() {
  try {
    const accessToken = getAuthCookie("accessToken");
    const idToken = getAuthCookie("idToken");

    if (!accessToken || !idToken) {
      return Response.json(
        {
          accessToken,
          idToken,
        },
        { status: 404 },
      );
    }

    return Response.json({ accessToken, idToken }, { status: 204 });
  } catch (error) {
    console.log("Error getting cookie", error);
    return Response.json("Internal server error", { status: 500 });
  }
}

export async function POST(req: NextRequest) {
  try {
    const body = await req.json();

    if (!body) {
      return Response.json("Missing body", { status: 400 });
    }

    const { accessToken, idToken } = body;

    if (!accessToken || !idToken) {
      return Response.json("Missing access token or id token", { status: 400 });
    }

    const res = NextResponse.json(null, {
      status: 200,
    });

    res.cookies.set("accessToken", accessToken);
    res.cookies.set("idToken", idToken);

    console.log("Cookies set success");

    return res;
  } catch (error) {
    console.log("Error setting cookie", error);
    return Response.json("Internal server error", { status: 500 });
  }
}
