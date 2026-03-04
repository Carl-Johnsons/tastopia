import logging
from modal import is_local
import pymongo

if not is_local():
    import socket
    import socks
    socks.set_default_proxy(socks.SOCKS5, "0.0.0.0", 1080)
    socket.socket = socks.socksocket

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
    self.mongo_client = pymongo.MongoClient(self.mongodb_url)
    logging.info("Mongodb connect successfully!!")
