from EnvUtility import get_mongodb_connection_string
from MongoClient import MongoClient


class RecipeManager:
    def __init__(self):
        self.manager = None
        self.recipe_db = None
        self.tag_collection = None

    def _connect_db(self):
        mongo_client = MongoClient(get_mongodb_connection_string()).get_mongo_client()
        self.recipe_db = mongo_client["RecipeDB"]
        self.tag_collection = recipe_db["Tag"]

    def get_manager(self):
        if self.manager is None:
            self._connect_db()
        return self

    def get_tag_collection(self):
        return self.tag_collection
