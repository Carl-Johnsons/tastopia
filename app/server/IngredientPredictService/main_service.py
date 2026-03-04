from contextlib import asynccontextmanager

from fastapi import FastAPI, File, UploadFile
import httpx
import logging
import logging.config
import random
import modal
import uvicorn
import yaml
from apscheduler.schedulers.background import BackgroundScheduler
from apscheduler.triggers.cron import CronTrigger
import asyncio
import os

from EnvUtility import get_modal_app_name, is_development, load_env
from RecipeManager import RecipeManager

load_env()

with open("log_config.yaml", "r") as f:
    log_config = yaml.safe_load(f.read())
logging.config.dictConfig(log_config)
logging.addLevelName(logging.INFO, "Information")
logging.addLevelName(logging.WARNING, "Warning")

service_host = os.getenv("SERVICE_HOST")
service_port = int(os.getenv("PORT"))


@asynccontextmanager
async def lifespan(app: FastAPI):
    service_name = os.getenv("CONSUL_INGREDIENT_PREDICT")
    service_id = service_name + str(random.randint(100000, 999999))
    consul_scheme = os.getenv("CONSUL_SCHEME")
    consul_port = int(os.getenv("CONSUL_PORT"))
    consul_host = os.getenv("CONSUL_HOST")
    consul_base_address = f"{consul_scheme}://{consul_host}:{consul_port}"

    consul_register_url = f"{consul_base_address}/v1/agent/service/register"
    consul_deregister_url = (
        f"{consul_base_address}/v1/agent/service/deregister/{service_id}"
    )

    if is_development():
        health_check_url = f"http://host.docker.internal:{service_port}/health"
    else:
        health_check_url = f"http://{service_host}:{service_port}/health"

    logging.info(f"Start instance {service_id}")
    service_registration = {
        "ID": service_id,
        "Name": service_name,
        "Address": service_host,
        "Port": service_port,
        "Tags": ["fastapi", "python"],
        "Check": {
            "HTTP": health_check_url,
            "Interval": "10s",
            "DeregisterCriticalServiceAfter": "1m",
        },
    }
    async with httpx.AsyncClient() as client:
        response = await client.put(consul_register_url, json=service_registration)
        if response.status_code == 200:
            logging.info("Successfully registered with Consul")
        else:
            logging.error("Failed to register with Consul", response.text)
    yield  # The app starts here

    async with httpx.AsyncClient() as client:
        response = await client.put(consul_deregister_url)
        if response.status_code == 200:
            logging.info("Deregistered from Consul")
        else:
            logging.error("Deregistration failed:", response.text)


# Set up the scheduler
# scheduler = BackgroundScheduler()
# trigger = CronTrigger(hour=0, minute=0)  # midnight every day
# scheduler.add_job(sync_tags_and_load_faiss, trigger)
# sync_ai_url_trigger = CronTrigger(minute="*/10")  # every 10 minutes
# scheduler.add_job(sync_ai_kaggle_server_url, sync_ai_url_trigger)
# scheduler.start()

app = FastAPI(lifespan=lifespan, redirect_slashes=False)
ai_server_url: str | None
MODAL_APP_NAME = get_modal_app_name()


@app.get("/health")
async def health():
    return {"status": "ok"}


predict = modal.Function.from_name(MODAL_APP_NAME, "predict")


async def perform_predict(file: UploadFile):
    image = await file.read()
    response = await predict.remote.aio(image)
    return response


@app.post("/api/ingredient-predict-v2")
async def predict_v2(file: UploadFile = File(...)):
    response = await perform_predict(file)
    return response


@app.post("/api/ingredient-predict-v2/multi")
async def predict_v2_multi(files: list[UploadFile] = File(...)):
    tasks = [perform_predict(file) for file in files]
    results = await asyncio.gather(*tasks)
    return {"predictions": results}


@app.get("/api/tags")
async def get_tags():
    tag_collection = RecipeManager().get_manager().get_tag_collection()
    ans = []
    for tag in tag_collection.find(
        {"Status": "Active", "Category": "Ingredient"}
    ).to_list():
        ans.append([tag["Code"], tag["Value"]["En"], tag["Value"]["Vi"]])
    return ans


@app.get("/")
async def root():
    return {"message": "FastAPI is running!"}


uvicorn.run(app, host="0.0.0.0", port=service_port, log_config=log_config)
