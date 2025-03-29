import uuid
from urllib.parse import urlparse

from app.database import db

class Account(db.Model):
    id = db.Column(db.String(36), primary_key=True, unique=True, default=lambda: str(uuid.uuid4()))
    user_id = db.Column(db.String(36), db.ForeignKey("user.id"), nullable=False)
    name = db.Column(db.String(120), nullable=False)
    username = db.Column(db.String(120), nullable=False)
    password = db.Column(db.String(120), nullable=False)
    url = db.Column(db.String(255), nullable=True)
    favicon = db.Column(db.String(255), nullable=True)
    folder_id = db.Column(db.String(36), db.ForeignKey("folder.id"), nullable=True)
    created_at = db.Column(db.DateTime, server_default=db.func.now())
    updated_at = db.Column(db.DateTime, server_default=db.func.now(), server_onupdate=db.func.now())
    
    def set_favicon(self):
        if self.url:
            parsed_url = urlparse(self.url)
            if parsed_url.netloc:  # Ensure valid domain extraction
                self.favicon = f"https://{parsed_url.netloc}/favicon.ico"
        return None

    def set_url(self, new_url):
        self.url = new_url
        self.favicon = self.get_favicon()
    
    def to_dict(self):
        return {
            "id": self.id,
            "user_id": self.user_id,
            "username": self.username,
            "password": self.password,
            "url": self.url,
            "favicon": self.favicon,
            "folder_id": self.folder_id,
            "created_at": self.created_at.isoformat(),
            "updated_at": self.updated_at.isoformat()
        }