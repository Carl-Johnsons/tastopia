import { v4 as uuidv4 } from "uuid";
import { DuendeISUser } from "next-auth/providers/duende-identity-server6";
import { OAuthUserConfig } from "next-auth/providers/oauth";

import { CLIENT_BASE_URL, DUENDE_IDS6_ISSUER, SCOPE } from "./api";

export const authOptions: OAuthUserConfig<DuendeISUser> = {
  clientId: process.env.DUENDE_IDS6_ID as string,
  clientSecret: "",
  issuer: DUENDE_IDS6_ISSUER,
  authorization: {
    params: {
      scope: SCOPE,
      nonce: uuidv4(),
      redirect_uri: `${CLIENT_BASE_URL}/api/auth/callback/duende-identityserver6`,
    },
  },
  checks: ["pkce", "nonce"],
  profile(profile) {
    console.log("Received profile:", profile);
    return {
      id: profile.sub,
      name: profile.name,
      email: profile.email,
    };
  },
};


