from contextlib import asynccontextmanager
from EnvUtility import EnvUtility
from fastapi import FastAPI, File, UploadFile, WebSocket, WebSocketDisconnect
from PIL import Image, ImageOps
from ultralytics import YOLO
import httpx
import pymongo
import io
import logging
import logging.config
import os
import random
import uvicorn
import yaml

with open("log_config.yaml", "r") as f:
    log_config = yaml.safe_load(f.read())
logging.config.dictConfig(log_config)
logging.addLevelName(logging.INFO, "Information")
logging.addLevelName(logging.WARNING, "Warning")

# Load the YOLO model
model = YOLO("./model/best_filtered.pt")
box_model = YOLO('./model/bestv13.pt')

envUtil = EnvUtility()
envUtil.load_env()

service_host = os.getenv("SERVICE_HOST")
service_port = int(os.getenv("PORT"))

mongo_client = pymongo.MongoClient(envUtil.get_mongodb_connection_string())
print(mongo_client.list_database_names())
recipe_db = mongo_client["RecipeDB"]
tag_collection = recipe_db["Tag"]

for x in tag_collection.find():
  print(x)

@asynccontextmanager
async def lifespan(app: FastAPI):
    service_name = os.getenv("CONSUL_INGREDIENT_PREDICT")
    service_id = service_name + str(random.randint(100000, 999999))
    consul_scheme = os.getenv("CONSUL_SCHEME")
    consul_port = int(os.getenv("CONSUL_PORT"))
    consul_host = os.getenv("CONSUL_HOST")
    consul_base_address = f"{consul_scheme}://{consul_host}:{consul_port}"

    consul_register_url = f"{consul_base_address}/v1/agent/service/register"
    consul_deregister_url = f"{consul_base_address}/v1/agent/service/deregister/{service_id}"

    if(envUtil.is_development()):
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
            "DeregisterCriticalServiceAfter": "1m"
        }
    }
    async with httpx.AsyncClient() as client:
        response = await client.put(consul_register_url, json=service_registration)
        if response.status_code == 200:
            logging.info("Successfully registered with Consul 😊")
        else:
            logging.error("Failed to register with Consul", response.text)
    yield  # The app starts here

    async with httpx.AsyncClient() as client:
        response = await client.put(consul_deregister_url)
        if response.status_code == 200:
            logging.info("Deregistered from Consul 👋")
        else:
            logging.error("Deregistration failed:", response.text)

app = FastAPI(lifespan=lifespan, redirect_slashes=False)

names = dict()
for i in open("name_edited.txt", encoding='utf-8').read().splitlines():
    names[i.split('_')[0]] = [i.split('_')[2], i.split('_')[4]]

@app.get("/health")
async def health():
    return {"status": "ok"}

@app.post("/api/ingredient-predict")
async def predict(file: UploadFile = File(...)):
    image = Image.open(io.BytesIO(await file.read()))
    # Apply exif metadata if exist
    image = ImageOps.exif_transpose(image)

    results = model(image, verbose=False)

    classifications = []
    for result in results:
        for cls, conf in zip(result.probs.top5, result.probs.top5conf):
            classifications.append({
                "class": result.names[cls],
                "confidence": float(conf),
                "name": {
                    'en': names[result.names[cls]][0],
                    'vi': names[result.names[cls]][1]
                },
                "code": '_'.join(names[result.names[cls]][0].split(' ')).upper(),
            })

    results = box_model(image, verbose=False)
    return {"classifications": classifications, "boxes": results[0].boxes.xyxyn.tolist()}


@app.get("/")
async def root():
    return {"message": "YOLO FastAPI is running!"}

uvicorn.run(app, host="0.0.0.0", port=5009,log_config=log_config)
