import os
from typing import Dict, List

from modal import App, CloudBucketMount, Image, Secret, Volume, is_local
from EnvUtility import get_env, get_modal_app_name

SECRET_NAME = "tastopia-ingredient-predict-secret"
TAILSCALE_SECRET_NAME = "tailscale-auth"
ENVIRONMENT = get_env()
BUCKET_NAME = "tastopia-ingredient-predict-model"
CLOUDFLARE_R2_BUCKET_ENDPOINT = os.getenv("CLOUDFLARE_R2_BUCKET_ENDPOINT")
MODAL_APP_NAME = get_modal_app_name()


def get_secrets() -> List[Secret]:
    secrets = []

    # if is_local():
    # print("Loading env from local...")
    # load_env()
    # secret = Secret.from_dict(
    # {
    # "PYTHON_ENV": os.environ["PYTHON_ENV"],
    # "MONGODB_HOST": os.environ["MONGODB_HOST"],
    # "MONGODB_PORT": os.environ["MONGODB_PORT"],
    # "MONGO_INITDB_ROOT_USERNAME": os.environ["MONGO_INITDB_ROOT_USERNAME"],
    # "MONGO_INITDB_ROOT_PASSWORD": os.environ["MONGO_INITDB_ROOT_PASSWORD"],
    # "REDIS_HOST": os.environ["REDIS_HOST"],
    # "REDIS_PORT": os.environ["REDIS_PORT"],
    # "REDIS_PASSWORD": os.environ["REDIS_PASSWORD"],
    # "CLOUDFLARE_R2_BUCKET_ENDPOINT": os.environ[
    # "CLOUDFLARE_R2_BUCKET_ENDPOINT"
    # ],
    # "AWS_ACCESS_KEY_ID": os.environ["CLOUDFLARE_R2_ACCESS_KEY"],
    # "AWS_SECRET_ACCESS_KEY": os.environ["CLOUDFLARE_R2_SECRET_ACCESS_KEY"],
    # "AWS_DEFAULT_REGION": os.environ["AWS_DEFAULT_REGION"],
    # }
    # )
    # else:
    print("Loading env from remote...")
    secret = Secret.from_name(SECRET_NAME, environment_name=ENVIRONMENT)
    secrets.append(secret)

    tailscale_secret = Secret.from_name(
        TAILSCALE_SECRET_NAME, environment_name=ENVIRONMENT
    )
    secrets.append(tailscale_secret)
    secrets.append(
        Secret.from_dict(
            {
                "ALL_PROXY": "socks5://localhost:1080/",
                "HTTP_PROXY": "http://localhost:1080/",
                "http_proxy": "http://localhost:1080/",
            }
        )
    )

    return secrets


def get_volumes() -> Dict[str, Volume]:
    secret = Secret.from_name(SECRET_NAME, environment_name=ENVIRONMENT)
    volume = CloudBucketMount(
        bucket_name=BUCKET_NAME,
        bucket_endpoint_url=CLOUDFLARE_R2_BUCKET_ENDPOINT,
        secret=secret,
        read_only=True,
    )
    hf_cache_vol = Volume.from_name("huggingface-cache", create_if_missing=True)
    volumes = {"/models": volume, "/root/.cache/huggingface": hf_cache_vol}
    return volumes


def get_image() -> Image:
    image = (
        Image.debian_slim(python_version="3.11")
        .pip_install_from_requirements("requirements_ai.txt")
        .apt_install(["curl", "iputils-ping"])
        .run_commands("curl -fsSL https://tailscale.com/install.sh | sh")
        .uv_pip_install("requests==2.32.3", "PySocks==1.7.1")
        .add_local_file("tailscale_entrypoint.sh", "/root/entrypoint.sh", copy=True)
        .run_commands("chmod a+x /root/entrypoint.sh")
        .entrypoint(["/root/entrypoint.sh"])
        .add_local_dir("clip_feature", "/root/clip_feature")
        .add_local_file("./EnvUtility.py", "/root/EnvUtility.py")
        .add_local_file("./AI.py", "/root/AI.py")
        .add_local_file("./ModalUtility.py", "/root/ModalUtility.py")
        .add_local_file("./ModelLoader.py", "/root/ModelLoader.py")
        .add_local_file("./MongoClient.py", "/root/MongoClient.py")
        .add_local_file("./RedisManager.py", "/root/RedisManager.py")
        .add_local_file("name_edited.txt", "/root/name_edited.txt")
        .add_local_file("./log_config.yaml", "/root/log_config.yaml")
        .add_local_file("./filters.py", "/root/filters.py")
    )

    with image.imports():
        import socket
        import socks

    if not is_local():
        socks.set_default_proxy(socks.SOCKS5, "0.0.0.0", 1080)
        socket.socket = socks.socksocket

    return image


def get_app() -> App:
    app = App(
        MODAL_APP_NAME, image=get_image(), secrets=get_secrets(), volumes=get_volumes()
    )
    return app
