import logging

class HealthFilter(logging.Filter):
    def filter(self, record):
        # Suppress log messages that contain "/health"
        return "/health" not in record.getMessage()