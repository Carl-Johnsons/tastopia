import logging
import pymongo

MONGODB_SERVER_SELECTION_TIMEOUT_MS = 2000
MONGODB_SOCKET_TIMEOUT_MS = 2000
MONGODB_CONNECT_TIMEOUT_MS = 2000


class MongoClient:
  def __init__(self, mongodb_url):
    self.mongo_client = None
    self.mongodb_url = mongodb_url
  
  def get_mongo_client(self):
    if(self.mongo_client is None):
      self._connect_db()
    return self.mongo_client

  def _connect_db(self):
    if(self.mongodb_url is None):
      logging.error("Missing mongodb url")
      return
    logging.info(f"Connect to mongodb")
    self.mongo_client = pymongo.MongoClient(
        self.mongodb_url,
        serverSelectionTimeoutMS=MONGODB_SERVER_SELECTION_TIMEOUT_MS,
        socketTimeoutMS=MONGODB_SOCKET_TIMEOUT_MS,
        connectTimeoutMS=MONGODB_CONNECT_TIMEOUT_MS,
    )
    logging.info("Mongodb connect successfully!!")
