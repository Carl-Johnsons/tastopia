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
import random
import uvicorn
import yaml
import tensorflow as tf
import numpy as np
from open_clip import create_model_from_pretrained, get_tokenizer
import torch
import cv2
import faiss
from apscheduler.schedulers.background import BackgroundScheduler  # runs tasks in the background
from apscheduler.triggers.cron import CronTrigger  # allows us to specify a recurring time for execution
import os
os.environ["KMP_DUPLICATE_LIB_OK"] = "TRUE"

with open("log_config.yaml", "r") as f:
    log_config = yaml.safe_load(f.read())
logging.config.dictConfig(log_config)
logging.addLevelName(logging.INFO, "Information")
logging.addLevelName(logging.WARNING, "Warning")

# Load the YOLO model
# model = YOLO("./model/best_filtered.pt")
convnext_model = tf.keras.models.load_model('model/convnext_224_f.model.keras')
box_model = YOLO('./model/bestv13.pt')
clip_model, preprocess = create_model_from_pretrained('hf-hub:apple/DFN5B-CLIP-ViT-H-14')
tokenizer = get_tokenizer('ViT-H-14')

envUtil = EnvUtility()
envUtil.load_env()

service_host = os.getenv("SERVICE_HOST")
service_port = int(os.getenv("PORT"))

# Load tag from MongoDB
mongo_client = pymongo.MongoClient(envUtil.get_mongodb_connection_string())
print(mongo_client.list_database_names())
recipe_db = mongo_client["RecipeDB"]
tag_collection = recipe_db["Tag"]

def load_clip_features(names: dict, tag_dict: dict):
    # Load feature
    features = np.load('clip_feature/features.npy')
    features = features.reshape(features.shape[0], features.shape[2])
    filename_index = np.load('clip_feature/features_index.npy')

    if not tag_dict or not names:
        print("Filter clip features failed")
    # Filter
    features_list = []
    filename_index_list = []
    for i in range(filename_index.shape[0]):
        new_index = len(filename_index_list)
        class_label = filename_index[i].split('/')[1]
        class_code = names[class_label][2]
        if not tag_dict.get(class_code):
            continue

        new_filename = filename_index[i].split(' ')[0] + ' ' + str(new_index)
        features_list.append(features[i])
        filename_index_list.append(new_filename)

    features_list = np.array(features_list)
    filename_index_list = np.array(filename_index_list)
    return features_list, filename_index_list

def sync_tags_and_load_faiss():
    global tag_dict, names, index, filename_index

    # Load tags from MongoDB
    tag_dict = dict()
    for tag in tag_collection.find({'Status': 'Active', 'Category': 'Ingredient'}).to_list():
        tag_dict[tag['Code']] = {
            'En': tag['Value']['En'],
            'Vi': tag['Value']['Vi'],
        }

    # Load names from file
    names = dict()
    for i in open("name_edited.txt", encoding='utf-8').read().splitlines():
        code = i.split('_')[2].replace(' ', '_').upper()
        names[i.split('_')[0]] = [i.split('_')[2], i.split('_')[4], code]
        if tag_dict.get(code):
            tag_dict[code]['Pretrained'] = True

    # Load feature
    features, filename_index = load_clip_features(names, tag_dict)
    index = faiss.IndexFlatL2(features.shape[1])
    index.add(features)

# Initialize the index, tags and load features
sync_tags_and_load_faiss()

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

# Set up the scheduler
scheduler = BackgroundScheduler()
trigger = CronTrigger(hour=0, minute=0)  # midnight every day
scheduler.add_job(sync_tags_and_load_faiss, trigger)
scheduler.start()

app = FastAPI(lifespan=lifespan, redirect_slashes=False)
# app = FastAPI(redirect_slashes=False)

@app.get("/health")
async def health():
    return {"status": "ok"}

def cal_mix_clip_cnn(a, b):
    scores = dict()
    for i in a:
        scores[i] = 0
    for i, val in enumerate(a):
        scores[val] += 1 / (i + 0.5)
        # scores[val] += 1

    for key in scores.keys():
        scores[key] *= b[key]

    ans = a[0]
    score = scores[a[0]]
    for key in scores.keys():
        if scores[key] > score:
            score = scores[key]
            ans = key
    
    indexs = [i for i in scores.keys()]
    probs = [scores[i] for i in scores.keys()]

    sorted_pairs = sorted(zip(indexs, probs), key=lambda x: x[1], reverse=True)
    indexs, probs = zip(*sorted_pairs)

    return list(indexs), list(probs)

def get_raw_convnext_predict(image):
    image_size = (224, 224)
    # image = Image.open('test_images/z6471002443892_1f49bc4e11465aebdcde6beee10b2a7d.jpg').convert("RGB")
    image_np = np.array(image)
    image_np = cv2.resize(image_np, image_size, interpolation=cv2.INTER_AREA)
    image_nps = np.expand_dims(image_np, axis=0)
    results = convnext_model.predict(image_nps)
    return results.tolist()

def chose_res_clip(a):
    scores = dict()
    for i in a:
        scores[i] = 0
    for i, val in enumerate(a):
        scores[val] += 1 / (i + 1)
    ans = a[0]
    score = scores[a[0]]
    for key in scores.keys():
        if scores[key] > score:
            score = scores[key]
            ans = key
    
    return ans

def get_raw_clip_predict(image, no_sample=12):
    image = preprocess(image).unsqueeze(0)
    image_features = clip_model.encode_image(image).cpu().detach().numpy()

    clip_pred_raw = []
    D, I = index.search(np.array(image_features), no_sample)
    pred_class = [int(filename_index[i].split('/')[1]) - 1 for i in I[0]]
    clip_pred_raw.append(pred_class)
    return clip_pred_raw

@app.post("/api/ingredient-predict-v2")
async def predict(file: UploadFile = File(...)):
    image = Image.open(io.BytesIO(await file.read()))
    image = image.convert("RGB")
    # Apply exif metadata if exist
    image = ImageOps.exif_transpose(image)

    clip_pred_raw = get_raw_clip_predict(image, 50)
    convnext_pred_raw = get_raw_convnext_predict(image)
    indexs, probs = cal_mix_clip_cnn(clip_pred_raw[0], convnext_pred_raw[0])

    classifications = []
    for class_index, conf in zip(indexs[:5], probs[:5]):
        class_label = str(class_index + 1).zfill(3)
        classifications.append({
            "class": class_label,
            "confidence": float(conf),
            "name": {
                'en': names[class_label][0],
                'vi': names[class_label][1]
            },
            "code": '_'.join(names[class_label][0].split(' ')).upper(),
        })

    results = box_model(image, verbose=False)
    return {"classifications": classifications, "boxes": results[0].boxes.xyxyn.tolist()}

@app.get("/api/test")
async def test_api():
    for x in tag_collection.find():
        print(x)
    return ''

@app.get("/")
async def root():
    return {"message": "FastAPI is running!"}

uvicorn.run(app, host="0.0.0.0", port=service_port,log_config=log_config)
