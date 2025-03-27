from typing import Dict

from app.models.password_entry import PasswordEntry
from app.database import db


"""
Returns a single password entry from the database for a given ID
:param password_entry_id: The ID of the password entry to retrieve
:return: A dictionary representation of the password entry
"""
def get_password_entry_by_id(password_entry_id: str):
    password_entry = PasswordEntry.query.get(password_entry_id)
    if not password_entry:
        raise ValueError("Password entry not found")
    return password_entry.to_dict()


"""
Returns all password entries from the database for a given user ID
:param user_id: The ID of the user to retrieve password entries for
:return: A list of dictionary representations of the password entries
"""
def get_password_entries_by_user_id(user_id: str):
    password_entries = PasswordEntry.query.filter_by(user_id=user_id).all()
    if not password_entries:
        raise ValueError(f"No password entries found for user ID {user_id}")
    
    return [password_entry.to_dict() for password_entry in password_entries]


"""
Creates a new password entry in the database
:param data: A dictionary containing the password entry data
:return: A dictionary representation of the newly created password entry
"""
def create_password_entry(data: Dict):
    password_entry = PasswordEntry(
        user_id=data["user_id"],
        username=data["username"],
        password=data["password"],
        url=data["url"],
        favicon=data["favicon"],
        folder_id=data["folder_id"],
        created_at=data["created_at"],
        updated_at=data["updated_at"]
    )
    db.session.add(password_entry)
    db.session.commit()
    return password_entry.to_dict()


"""
Updates an existing password entry in the database
:param password_entry_id: The ID of the password entry to update
:param data: A dictionary containing the fields to update
:return: A dictionary representation of the updated password entry
"""
def update_password_entry(password_entry_id: str, data: Dict):
    password_entry = PasswordEntry.query.get(password_entry_id)
    if not password_entry:
        raise ValueError("Password entry not found")

    for key, value in data.items():
        setattr(password_entry, key, value)
    db.session.commit()
    return password_entry.to_dict()


"""
Deletes a password entry from the database
:param password_entry_id: The ID of the password entry to delete
:return: A dictionary representation of the deleted password entry
"""
def delete_password_entry(password_entry_id: str):
    password_entry = PasswordEntry.query.get(password_entry_id)
    if not password_entry:
        raise ValueError("Password entry not found")

    db.session.delete(password_entry)
    db.session.commit()
    return password_entry.to_dict()