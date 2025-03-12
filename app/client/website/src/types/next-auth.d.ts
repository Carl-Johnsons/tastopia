import NextAuth from "next-auth";

declare module "next-auth" {
  interface Session {
    idToken: string;
    accessToken: string;
  }
}
