import logging
from typing import TYPE_CHECKING, Any, NotRequired, TypedDict

from RedisManager import RedisManager

if TYPE_CHECKING:
    from pymongo.mongo_client import MongoClient as PyMongoClient

logger = logging.getLogger(__name__)

MONGODB_PING_TIMEOUT_MS = 2000


class HealthReport(TypedDict):
    status: str
    reason: NotRequired[str]


def check_liveness() -> HealthReport:
    """
    Confirms the Python process and event loop are responsive.
    """
    return {"status": "Healthy"}


def check_redis_readiness(redis_manager: RedisManager) -> HealthReport:
    """
    Validates Redis connectivity via ping.

    Args:
        redis_manager: Redis manager instance providing ping connectivity.

    Returns:
        HealthReport indicating "Healthy" or "Unhealthy" with an optional failure reason.
    """
    try:
        if not redis_manager.ping():
            logger.error("Redis ping returned False")
            return {"status": "Unhealthy", "reason": "Redis readiness check failed"}
    except Exception as e:
        logger.exception(f"Redis readiness check failed: {e}")  # noqa: TRY401
        return {"status": "Unhealthy", "reason": "Redis readiness check failed"}
    return {"status": "Healthy"}


def check_mongodb_readiness(
    mongo_client: "PyMongoClient[Any] | None",
) -> HealthReport:
    """
    Validates MongoDB connectivity via ping command.

    Args:
        mongo_client: PyMongo MongoClient instance.

    Returns:
        HealthReport indicating "Healthy" or "Unhealthy" with an optional failure reason.
    """
    if mongo_client is None:
        return {"status": "Unhealthy", "reason": "MongoDB client is not initialized"}

    try:
        mongo_client.admin.command("ping", maxTimeMS=MONGODB_PING_TIMEOUT_MS)
    except Exception as e:
        logger.exception(f"MongoDB readiness check failed: {e}")  # noqa: TRY401
        return {"status": "Unhealthy", "reason": "MongoDB readiness check failed"}

    return {"status": "Healthy"}


def check_models_readiness(
    convnext_model: Any,
    clip_model: Any,
    index: Any,
) -> HealthReport:
    """
    Validates in-memory ML models and FAISS index readiness.

    Args:
        convnext_model: Pre-trained ConvNeXt feature extraction model.
        clip_model: Pre-trained CLIP multimodal model.
        index: FAISS index containing pre-computed ingredient embeddings.

    Returns:
        HealthReport indicating "Healthy" or "Unhealthy" with an optional failure reason.
    """
    models_loaded = (
        convnext_model is not None
        and clip_model is not None
        and index is not None
        and getattr(index, "ntotal", 0) > 0
    )
    if not models_loaded:
        return {
            "status": "Unhealthy",
            "reason": "In-memory ML models or FAISS index not ready",
        }

    return {"status": "Healthy"}


def check_readiness(
    redis_manager: RedisManager,
    mongo_client: "PyMongoClient[Any] | None",
    convnext_model: Any,
    clip_model: Any,
    index: Any,
) -> HealthReport:
    """
    Deep readiness check validating Redis, MongoDB connectivity, and ML models.
    Fails fast and returns the specific failure reason upon the first issue.

    Args:
        redis_manager: Redis manager instance providing ping connectivity.
        mongo_client: PyMongo MongoClient instance.
        convnext_model: Pre-trained ConvNeXt feature extraction model.
        clip_model: Pre-trained CLIP multimodal model.
        index: FAISS index containing pre-computed ingredient embeddings.

    Returns:
        HealthReport indicating "Healthy" or "Unhealthy" with an optional failure reason.
    """
    redis_report = check_redis_readiness(redis_manager)
    if redis_report["status"] != "Healthy":
        return redis_report

    mongodb_report = check_mongodb_readiness(mongo_client)
    if mongodb_report["status"] != "Healthy":
        return mongodb_report

    models_report = check_models_readiness(convnext_model, clip_model, index)
    if models_report["status"] != "Healthy":
        return models_report

    return {"status": "Healthy"}
