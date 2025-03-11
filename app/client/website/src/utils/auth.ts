import { getCookie, OptionsType, setCookie } from "cookies-next/client";
import { cookies } from "next/headers";

type AuthData = {
  accessToken: string;
  idToken: string;
};

export const setAuthCookies = async ({ accessToken, idToken }: AuthData) => {
  const expiresAt = new Date(Date.now() + 7 * 24 * 60 * 60 * 1000);
  const config: Partial<OptionsType> = {
    httpOnly: true,
    secure: true,
    expires: expiresAt,
    sameSite: "lax",
    path: "/",
  };

  try {
    await setCookie("accessToken", accessToken, config);
    await setCookie("idToken", idToken, config);

    console.log("Cookies set success");
  } catch (error) {
    console.log("setAuthCookies: Error setting cookies", error);
  }
};

export const getAuthCookie = async (name: "accessToken" | "idToken") => {
  const value = await getCookie(name);
  console.log("Cookie found result for", name, value);
  return value;
};

export const deleteAuthCookie = async (name: "accessToken" | "idToken") => {
  const cookieStore = await cookies();

  try {
    cookieStore.delete(name);
  } catch (error) {
    console.log("Error deleting cookie", error);
  }
};
