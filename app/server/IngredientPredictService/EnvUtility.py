from dotenv import load_dotenv
import os

class EnvUtility:
    def __init__(self):
      pass

    def load_env(self):
        # load the PYTHON_ENV
        load_dotenv()
        # load the global env
        if(self.is_development()):
          load_dotenv("../../../.env")
        else:
          load_dotenv("../../../.env.production")
        # load the current env again to override the global env
        load_dotenv()

    def is_development(self):
       return os.getenv("PYTHON_ENV") == "development"
    
    def is_production(self):
       return os.getenv("PYTHON_ENV") == "production"
    
    def get_mongodb_connection_string(self):
       host = os.getenv("MONGODB_HOST")
       port = os.getenv("MONGODB_PORT")
       user = os.getenv("MONGO_INITDB_ROOT_USERNAME")
       pwd = os.getenv("MONGO_INITDB_ROOT_PASSWORD")

       return f"mongodb://{user}:{pwd}@{host}:{port}?authSource=admin"
