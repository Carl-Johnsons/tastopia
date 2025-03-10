import DuendeIDS6Provider from "next-auth/providers/duende-identity-server6";
import NextAuth from "next-auth";
import { authOptions } from "@/constants/authOptions";

const handler = NextAuth({
  providers: [DuendeIDS6Provider(authOptions)],
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
    },
  },
});

export { handler as GET, handler as POST };
