from contextlib import asynccontextmanager
from EnvUtility import EnvUtility
from fastapi import FastAPI, File, UploadFile
from PIL import Image, ImageOps
# from ultralytics import YOLO
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
import asyncio
import aiohttp
import requests
import os
os.environ["KMP_DUPLICATE_LIB_OK"] = "TRUE"

with open("log_config.yaml", "r") as f:
    log_config = yaml.safe_load(f.read())
logging.config.dictConfig(log_config)
logging.addLevelName(logging.INFO, "Information")
logging.addLevelName(logging.WARNING, "Warning")

# Load the CNN model
convnext_model = tf.keras.models.load_model('model/convnext_224_f.model.keras')
# yolo_model = YOLO("./model/yolo_best_f.pt")
clip_model, preprocess = create_model_from_pretrained('hf-hub:apple/DFN5B-CLIP-ViT-H-14')
tokenizer = get_tokenizer('ViT-H-14')

# device = torch.device('cuda:0' if torch.backends.cuda.is_built() else 'cpu')
device = torch.device('cpu')
clip_model = clip_model.to(device)

envUtil = EnvUtility()
envUtil.load_env()

service_host = os.getenv("SERVICE_HOST")
service_port = int(os.getenv("PORT"))

ai_kaggle_server_url = ''

# Load tag from MongoDB
mongo_client = pymongo.MongoClient(envUtil.get_mongodb_connection_string())
print(mongo_client.list_database_names())
recipe_db = mongo_client["RecipeDB"]
tag_collection = recipe_db["Tag"]

