from EnvUtility import get_mongodb_connection_string, load_env
from ModalUtility import get_app
from MongoClient import MongoClient

load_env()
app = get_app()


@app.function(timeout=1000, enable_memory_snapshot=True)
def predict(image_bytes: bytes):
    from AI import predict_local

    return predict_local(image_bytes)


@app.function(timeout=300)
def debug_network():
    mongoClient = MongoClient(get_mongodb_connection_string())
    mongo_client = mongoClient.get_mongo_client()
    recipe_db = mongo_client["RecipeDB"]
    tag_collection = recipe_db["Tag"]
    tag_list = tag_collection.find({'Status': 'Active', 'Category': 'Ingredient'}).to_list()
    
    # redisManager = RedisManager()
