from flask import Blueprint, jsonify, request
from flask_jwt_extended import jwt_required, get_jwt_identity

from app.services.password_service import (
    get_password_by_id,
    get_password_by_user_id,
    create_password,
    update_password,
    delete_password
)

password_controller = Blueprint("passwords", __name__)


"""
GET /api/passwords/v1

Returns all passwords for a given user
:return: A list of dictionary representations of the password entries
"""
@password_controller.route("/", methods=["GET"])
@jwt_required()
def get_user_passwords():
    user_id = get_jwt_identity()

    try:
        passwords = get_password_by_user_id(user_id)
        return jsonify({
            "status": "SUCCESS",
            "message": "Passwords retrieved successfully.",
            "passwords": passwords
        }), 200
    except ValueError as e:
        return jsonify({
            "status": "ERROR",
            "error": {
                "code": "ACCOUNTS_NOT_FOUND",
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
POST /api/passwords/v1

Creates a new password entry for a user
:return: A dictionary representation of the newly created password entry
"""
@password_controller.route("/", methods=["POST"])
@jwt_required()
def create_password_entry():
    user_id = get_jwt_identity()
    data = request.get_json()
    try:
        password = create_password(
            user_id=user_id, 
            username=data.get("username"), 
            password=data.get("password"),
            url=data.get("url"),
            favicon=data.get("favicon"),
            folder_id=data.get("folder_id")
        )
        return jsonify({
            "status": "SUCCESS",
            "message": "Password entry created successfully.",
            "password": password
        }), 201
    except ValueError as e:
        return jsonify({
            "status": "ERROR",
            "error": {
                "code": "BAD_REQUEST",
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


"""
PUT /api/passwords/v1/<password_id>

Updates an existing password entry for a user
:param password_id: The ID of the password entry to update
:return: A dictionary representation of the updated password entry
"""
@password_controller.route("/<password_id>", methods=["PUT"])
@jwt_required()
def update_password_entry_info(password_id: str):
    user_id = get_jwt_identity()
    data = request.get_json()

    if get_password_by_id(password_id)["user_id"] != user_id:
        return jsonify({
            "status": "ERROR",
            "error": {
                "code": "UNAUTHORIZED",
                "message": "User does not have permission to update this password entry."
            }
        }), 403
    
    try:
        password = update_password(password_id, data)
        return jsonify({
            "status": "SUCCESS",
            "message": "Password entry updated successfully.",
            "password": password
        }), 200
    except ValueError as e:
        return jsonify({
            "status": "ERROR",
            "error": {
                "code": "ACCOUNT_NOT_FOUND",
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
DELETE /api/passwords/v1/<password_id>
:param password_id: The ID of the password entry to delete
:return: A dictionary representation of the deleted password entry
"""
@password_controller.route("/<password_id>", methods=["DELETE"])
@jwt_required()
def delete_password_entry(password_id: str):
    user_id = get_jwt_identity()

    if get_password_by_id(password_id)["user_id"] != user_id:
        return jsonify({
            "status": "ERROR",
            "error": {
                "code": "UNAUTHORIZED",
                "message": "User does not have permission to delete this password entry."
            }
        }), 403
    
    try:
        password = delete_password(password_id)
        return jsonify({
            "status": "SUCCESS",
            "message": "Password entry deleted successfully.",
            "password": password
        }), 200
    except ValueError as e:
        return jsonify({
            "status": "ERROR",
            "error": {
                "code": "ACCOUNT_NOT_FOUND",
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