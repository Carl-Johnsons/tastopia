#!/usr/bin/env python3

import argparse
import json
import os
import subprocess
from dataclasses import dataclass, field
from enum import StrEnum
from functools import cache

_ENV = os.environ.get("ENV")
_PR_NUMBER = os.environ.get("PR_NUMBER")


class ServiceName(StrEnum):
    WEBSITE = "website"
    MOBILE = "mobile"
    API_GATEWAY = "api-gateway"
    SIGNALR = "signalr"
    TRACKING_API = "tracking-api"
    UPLOAD_API = "upload-api"
    IDENTITY_API = "identity-api"
    NOTIFICATION_API = "notification-api"
    RECIPE_API = "recipe-api"
    USER_API = "user-api"
    INGREDIENT_PREDICT_API = "ingredient-predict-api"
    EMAIL_WORKER = "email-worker"
    SMS_WORKER = "sms-worker"
    PUSH_WORKER = "push-notification-worker"
    RECIPE_WORKER = "recipe-worker"
    CONTRACT = "contract"


@dataclass(frozen=True)
class Service:
    name: ServiceName
    paths: tuple[str, ...]
    dependencies: tuple["Service", ...] = field(default_factory=tuple)
    exclude_paths: tuple[str, ...] = field(default_factory=tuple)
    isClient: bool = False


contract = Service(ServiceName.CONTRACT, ("app/server/Contract",))
website = Service(
    ServiceName.WEBSITE,
    ("app/client/website",),
    exclude_paths=(
        "app/client/website/cypress/**",
        "app/client/website/cypress.config.ts",
    ),
    isClient=True,
)
mobile = Service(
    ServiceName.MOBILE,
    ("app/client/mobile",),
    exclude_paths=("app/client/mobile/.maestro",),
    isClient=True,
)
api_gateway = Service(ServiceName.API_GATEWAY, ("app/server/APIGateway",), (contract,))
signalr = Service(ServiceName.SIGNALR, ("app/server/SignalRService",), (contract,))
tracking_api = Service(
    ServiceName.TRACKING_API, ("app/server/TrackingService",), (contract,)
)
upload_api = Service(
    ServiceName.UPLOAD_API, ("app/server/UploadFileService",), (contract,)
)
identity_api = Service(
    ServiceName.IDENTITY_API, ("app/server/IdentityService",), (contract,)
)
notification_api = Service(
    ServiceName.NOTIFICATION_API,
    (
        "app/server/NotificationService/src/NotificationService.API",
        "app/server/NotificationService/src/NotificationService.Application",
        "app/server/NotificationService/src/NotificationService.Domain",
        "app/server/NotificationService/src/NotificationService.Infrastructure",
    ),
    (contract,),
)
recipe_api = Service(
    ServiceName.RECIPE_API,
    (
        "app/server/RecipeService/src/RecipeService.API",
        "app/server/RecipeService/src/RecipeService.Application",
        "app/server/RecipeService/src/RecipeService.Domain",
        "app/server/RecipeService/src/RecipeService.Infrastructure",
    ),
    (contract,),
)
user_api = Service(ServiceName.USER_API, ("app/server/UserService",), (contract,))
ingredient_predict_api = Service(
    ServiceName.INGREDIENT_PREDICT_API,
    ("app/server/IngredientPredictService",),
)
email_worker = Service(
    ServiceName.EMAIL_WORKER,
    ("app/server/NotificationService/src/EmailWorker",),
    (contract,),
)
sms_worker = Service(
    ServiceName.SMS_WORKER,
    ("app/server/NotificationService/src/SMSWorker",),
    (contract,),
)
push_worker = Service(
    ServiceName.PUSH_WORKER,
    ("app/server/NotificationService/src/PushNotificationWorker",),
    (contract,),
)
recipe_worker = Service(
    ServiceName.RECIPE_WORKER,
    ("app/server/RecipeService/src/RecipeWorker",),
    (contract,),
)

all_services: tuple[Service, ...] = (
    website,
    mobile,
    api_gateway,
    signalr,
    tracking_api,
    upload_api,
    identity_api,
    notification_api,
    recipe_api,
    user_api,
    ingredient_predict_api,
    email_worker,
    sms_worker,
    push_worker,
    recipe_worker,
)


@cache
def get_paths(service: Service) -> frozenset[str]:
    paths = set(service.paths)

    for dependency in service.dependencies:
        paths.update(get_paths(dependency))

    return frozenset(paths)


@cache
def get_exclude_paths(service: Service) -> frozenset[str]:
    exclude_paths = set(service.exclude_paths)

    for dependency in service.dependencies:
        exclude_paths.update(get_exclude_paths(dependency))

    return frozenset(exclude_paths)


def _get_latest_service_tag(service: Service, tag_length: int = 8) -> str:
    """
    Returns the latest tag based on the current state of the Git commit tree.

    Args:
        service - The targeted service
        tag_length - The length of the tag result
    """

    paths = get_paths(service)
    exclude_paths = [f":(exclude,glob){p}" for p in get_exclude_paths(service)]
    latest_sha = subprocess.run(
        ["git", "log", "-1", "--pretty=format:%H", "--", *paths, *exclude_paths],
        capture_output=True,
        check=True,
        text=True,
    ).stdout[:tag_length]

    return latest_sha


