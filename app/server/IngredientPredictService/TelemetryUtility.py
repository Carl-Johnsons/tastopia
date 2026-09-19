import logging
import os

from fastapi import FastAPI
from opentelemetry import trace
from opentelemetry._logs import set_logger_provider
from opentelemetry.exporter.otlp.proto.grpc._log_exporter import OTLPLogExporter
from opentelemetry.exporter.otlp.proto.grpc.trace_exporter import OTLPSpanExporter
from opentelemetry.instrumentation.aiohttp_client import AioHttpClientInstrumentor
from opentelemetry.instrumentation.fastapi import FastAPIInstrumentor
from opentelemetry.instrumentation.httpx import HTTPXClientInstrumentor
from opentelemetry.instrumentation.pymongo import PymongoInstrumentor
from opentelemetry.instrumentation.redis import RedisInstrumentor
from opentelemetry.instrumentation.requests import RequestsInstrumentor
from opentelemetry.sdk._logs import LoggerProvider, LoggingHandler
from opentelemetry.sdk._logs.export import BatchLogRecordProcessor
from opentelemetry.sdk.resources import SERVICE_NAME, Resource
from opentelemetry.sdk.trace import TracerProvider
from opentelemetry.sdk.trace.export import BatchSpanProcessor

_tracer_provider: TracerProvider | None = None
_logger_provider: LoggerProvider | None = None


def init(service_name: str) -> None:
    """Initialize OpenTelemetry distributed tracing and logging."""
    global _tracer_provider, _logger_provider

    resource = Resource.create({SERVICE_NAME: service_name})

    _tracer_provider = _create_tracer_provider(resource)
    trace.set_tracer_provider(_tracer_provider)
    _configure_instrumentation()

    _logger_provider = _create_logger_provider(resource)
    set_logger_provider(_logger_provider)
    _configure_logging_handler(_logger_provider)


def _create_tracer_provider(resource: Resource) -> TracerProvider:
    provider = TracerProvider(resource=resource)
    otlp_span_exporter = OTLPSpanExporter()
    trace_processor = BatchSpanProcessor(otlp_span_exporter)
    provider.add_span_processor(trace_processor)
    return provider


def _create_logger_provider(resource: Resource) -> LoggerProvider:
    provider = LoggerProvider(resource=resource)
    otlp_log_exporter = OTLPLogExporter()
    log_processor = BatchLogRecordProcessor(otlp_log_exporter)
    provider.add_log_record_processor(log_processor)
    return provider


def configure_fastapi(app: FastAPI) -> None:
    FastAPIInstrumentor.instrument_app(
        app, tracer_provider=_tracer_provider, excluded_urls=r".*health.*"
    )


def _configure_instrumentation() -> None:
    AioHttpClientInstrumentor().instrument()
    HTTPXClientInstrumentor().instrument()
    RequestsInstrumentor().instrument()
    PymongoInstrumentor().instrument(capture_statement=True)
    RedisInstrumentor().instrument()


def _configure_logging_handler(logger_provider: LoggerProvider) -> None:
    otel_log_level_name = os.getenv("OTEL_LOG_LEVEL", "INFO").upper()
    otel_log_level = getattr(logging, otel_log_level_name, logging.INFO)

    root_logger = logging.getLogger()
    root_logger.setLevel(otel_log_level)

    handler = LoggingHandler(level=otel_log_level, logger_provider=logger_provider)
    root_logger.addHandler(handler)


def shutdown() -> None:
    """Flush and shut down span and log processors on application termination."""
    if _tracer_provider is not None:
        _tracer_provider.shutdown()

    if _logger_provider is not None:
        _logger_provider.shutdown()