def sync_ai_kaggle_server_url():
    global ai_kaggle_server_url
    ai_kaggle_server_url = requests.get('https://script.google.com/macros/s/AKfycbyVK0wd-G_kuZXrQ0dGDC86xBkhfuHFTm5bXXe0hyL39IND815GSHpli4_v99Cb2KeZFg/exec').text
    
    print(f"AI Kaggle Server URL: {ai_kaggle_server_url}")

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
    global tag_dict, names, index, filename_index, text_features, tag_codes

    # Load tags from MongoDB
    tag_dict = dict()
    for tag in tag_collection.find({'Status': 'Active', 'Category': 'Ingredient'}).to_list():
        tag_dict[tag['Code']] = {
            'En': tag['Value']['En'],
            'Vi': tag['Value']['Vi'],
            'Pretrained': False,
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

    # Process text features
    tag_codes = [i for i in tag_dict.keys()]
    labels_list = [tag_dict[i]['En'] for i in tag_codes]
    text = tokenizer(labels_list, context_length=clip_model.context_length)
    text = torch.as_tensor(text, device=device)
    with torch.no_grad():
        text_features = clip_model.encode_text(text)

# Initialize the index, tags and load features
sync_tags_and_load_faiss()
sync_ai_kaggle_server_url()

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
sync_ai_url_trigger = CronTrigger(minute='*/10')  # every 10 minutes
scheduler.add_job(sync_ai_kaggle_server_url, sync_ai_url_trigger)
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

def encode_image_by_clip(image):
    image = preprocess(image).unsqueeze(0).to(device)
    image_features = clip_model.encode_image(image)
    return image_features

def get_raw_clip_predict(image_features, no_sample=12):
    image_features = image_features.cpu().detach().numpy()

    clip_pred_raw = []
    D, I = index.search(np.array(image_features), no_sample)
    pred_class = [int(filename_index[i].split('/')[1]) - 1 for i in I[0]]
    clip_pred_raw.append(pred_class)
    return clip_pred_raw

def get_raw_clip_text_predict(image_features):
    res = (image_features @ text_features.T)
    res = res[0].tolist()

    sorted_pairs = sorted(zip([i for i in range(len(res))], res), key=lambda x: x[1], reverse=True)
    indexs, probs = zip(*sorted_pairs)

    max_pretrained = 0
    for i in range(len(indexs)):
        if tag_dict.get(tag_codes[indexs[i]])['Pretrained']:
            max_pretrained = probs[i]
            break

    if probs[0] < 0.6:
        return indexs[:1], probs[:1]

    if probs[0] > max_pretrained * 1.2 and not tag_dict.get(tag_codes[indexs[0]])['Pretrained']:
        return indexs, probs
    return [], []

async def predict_server(image_bytes: bytes):
    try:
        async with aiohttp.ClientSession() as session:
            data = aiohttp.FormData()
            data.add_field('file', image_bytes, filename="image.jpg", content_type="image/jpeg")

            async with session.post(ai_kaggle_server_url + '/api/ingredient-predict', data=data, timeout=30) as resp:
                if resp.status == 200:
                    return await resp.json()
    except Exception as e:
        print(f"[Server Error] {e}")
    return None

async def predict_local(image_bytes: bytes):
    try:
        image = await asyncio.to_thread(Image.open, io.BytesIO(image_bytes))
        image = await asyncio.to_thread(image.convert, "RGB")
        image = await asyncio.to_thread(ImageOps.exif_transpose, image)

        classifications = []

        # Predict with pretrained and not pretrained text
        image_features = await asyncio.to_thread(encode_image_by_clip, image)
        indexs, probs = await asyncio.to_thread(get_raw_clip_text_predict, image_features)
        if len(indexs) > 0:
            if (len(indexs) == 1 and probs[0] < 0.6):
                pass
            else:
                for class_index, conf in zip(indexs[:5], probs[:5]):
                    class_label = '0'
                    classifications.append({
                        "class": class_label,
                        "confidence": float(conf),
                        "name": {
                            'en': tag_dict.get(tag_codes[class_index])['En'],
                            'vi': tag_dict.get(tag_codes[class_index])['Vi']
                        },
                        "code": tag_codes[class_index],
                    })
        else:
            # Predict with pretrained class
            clip_task = asyncio.to_thread(get_raw_clip_predict, image_features, 50)
            convnext_task = asyncio.to_thread(get_raw_convnext_predict, image)

            clip_pred_raw, convnext_pred_raw = await asyncio.gather(clip_task, convnext_task)
            indexs, probs = await asyncio.to_thread(cal_mix_clip_cnn, clip_pred_raw[0], convnext_pred_raw[0])

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

        # results = box_model(image, verbose=False)
        return {"classifications": classifications, "boxes": []}
    except Exception as e:
        print(f"[Server Error] {e}")
    return None
    

@app.post("/api/ingredient-predict-v2")
async def predict_v2(file: UploadFile = File(...)):
    image_bytes = await file.read()

    task_server = asyncio.create_task(predict_server(image_bytes))
    task_local = asyncio.create_task(predict_local(image_bytes))
    

    done, pending = await asyncio.wait(
        [task_server, task_local], return_when=asyncio.FIRST_COMPLETED
    )

    if not list(done)[0].result():
        return await list(pending)[0]

    for task in pending:
        task.cancel()

    result = list(done)[0].result()
    return result

@app.post("/api/ingredient-predict")
async def predict(file: UploadFile = File(...)):
    image = Image.open(io.BytesIO(await file.read()))
    image = image.convert("RGB")
    # Apply exif metadata if exist
    image = ImageOps.exif_transpose(image)

    classifications = []

    # Predict with pretrained and not pretrained text
    image_features = encode_image_by_clip(image)
    indexs, probs = get_raw_clip_text_predict(image_features)
    if len(indexs) > 0:
        for class_index, conf in zip(indexs[:5], probs[:5]):
            class_label = '0'
            classifications.append({
                "class": class_label,
                "confidence": float(conf),
                "name": {
                    'en': tag_dict.get(tag_codes[class_index])['En'],
                    'vi': tag_dict.get(tag_codes[class_index])['Vi']
                },
                "code": tag_codes[class_index],
            })
    else:
        # Predict with pretrained class
        clip_pred_raw = get_raw_clip_predict(image_features, 50)
        results = yolo_model(image, verbose=False)
        yolo_pred_raw = [[0] * 300]
        for i in range(len(results[0].names)):
            class_index = int(results[0].names[i]) - 1
            yolo_pred_raw[0][class_index] = float(results[0].probs.data[i])
        # yolo_pred_raw = [results[0].probs.data.tolist()]
        indexs, probs = cal_mix_clip_cnn(clip_pred_raw[0], yolo_pred_raw[0])

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

    # results = box_model(image, verbose=False)
    return {"classifications": classifications, "boxes": []}

@app.get("/api/tags")
async def get_tags():
    ans = []
    for tag in tag_collection.find({'Status': 'Active', 'Category': 'Ingredient'}).to_list():
        ans.append([tag['Code'], tag['Value']['En'], tag['Value']['Vi']])
    return ans

@app.get("/")
async def root():
    return {"message": "FastAPI is running!"}

uvicorn.run(app, host="0.0.0.0", port=service_port,log_config=log_config)
