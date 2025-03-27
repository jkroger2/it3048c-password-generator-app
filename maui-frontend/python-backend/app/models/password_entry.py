import uuid
import datetime
from app.database import db

class PasswordEntry(db.Model):
    id = db.Column(db.String(36), primary_key=True, unique=True, default=lambda: str(uuid.uuid4()))
    user_id = db.Column(db.String(36), db.ForeignKey("user.id"), nullable=False)
    username = db.Column(db.String(120), nullable=False)
    password = db.Column(db.String(120), nullable=False)
    url = db.Column(db.String(255), nullable=False)
    favicon = db.Column(db.String(255), nullable=False)
    folder_id = db.Column(db.String(36), db.ForeignKey("folder.id"), nullable=True)
    created_at = db.Column(db.DateTime, default=datetime.utcnow)
    updated_at = db.Column(db.DateTime, default=datetime.utcnow, onupdate=datetime.utcnow)

    def get_favicon(self):
        return self.favicon
    
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