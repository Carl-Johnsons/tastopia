import { ResponseCookie } from "next/dist/compiled/@edge-runtime/cookies";
import { cookies } from "next/headers";

type AuthData = {
  accessToken: string;
  idToken: string;
};

export const setAuthCookies = async ({ accessToken, idToken }: AuthData) => {
  const cookieStore = cookies();
  const expiresAt = new Date(Date.now() + 7 * 24 * 60 * 60 * 1000);
  const config: Partial<ResponseCookie> = {
    httpOnly: true,
    secure: true,
    expires: expiresAt,
    sameSite: "lax",
    path: "/",
  };

  try {
    cookieStore.set("accessToken", accessToken, config);
    cookieStore.set("idToken", idToken, config);

    console.log("Cookies set success");
  } catch (error) {
    console.log("setAuthCookies: Error setting cookies", error);
  }
};

export const getAuthCookie = async (name: "accessToken" | "idToken") => {
  const cookieStore = cookies();
  const value = cookieStore.get(name);
  console.log("Cookie found result for", name, value);
  return value;
};

export const deleteAuthCookie = async (name: "accessToken" | "idToken") => {
  const cookieStore = cookies();

  try {
    cookieStore.delete(name);
  } catch (error) {
    console.log("Error deleting cookie", error);
  }
};

export const deleteAllAuthCookies = () => {
  deleteAuthCookie("accessToken");
  deleteAuthCookie("idToken");
};
