import { getRandomBytes } from "expo-crypto";

let isCiMode = false;
let sessionTraceId: string | null = null;
let testName: string | null = null;
let testEnv: string | null = null;
let testRunId: string | null = null;

function randomHex(bytes: number): string {
  const arr = getRandomBytes(bytes);
  return Array.from(arr, b => b.toString(16).padStart(2, "0")).join("");
}

export function generateTraceId(): string {
  return randomHex(16);
}

export function generateSpanId(): string {
  return randomHex(8);
}

export interface CiTelemetryOptions {
  traceId?: string;
  testName?: string;
  env?: string;
  runId?: string;
}

export function enableCiMode(options?: CiTelemetryOptions | string): string {
  isCiMode = true;

  if (typeof options === "string") {
    sessionTraceId = options || generateTraceId();
  } else {
    // If a new testName is passed or no sessionTraceId exists, refresh the session trace ID
    if (options?.testName && options.testName !== testName) {
      testName = options.testName;
      sessionTraceId = options.traceId ?? generateTraceId();
    } else {
      sessionTraceId = options?.traceId ?? sessionTraceId ?? generateTraceId();
      if (options?.testName) testName = options.testName;
    }
    if (options?.env) testEnv = options.env;
    if (options?.runId) testRunId = options.runId;
  }

  console.log(
    `[CI_TELEMETRY] CI mode enabled, sessionTraceId=${sessionTraceId} testName=${testName} env=${testEnv} runId=${testRunId}`
  );
  return sessionTraceId;
}

export function isCi(): boolean {
  return isCiMode;
}

export function getSessionTraceId(): string | null {
  return sessionTraceId;
}

export function getTestName(): string | null {
  return testName;
}

export function getTestEnv(): string | null {
  return testEnv;
}

export function getTestRunId(): string | null {
  return testRunId;
}

export function getBaggageHeader(): string | null {
  const items: string[] = [];
  if (testName) {
    items.push(`test.name=${encodeURIComponent(testName)}`);
  }
  if (testEnv) {
    items.push(`env=${encodeURIComponent(testEnv)}`);
  }
  if (testRunId) {
    items.push(`ci.run_id=${encodeURIComponent(testRunId)}`);
  }
  return items.length > 0 ? items.join(",") : null;
}

/**
 * Generates a W3C traceparent header:
 * version(00) - traceId(32 hex) - spanId(16 hex) - flags(01: sampled)
 *
 * In CI mode, reuses the sessionTraceId across requests.
 * In production mode, generates a fresh traceId per request.
 */
export function generateTraceparent(traceId?: string, spanId?: string) {
  if (!traceId) {
    if (isCiMode) {
      if (!sessionTraceId) {
        sessionTraceId = generateTraceId();
      }
      traceId = sessionTraceId;
    } else {
      traceId = generateTraceId();
    }
  }

  spanId = spanId ?? generateSpanId();

  return {
    traceId,
    spanId,
    traceparent: `00-${traceId}-${spanId}-01`
  };
}

/**
 * Helper to inject both traceparent and baggage headers into Axios config.headers
 */
export function setupTelemetryHeaders(headers: any) {
  const { traceparent, traceId, spanId } = generateTraceparent();
  headers.set("traceparent", traceparent);

  const baggage = getBaggageHeader();
  if (baggage) {
    headers.set("baggage", baggage);
  }

  return { traceparent, traceId, spanId, baggage };
}
