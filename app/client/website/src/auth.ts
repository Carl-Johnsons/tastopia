import DuendeIDS6Provider from "next-auth/providers/duende-identity-server6";
import NextAuth from "next-auth";
import {
  CLIENT_BASE_URL,
  DUENDE_IDS6_ID,
  DUENDE_IDS6_ISSUER,
  SCOPE
} from "./constants/api";
import { v4 as uuidv4 } from "uuid";

export const { handlers, signIn, signOut, auth } = NextAuth({
  providers: [
    DuendeIDS6Provider({
      clientId: DUENDE_IDS6_ID as string,
      clientSecret: "",
      issuer: `${DUENDE_IDS6_ISSUER}`,
      wellKnown: `${DUENDE_IDS6_ISSUER}/.well-known/openid-configuration`,
      token: {
        url: `${DUENDE_IDS6_ISSUER}/connect/token`
      },
      authorization: {
        url: `${DUENDE_IDS6_ISSUER}/connect/authorize`,
        params: {
          scope: SCOPE,
          nonce: uuidv4(),
          redirect_uri: `${CLIENT_BASE_URL}/api/auth/callback/duende-identity-server6`
        }
      },
      checks: ["pkce", "nonce"],
      profile(profile) {
        console.log("Received profile:", profile);
        return {
          id: profile.sub,
          name: profile.name,
          email: profile.email
        };
      }
    })
  ],
  callbacks: {
    async jwt({ token, user, account }) {
      if (account && user) {
        token.idToken = account.id_token;
        token.accessToken = account.access_token;
      }

      return token;
    },
    async session({ session, token }) {
      session.accessToken = token.accessToken as string;
      session.idToken = token.idToken as string;

      return session;
    }
  },
  pages: {
    error: "/en/",
    signIn: "/en/",
    signOut: "/en/"
  }
});
