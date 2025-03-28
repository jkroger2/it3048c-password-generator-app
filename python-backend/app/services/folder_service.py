from typing import Dict

from app.models.folder import Folder
from app.database import db


"""
Returns a single folder from the database for a given ID
:param folder_id: The ID of the folder to retrieve
:return: A dictionary representation of the folder
"""
def get_folder_by_id(folder_id: str):
    folder = Folder.query.get(folder_id)
    if not folder:
        raise ValueError("Folder not found.")
    return folder.to_dict()


"""
Returns all folders from the database for a given user ID
:param user_id: The ID of the user to retrieve folders for
:return: A list of dictionary representations of the folders
"""
def get_folders_by_user_id(user_id: str):
    folders = Folder.query.filter_by(user_id=user_id).all()
    if not folders:
        raise ValueError(f"No folders found for user ID {user_id}.")
    
    return [folder.to_dict() for folder in folders]


"""
Creates a new folder in the database
:param data: A dictionary containing the folder data
:return: A dictionary representation of the newly created folder
"""
def create_folder(data: Dict):
    folder = Folder(
        user_id=data["user_id"],
        name=data["name"],
        created_at=data["created_at"],
        updated_at=data["updated_at"]
    )
    db.session.add(folder)
    db.session.commit()
    return folder.to_dict()


"""
Updates an existing folder in the database
:param folder_id: The ID of the folder to update
:param data: A dictionary containing the fields to update
:return: A dictionary representation of the updated folder
"""
def update_folder(folder_id: str, data: Dict):
    folder = Folder.query.get(folder_id)
    if not folder:
        raise ValueError("Folder not found.")

    for key, value in data.items():
        setattr(folder, key, value)

    db.session.commit()
    
    updated_folder = Folder.query.get(folder_id)
    return updated_folder.to_dict()


"""
Deletes a folder from the database
:param folder_id: The ID of the folder to delete
:return: A dictionary representation of the deleted folder
"""
def delete_folder(folder_id: str):
    folder = Folder.query.get(folder_id)
    if not folder:
        raise ValueError("Folder not found.")

    db.session.delete(folder)
    db.session.commit()
    return folder.to_dict()