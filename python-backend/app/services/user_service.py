from typing import Dict

from app.models.user import User
from app.database import db


"""
Returns a single user from the database for a given ID
:param user_id: The ID of the user to retrieve
:return: A dictionary representation of the user
"""
def get_user_by_id(user_id: str):
    user = User.query.get(user_id)
    if not user:
        raise ValueError("User not found")
    return user.to_dict()

"""
Creates a new user in the database
:param data: A dictionary containing the user"s email and password
:return: A dictionary representation of the newly created user
"""
def create_user(data: Dict):
    if User.query.filter_by(email=data["email"]).first():
        raise ValueError("User already exists")
    
    user = User(email=data["email"])
    user.set_password(data["password"])
    db.session.add(user)
    db.session.commit()
    return user.to_dict()


"""
Updates an existing user in the database
:param user_id: The ID of the user to update
:param data: A dictionary containing the fields to update
:return: A dictionary representation of the updated user
"""
def update_user(user_id: str, data: Dict):
    user = User.query.get(user_id)
    if not user:
        raise ValueError("User not found")

    for key, value in data.items():
        setattr(user, key, value)
    db.session.commit()
    return user.to_dict()

"""
Deletes a user from the database
:param user_id: The ID of the user to delete
:return: A dictionary representation of the deleted user
"""
def delete_user(user_id: str):
    user = User.query.get(user_id)
    if not user:
        raise ValueError("User not found")

    db.session.delete(user)
    db.session.commit()
    return user.to_dict()