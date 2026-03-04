from EnvUtility import get_mongodb_connection_string, load_env
from MongoClient import MongoClient
from ModelLoader import get_clip_model, get_clip_preprocessor, get_model, get_model_tokenizer
from RedisManager import RedisManager
from PIL import Image, ImageOps
import io
import logging
import logging.config
import yaml
import numpy as np
import torch
import cv2
import faiss
import asyncio
import aiohttp
import os

os.environ["KMP_DUPLICATE_LIB_OK"] = "TRUE"

with open("log_config.yaml", "r") as f:
    log_config = yaml.safe_load(f.read())
logging.config.dictConfig(log_config)
logging.addLevelName(logging.INFO, "Information")
logging.addLevelName(logging.WARNING, "Warning")

load_env()
# Define utility
mongoClient = MongoClient(get_mongodb_connection_string())
redisManager = RedisManager()

# Load the CNN model
convnext_model = get_model()
# yolo_model = YOLO("./model/yolo_best_f.pt")
clip_model = get_clip_model()
preprocess = get_clip_preprocessor() 
tokenizer = get_model_tokenizer()

# device = torch.device('cuda:0' if torch.backends.cuda.is_built() else 'cpu')
device = torch.device('cpu')
clip_model = clip_model.to(device)

ai_server_url = ''
MODAL_APP_NAME = "tastopia-ingredient-predict"
BUCKET_NAME = "tastopia-ingredient-predict-model"
CLOUDFLARE_R2_BUCKET_ENDPOINT = os.getenv("CLOUDFLARE_R2_BUCKET_ENDPOINT")

# Load tag from MongoDB
mongo_client = mongoClient.get_mongo_client()
# print(mongo_client.list_database_names())
recipe_db = mongo_client["RecipeDB"]
tag_collection = recipe_db["Tag"]
tag_list = tag_collection.find({'Status': 'Active', 'Category': 'Ingredient'}).to_list()

if not tag_list:
    logging.error("Empty tag list detected. Please check database and try again")
    raise Exception("Empty tag list detected")

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
    logging.info('Begin sync_tags_and_load_faiss')
    global tag_dict, names, index, filename_index, text_features, tag_codes

    # Load tags from MongoDB
    tag_dict = dict()
    tag_list = tag_collection.find({'Status': 'Active', 'Category': 'Ingredient'}).to_list()

    for tag in tag_list:
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
    logging.info('sync_tags_and_load_faiss End')
    

# Initialize the index, tags and load features
sync_tags_and_load_faiss()

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

            async with session.post(ai_server_url, data=data, timeout=30) as resp:
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
