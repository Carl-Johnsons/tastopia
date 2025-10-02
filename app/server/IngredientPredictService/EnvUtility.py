from dotenv import load_dotenv
import os

def load_env():
    # load the global env
    if(is_development()):
      load_dotenv("../../../.env")
      load_dotenv(".env")
    else:
      load_dotenv("../../../.env.production")
      load_dotenv(".env.production")

    # load the current env again to override the global env

def is_development():
    return os.getenv("PYTHON_ENV", "development") == "development"

def is_production():
    return os.getenv("PYTHON_ENV", "development") == "production"

def get_mongodb_connection_string():
    host = os.getenv("MONGODB_HOST")
    port = os.getenv("MONGODB_PORT")
    user = os.getenv("MONGO_INITDB_ROOT_USERNAME")
    pwd = os.getenv("MONGO_INITDB_ROOT_PASSWORD")

    return f"mongodb://{user}:{pwd}@{host}:{port}?authSource=admin"