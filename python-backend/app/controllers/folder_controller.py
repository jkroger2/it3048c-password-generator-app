from flask import Blueprint, jsonify, request
from flask_login import jwt_required, get_jwt_identity
from app.services.folder_service import (
    get_folder_by_id,
    get_folders_by_user_id,
    create_folder,
    update_folder,
    delete_folder
)


folder_controller = Blueprint("folder_controller", __name__)


"""
GET /api/folders/v1/

Returns all folders for a given user
:return: A list of dictionary representations of the folder
"""
@folder_controller.route("/", methods=["GET"])
@jwt_required()
def get_user_folders():
    user_id = get_jwt_identity()

    try:
        folders = get_folders_by_user_id(user_id)
        return jsonify({
            "status": "SUCCESS",
            "message": "Folders retrieved successfully.",
            "folders": folders
        })
    except ValueError as e:
        return jsonify({
            "status": "ERROR",
            "error": {
                "code": "FOLDER_NOT_FOUND",
                "message": str(e)
            }
        }), 404
    except Exception as e:
        return jsonify({
            "status": "ERROR",
            "error": {
                "code": "INTERNAL_SERVER_ERROR",
                "message": str(e)
            }
        }), 500

"""
POST /api/folders/v1/

Creates a new folder entry for a user
:return: A dictionary representation of the newly created folder
"""
@folder_controller.route("/", methods=["POST"])
@jwt_required()
def create_new_folder():
    user_id = get_jwt_identity()
    data = request.get_json()

    try:
        folder = create_folder(
            user_id=user_id,
            name=data.get("name")
        )
        return jsonify({
            "status": "SUCCESS",
            "message": "Folder created successfully.",
            "folder": folder
        }), 201
    except ValueError as e:
        return jsonify({
            "status": "ERROR",
            "error": {
                "code": "FOLDER_NOT_FOUND",
                "message": str(e)
            }
        }), 404
    except Exception as e:
        return jsonify({
            "status": "ERROR",
            "error": {
                "code": "INTERNAL_SERVER_ERROR",
                "message": str(e)
            }
        }), 500


"""
PUT /api/folders/v1/<folder_id>

Updates an existing folder entry for a user
:return: A dictionary representation of the updated folder
"""
@folder_controller.route("/<folder_id>", methods=["PUT"])
@jwt_required()
def update_existing_folder(folder_id):
    user_id = get_jwt_identity()
    data = request.get_json()

    if get_folder_by_id(folder_id)["user_id"] != user_id:
        return jsonify({"error": "Folder not found"}), 404
    
    try:
        folder = update_folder(folder_id, data)
        return jsonify({
            "status": "SUCCESS",
            "message": "Folder updated successfully.",
            "folder": folder
        }), 200
    except ValueError as e:
        return jsonify({
            "status": "ERROR",
            "error": {
                "code": "FOLDER_NOT_FOUND",
                "message": str(e)
            }
        }), 404
    except Exception as e:
        return jsonify({
            "status": "ERROR",
            "error": {
                "code": "INTERNAL_SERVER_ERROR",
                "message": str(e)
            }
        }), 500


"""
DELETE /api/folders/v1/<folder_id>

Deletes an existing folder entry for a user
:return: A dictionary representation of the deleted folder
"""
@folder_controller.route("/<folder_id>", methods=["DELETE"])
@jwt_required()
def delete_existing_folder(folder_id):
    user_id = get_jwt_identity()

    if get_folder_by_id(folder_id)["user_id"] != user_id:
        return jsonify({"error": "Unauthorized"}), 403
    
    try:
        folder = delete_folder(folder_id)
        return jsonify({
            "status": "SUCCESS",
            "message": "Folder deleted successfully.",
            "folder": folder
        }), 200
    except ValueError as e:
        return jsonify({
            "status": "ERROR",
            "error": {
                "code": "FOLDER_NOT_FOUND",
                "message": str(e)
            }
        }), 400
    except Exception as e:
        return jsonify({
            "status": "ERROR",
            "error": {
                "code": "INTERNAL_SERVER_ERROR",
                "message": str(e)
            }
        }), 500
        