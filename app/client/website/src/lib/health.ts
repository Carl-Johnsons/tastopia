export type HealthStatus = "Healthy" | "Unhealthy";

export interface HealthReport {
  status: HealthStatus;
  reason?: string;
}

let isShuttingDown = false;

if (typeof process !== "undefined" && process.on) {
  process.on("SIGTERM", () => {
    isShuttingDown = true;
  });
}

/**
 * Confirms Node.js process is active and responding.
 */
export function checkLiveness(): HealthReport {
  return { status: "Healthy" };
}

function checkShutdownState(): HealthReport | null {
  if (isShuttingDown) {
    return {
      status: "Unhealthy",
      reason: "Server is shutting down"
    };
  }
  return null;
}

function checkRuntimeConfig(): HealthReport | null {
  const apiGatewayHost = process.env.NEXT_PUBLIC_API_GATEWAY_HOST;
  const duendeIssuer = process.env.DUENDE_IDS6_ISSUER;

  if (!apiGatewayHost || !duendeIssuer) {
    return {
      status: "Unhealthy",
      reason: "Missing critical environment configuration"
    };
  }
  return null;
}

/**
 * Check if the service is ready to serve requests.
 */
export function checkReadiness(): HealthReport {
  const shutdownFailure = checkShutdownState();
  if (shutdownFailure) {
    return shutdownFailure;
  }

  const configFailure = checkRuntimeConfig();
  if (configFailure) {
    return configFailure;
  }

  return { status: "Healthy" };
}
