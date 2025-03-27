import uuid
import datetime
from app.database import db

class Folder(db.Model):
    id = db.Column(db.String(36), primary_key=True, unique=True, default=lambda: str(uuid.uuid4()))
    user_id = db.Column(db.String(36), db.ForeignKey('user.id'), nullable=False)
    name = db.Column(db.String(120), nullable=False)
    passwords = db.relationship('Password', backref='folder', lazy=True)
    created_at = db.Column(db.DateTime, default=datetime.utcnow)
    updated_at = db.Column(db.DateTime, default=datetime.utcnow, onupdate=datetime.utcnow)


    def to_dict(self):
        return {
            "id": self.id,
            "name": self.name,
            "passwords": [password.to_dict() for password in self.passwords],
            "created_at": self.created_at.isoformat(),
            "updated_at": self.updated_at.isoformat()
        }