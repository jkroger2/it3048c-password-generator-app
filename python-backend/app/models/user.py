import uuid
from app.database import db

class User(db.Model):
    id = db.Column(db.String(36), primary_key=True, unique=True, default=lambda: str(uuid.uuid4()))
    email = db.Column(db.String(120), unique=True, nullable=False)
    master_password = db.Column(db.String(80), nullable=False)
    password_salt = db.Column(db.String(32), nullable=False)
    password_hash = db.Column(db.String(64), nullable=False)
    created_at = db.Column(db.DateTime, nullable=False)
    updated_at = db.Column(db.DateTime, nullable=False)

    def to_dict(self):
        return {
            'id': self.id,
            'email': self.email,
            'created_at': self.created_at,
            'updated_at': self.updated_at
        }