import redis
import json
import logging
from PIL import Image
import os
import io
import imagehash

class RedisManager:
  def __init__(self):
    self.redis = None
    self.connect_redis()
  
  def connect_redis(self):
    host = os.getenv("REDIS_HOST")
    port = os.getenv("REDIS_PORT")
    password = os.getenv("REDIS_PASSWORD")
    self.redis = redis.Redis(host=host, port=port, password=password)     
  
  def get_prediction_from_cache(self, phash: str):
      key = f"prediction_img:{phash}"
      data = self.redis.get(key)
      return json.loads(data) if data else None

  def save_prediction_to_cache(self, phash: str, prediction, ttl=86400):
      key = f"prediction_img:{phash}"
      self.redis.set(key, json.dumps(prediction), ex=ttl)  # expire in 24h

  # ---- Image Hash Helper ----
  def compute_phash(self, image_bytes: bytes):
      image = Image.open(io.BytesIO(image_bytes))
      return str(imagehash.phash(image))  # perceptual hash
