from dotenv import load_dotenv
import os

class EnvUtility:
    def __init__(self):
      pass

    def load_env(self):
        # load the global env
        if(self.is_development()):
          load_dotenv("../../../.env")
          load_dotenv(".env")
        else:
          load_dotenv("../../../.env.production")
          load_dotenv(".env.production")

        # load the current env again to override the global env

    def is_development(self):
       return os.getenv("PYTHON_ENV", "development") == "development"
    
    def is_production(self):
       return os.getenv("PYTHON_ENV", "development") == "production"
    
    def get_mongodb_connection_string(self):
       host = os.getenv("MONGODB_HOST")
       port = os.getenv("MONGODB_PORT")
       user = os.getenv("MONGO_INITDB_ROOT_USERNAME")
       pwd = os.getenv("MONGO_INITDB_ROOT_PASSWORD")

       return f"mongodb://{user}:{pwd}@{host}:{port}?authSource=admin"
