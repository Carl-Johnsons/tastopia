export const SCOPE =
  "openid profile phone email offline_access IdentityServerApi";

export const CLIENT_BASE_URL = process.env.CLIENT_BASE_URL;

export const DUENDE_IDS6_ISSUER = process.env.DUENDE_IDS6_ISSUER;
export const DUENDE_IDENTITY_PROVIDER_NAME = "duende-identity-server6";
export const DUENDE_IDS6_ID = process.env.DUENDE_IDS6_ID;

export const API_GATEWAY_SCHEME = process.env.API_GATEWAY_SCHEME;
export const API_GATEWAY_HOST = process.env.API_GATEWAY_HOST;
export const API_GATEWAY_PORT = process.env.API_GATEWAY_PORT;

export const API_URI = `${API_GATEWAY_SCHEME}://${API_GATEWAY_HOST}:${API_GATEWAY_PORT}`;