def get_tag(service: Service, tag_length: int = 8) -> str:
    """
    Returns the latest tag based on the current state of the Git commit tree and
    the current build environment.

    Args:
        service - The targeted service
        tag_length - The length of the tag result
    """
    tag = ""

    if not service.isClient:
        return _get_latest_service_tag(service, tag_length)

    if not (_ENV or _PR_NUMBER):
        raise RuntimeError(f"""
        Either ENV or PR_NUMBER has to be specified to get tag of {service.name.name} service
                           """)

    if (not _ENV or _ENV == "dev") and not _PR_NUMBER:
        raise RuntimeError(
            f"PR_NUMBER has to be specified to get a unique dev tag for {service.name.value}"
        )

    if _ENV:
        tag += f"{_ENV}-"
    elif _PR_NUMBER:
        tag += "dev-"

    if _PR_NUMBER and (not _ENV or _ENV == "dev"):
        tag += f"{_PR_NUMBER}-"

    tag += _get_latest_service_tag(service, tag_length)

    return tag


def get_service_by_name(service_name: ServiceName) -> Service:
    return next(s for s in all_services if s.name == service_name)


def is_valid_git_ref(ref: str) -> bool:
    if not ref or all(c == "0" for c in ref):
        return False

    res = subprocess.run(
        ["git", "cat-file", "-e", f"{ref}^{{commit}}"],
        capture_output=True,
        check=False,
    )

    return res.returncode == 0


def get_base_ref() -> str | None:
    event_name = os.environ.get("GITHUB_EVENT_NAME")

    if not event_name:
        return "HEAD~1"

    if event_name == "workflow_dispatch":
        return None

    if event_name == "pull_request":
        base_ref = os.environ.get("GITHUB_BASE_REF")
        return f"origin/{base_ref}" if base_ref else None

    event_path = os.environ.get("GITHUB_EVENT_PATH")

    if event_name == "push" and event_path and os.path.exists(event_path):
        with open(event_path, "r", encoding="utf-8") as f:
            payload = json.load(f)
            before = payload.get("before")

            if before and is_valid_git_ref(before):
                return before

    return "HEAD~1"


def is_service_changed(service: Service, base_ref: str | None = None) -> bool:
    if base_ref is None:
        return True

    paths = get_paths(service)
    exclude_paths = [f":(exclude,glob){p}" for p in get_exclude_paths(service)]
    try:
        proc = subprocess.run(
            [
                "git",
                "diff",
                "--name-only",
                f"{base_ref}...HEAD",
                "--",
                *paths,
                *exclude_paths,
            ],
            capture_output=True,
            check=True,
            text=True,
        )
        return bool(proc.stdout.strip())
    except subprocess.CalledProcessError:
        return True


def TagLength(val: str) -> int:
    parsed_val = int(val)

    if not 8 <= parsed_val <= 40:
        raise argparse.ArgumentTypeError(
            "Tag length must be between 8 and 40 (inclusive)"
        )

    return parsed_val


class Args(argparse.Namespace):
    services: list[ServiceName]
    tag_length: int
    check_list: list[ServiceName] | None
    base_ref: str | None


def parse_args() -> Args:
    parser = argparse.ArgumentParser(
        prog="derive-service-state.py",
        description="A small utility that reads Git commit history and give the right tags for each service.",
    )

    parser.add_argument(
        "services",
        nargs="*",
        type=ServiceName,
        help="The list of services to check, separated by a space",
    )
    parser.add_argument(
        "-t",
        "--tag-length",
        type=TagLength,
        default=8,
        help="Trim the length of each tag to the provided integer. The value must be bettween 8 and 40 (inclusive)",
    )
    parser.add_argument(
        "--check-changed",
        dest="check_list",
        metavar="SERVICE",
        nargs="+",
        type=ServiceName,
        default=None,
        help="Check if one or more services changed compared to base ref",
    )
    parser.add_argument(
        "--base-ref",
        type=str,
        default=None,
        help="Explicit base git ref to diff against",
    )

    return parser.parse_args(namespace=Args())


def main():
    args = parse_args()

    if args.check_list is not None:
        base_ref = args.base_ref if args.base_ref else get_base_ref()
        changed_services = [
            svc.name
            for svc in [get_service_by_name(name) for name in args.check_list]
            if is_service_changed(svc, base_ref)
        ]

        if len(args.check_list) == 1:
            print("true" if changed_services else "false")
        else:
            print(" ".join(changed_services))
        return

    result: list[str] = []
    services = [get_service_by_name(name) for name in args.services]

    if not services:
        services = all_services

    for service in services:
        tag = get_tag(service=service, tag_length=args.tag_length)
        if not tag:
            continue

        result.append(f"{service.name}:{tag}")

    print(" ".join(result))


if __name__ == "__main__":
    main()
