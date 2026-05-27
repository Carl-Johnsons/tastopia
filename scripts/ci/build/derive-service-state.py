#!/usr/bin/env python3

import argparse
import os
import subprocess
from dataclasses import dataclass, field
from enum import StrEnum
from functools import cache


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


_REPO_ROOT = subprocess.run(
    ["git", "rev-parse", "--show-toplevel"],
    capture_output=True,
    check=True,
    text=True,
).stdout.strip()


@dataclass(frozen=True)
class Service:
    name: ServiceName
    paths: tuple[str, ...]
    dependencies: tuple["Service", ...] = field(default_factory=tuple)
    exclude_paths: tuple[str, ...] = field(default_factory=tuple)

    def __post_init__(self):
        for p in [*self.paths, *self.exclude_paths]:
            if not p.endswith("/") and os.path.isdir(os.path.join(_REPO_ROOT, p)):
                raise ValueError(
                    f"[{self.name}] path '{p}' is a directory but has no "
                    "trailing '/'. Add a trailing slash to ensure that there "
                    "is no ambiguous path."
                )


contract = Service(ServiceName.CONTRACT, ("app/server/Contract/",))
website = Service(
    ServiceName.WEBSITE,
    ("app/client/website/",),
    exclude_paths=(
        "app/client/website/cypress/",
        "app/client/website/cypress.config.ts",
    ),
)
mobile = Service(
    ServiceName.MOBILE,
    ("app/client/mobile/",),
    exclude_paths=("app/client/mobile/.maestro/",),
)
api_gateway = Service(ServiceName.API_GATEWAY, ("app/server/APIGateway/",), (contract,))
signalr = Service(ServiceName.SIGNALR, ("app/server/SignalRService/",), (contract,))
tracking_api = Service(
    ServiceName.TRACKING_API, ("app/server/TrackingService/",), (contract,)
)
upload_api = Service(
    ServiceName.UPLOAD_API, ("app/server/UploadFileService/",), (contract,)
)
identity_api = Service(
    ServiceName.IDENTITY_API, ("app/server/IdentityService/",), (contract,)
)
notification_api = Service(
    ServiceName.NOTIFICATION_API,
    (
        "app/server/NotificationService/src/NotificationService.API/",
        "app/server/NotificationService/src/NotificationService.Application/",
        "app/server/NotificationService/src/NotificationService.Domain/",
        "app/server/NotificationService/src/NotificationService.Infrastructure/",
    ),
    (contract,),
)
recipe_api = Service(
    ServiceName.RECIPE_API,
    (
        "app/server/RecipeService/src/RecipeService.API/",
        "app/server/RecipeService/src/RecipeService.Application/",
        "app/server/RecipeService/src/RecipeService.Domain/",
        "app/server/RecipeService/src/RecipeService.Infrastructure/",
    ),
    (contract,),
)
user_api = Service(ServiceName.USER_API, ("app/server/UserService/",), (contract,))
ingredient_predict_api = Service(
    ServiceName.INGREDIENT_PREDICT_API,
    ("app/server/IngredientPredictService/",),
    (contract,),
)
email_worker = Service(
    ServiceName.EMAIL_WORKER,
    ("app/server/NotificationService/src/EmailWorker/",),
    (contract,),
)
sms_worker = Service(
    ServiceName.SMS_WORKER,
    ("app/server/NotificationService/src/SMSWorker/",),
    (contract,),
)
push_worker = Service(
    ServiceName.PUSH_WORKER,
    ("app/server/NotificationService/src/PushNotificationWorker/",),
    (contract,),
)
recipe_worker = Service(
    ServiceName.RECIPE_WORKER,
    ("app/server/RecipeService/src/RecipeWorker/",),
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


def get_latest_service_commit_sha(service: Service, tag_length: int = 8) -> str:
    """
    Returns the latest commit sha of the service based on the current state of the
    Git commit tree.

    Args:
        service - The targeted service
        tag_length - The length of the tag result

    Note:
        Exclusion is handled natively via ``:(exclude,glob)`` pathspecs, which
        ``git log`` supports. Keep the exclusion logic in sync with
        :func:`get_service_sha`, which must replicate it manually because
        ``git ls-tree`` does not support extended pathspecs.
    """

    paths = get_paths(service)
    exclude_paths = [f":(exclude,glob){p}" for p in get_exclude_paths(service)]
    latest_sha = subprocess.run(
        ["git", "log", "-1", "--pretty=format:%H", "--", *paths, *exclude_paths],
        capture_output=True,
        check=True,
        text=True,
        cwd=_REPO_ROOT,
    ).stdout[:tag_length]

    return latest_sha


def get_service_sha(service: Service, tag_length: int = 8) -> str:
    """
    Returns the latest sha hash based on the current state of the Git commit tree.

    Args:
        service - The targeted service
        tag_length - The length of the tag result

    Note:
        Exclusion is done manually via ``str.startswith()`` because ``git ls-tree``
        does not support ``:(exclude,glob)`` pathspecs. This mirrors the exclusion
        in :func:`get_latest_service_commit_sha`, which uses pathspecs natively.
        Keep both functions in sync when modifying exclude_paths logic.
    """

    paths = get_paths(service)
    tree = subprocess.run(
        ["git", "ls-tree", "-r", "HEAD", "--", *paths],
        capture_output=True,
        check=True,
        text=True,
        cwd=_REPO_ROOT,
    ).stdout

    exclude_paths = get_exclude_paths(service)
    filtered_tree: list[str] = []

    for line in tree.splitlines():
        _, p = line.split("\t", 1)
        ignored = False

        for exclude_p in exclude_paths:
            if p.startswith(exclude_p):
                ignored = True
                break

        if not ignored:
            filtered_tree.append(line)

    filtered_tree_text = "\n".join(filtered_tree) + "\n" if filtered_tree else ""
    hash = hashlib.sha256(filtered_tree_text.encode("utf-8")).hexdigest()[:tag_length]
    return hash


def get_service_by_name(service_name: ServiceName) -> Service:
    return next(s for s in all_services if s.name == service_name)


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
    hash_tree: bool


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
        "--hash-tree",
        action="store_true",
        help="""
         Change to the hash calculating mode. The tags will be calculated based on the state of the tree in
         regard to the HEAD
        """,
    )

    return parser.parse_args(namespace=Args())


def main():
    args = parse_args()
    result: list[str] = []
    services = [get_service_by_name(name) for name in args.services]

    if not services:
        services = all_services

    for service in services:
        tag = (
            get_service_sha(service=service, tag_length=args.tag_length)
            if args.hash_tree
            else get_latest_service_commit_sha(
                service=service, tag_length=args.tag_length
            )
        )
        if not tag:
            continue

        result.append(f"{service.name}:{tag}")

    print(" ".join(result))


if __name__ == "__main__":
    main()
