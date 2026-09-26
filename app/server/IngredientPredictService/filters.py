import logging


class HealthFilter(logging.Filter):
    def filter(self, record: logging.LogRecord) -> bool:
        message = record.getMessage()
        return not any(
            probe in message for probe in ("/health/live", "/health/ready", "/health")
        )
