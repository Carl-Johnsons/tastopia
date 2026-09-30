function randomHex(bytes: number): string {
  const arr = new Uint8Array(bytes);
  crypto.getRandomValues(arr);
  return Array.from(arr, b => b.toString(16).padStart(2, "0")).join("");
}

export function generateTraceId() {
  return randomHex(16);
}

export function generateSpanId() {
  return randomHex(8);
}

/**
 * Generates a W3C traceparent header:
 * version(00) - traceId(32 hex) - spanId(16 hex) - flags(01: sampled)
 */
export function generateTraceparent(traceId?: string, spanId?: string) {
  traceId = traceId ?? generateTraceId();
  spanId = spanId ?? generateSpanId();
  return {
    traceId,
    traceparent: `00-${traceId}-${spanId}-01`
  };
}

/**
 * Generates a W3C baggage header string with test metadata:
 * test.name=<testName>,env=<env>,ci.run_id=<runId>
 */
export function generateBaggage(
  testName?: string,
  env?: string,
  runId?: string
): string | null {
  const items: string[] = [];
  if (testName) items.push(`test.name=${encodeURIComponent(testName)}`);
  if (env) items.push(`env=${encodeURIComponent(env)}`);
  if (runId) items.push(`ci.run_id=${encodeURIComponent(runId)}`);
  return items.length > 0 ? items.join(",") : null;
}
