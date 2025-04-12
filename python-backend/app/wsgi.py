import os
import time
import urllib.parse as urlparse
from dotenv import load_dotenv
from flask import Flask
from flask_jwt_extended import JWTManager

from app.controllers.user_controller import user_controller
from app.controllers.account_controller import account_controller
from app.controllers.folder_controller import folder_controller
from app.database import db

def create_app():
    app = Flask(__name__)

    # Load environment variables
    load_dotenv()

    DATABASE_HOST = os.getenv("DATABASE_HOST")
    DATABASE = os.getenv("DATABASE")
    DATABASE_USER = os.getenv("DATABASE_USER")
    DATABASE_PASSWORD = urlparse.quote_plus(os.getenv("DATABASE_PASSWORD"))
    DATABASE_URL = f"mysql+pymysql://{DATABASE_USER}:{DATABASE_PASSWORD}@{DATABASE_HOST}/{DATABASE}"
    JWT_SECRET_KEY = os.getenv("JWT_SECRET_KEY")

    app.config["SQLALCHEMY_DATABASE_URI"] = DATABASE_URL
    app.config["SQLALCHEMY_TRACK_MODIFICATIONS"] = False
    app.config["JWT_SECRET_KEY"] = JWT_SECRET_KEY

    # Initialize DB and JWT manager
    db.init_app(app)
    jwt = JWTManager(app)

    @jwt.unauthorized_loader
    def unauthorized_callback(callback):
        return {
            "status": "ERROR",
            "error": {
                "code": "UNAUTHORIZED",
                "message": "Missing or invalid JWT token."
            }
        }, 401
    @jwt.expired_token_loader
    def expired_token_callback(jwt_header, jwt_data):
        return {
            "status": "ERROR",
            "error": {
                "code": "EXPIRED_TOKEN",
                "message": "Token has expired."
            }
        }, 401
    
    # Register API controllers
    print("Registering API controllers")
    app.register_blueprint(user_controller, url_prefix="/api/users/v1")
    app.register_blueprint(account_controller, url_prefix="/api/accounts/v1")
    app.register_blueprint(folder_controller, url_prefix="/api/folders/v1")

    print("Connecting to DB: " + DATABASE_URL)
    with app.app_context():
        db.create_all()
 
    return app


app = create_app()

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5000, debug=True)