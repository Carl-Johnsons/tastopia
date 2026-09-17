from fastapi import FastAPI
from opentelemetry import trace
from opentelemetry.exporter.otlp.proto.grpc.trace_exporter import OTLPSpanExporter
from opentelemetry.instrumentation.aiohttp_client import AioHttpClientInstrumentor
from opentelemetry.instrumentation.fastapi import FastAPIInstrumentor
from opentelemetry.instrumentation.httpx import HTTPXClientInstrumentor
from opentelemetry.instrumentation.pymongo import PymongoInstrumentor
from opentelemetry.instrumentation.redis import RedisInstrumentor
from opentelemetry.instrumentation.requests import RequestsInstrumentor
from opentelemetry.sdk.resources import SERVICE_NAME, Resource
from opentelemetry.sdk.trace import TracerProvider
from opentelemetry.sdk.trace.export import BatchSpanProcessor

_tracer_provider: TracerProvider | None = None


def init(service_name: str):
    global _tracer_provider

    resource = Resource.create({SERVICE_NAME: service_name})
    _tracer_provider = TracerProvider(resource=resource)

    _configure_exporter(_tracer_provider)
    trace.set_tracer_provider(_tracer_provider)

    _configure_instrumentation()


def _configure_exporter(tracer_provider: TracerProvider):
    otlp_exporter = OTLPSpanExporter()
    processor = BatchSpanProcessor(otlp_exporter)
    tracer_provider.add_span_processor(processor)


def configure_fastapi(app: FastAPI):
    FastAPIInstrumentor.instrument_app(
        app, tracer_provider=_tracer_provider, excluded_urls=r".*health.*"
    )


def _configure_instrumentation():
    AioHttpClientInstrumentor().instrument()
    HTTPXClientInstrumentor().instrument()
    RequestsInstrumentor().instrument()
    PymongoInstrumentor().instrument(capture_statement=True)
    RedisInstrumentor().instrument()


def shutdown():
    """Flush and shut down span processors on application termination."""
    if _tracer_provider is not None:
        _tracer_provider.shutdown()
