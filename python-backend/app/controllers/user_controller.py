from flask import Blueprint, jsonify, request
from flask_jwt_extended import jwt_required, get_jwt_identity
from app.services.user_service import (
    authenticate_user,
    get_user_by_id,
    create_user,
    update_user,
    delete_user,
)

user_controller = Blueprint("users", __name__)


"""
POST /api/users/v1/login

Logs in a user by authenticating their credentials
:param data: A dictionary containing the user's email and password
:return: A dictionary containing the user's access token and user information
"""
@user_controller.route("/login", methods=["POST"])
def login_user():
    data = request.get_json()
    try:
        user_details = authenticate_user(data)
        return jsonify({
            "status": "SUCCESS",
            "message": "User logged in successfully.",
            "access_token": user_details["access_token"],
            "user": {
                "email": user_details["user"]["email"],
                "created_at": user_details["user"]["created_at"],
                "updated_at": user_details["user"]["updated_at"]
            }
        }), 200
    except ValueError as e:
        return jsonify({
            "status": "ERROR",
            "error": {
                "code": "LOGIN_FAILED",
                "message": str(e)
            }
        }), 401
    except Exception as e:
        return jsonify({
            "status": "ERROR",
            "error": {
                "code": "INTERNAL_SERVER_ERROR",
                "message": str(e)
            }
        }), 500


"""
POST /api/users/v1/register

Registers a new user in the system
:return: A dictionary representation of the newly created user
"""
@user_controller.route("/register", methods=["POST"])
def create_new_user():
    data = request.get_json()
    try:
        user = create_user(data)
        return jsonify({
            "status": "SUCCESS",
            "message": "User successfully registered.",
            "user": {
                "id": user["id"],
                "email": user["email"],
                "created_at": user["created_at"],
            }
        }), 201
    except ValueError as e:
        return jsonify({
            "status": "ERROR",
            "error": {
                "code": "USER_ALREADY_EXISTS",
                "message": str(e)
            }
        }), 400
    except KeyError as e:
        return jsonify({
            "status": "ERROR",
            "error": {
                "code": "INVALID_REQUEST",
                "message": f"Missing required field: {str(e)}"
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
PUT /api/users/v1

Updates an existing user's information
:return: A dictionary representation of the updated user
"""
@user_controller.route("/", methods=["PUT"])
@jwt_required()
def update_user_info():
    user_id = get_jwt_identity()
    data = request.get_json()
        
    try:
        user = update_user(user_id, data)
        return jsonify({
            "status": "SUCCESS",
            "message": "User information successfully updated.",
            "user": {
                "email": user["email"],
                "created_at": user["created_at"],
                "updated_at": user["updated_at"]
            }
        }), 200
    except ValueError as e:
        return jsonify({
            "status": "ERROR",
            "error": {
                "code": "USER_NOT_FOUND",
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
DELETE /api/users/v1/

Deletes a user's account from the system
:return: A dictionary representation of the deleted user
"""
@user_controller.route("/", methods=["DELETE"])
@jwt_required()
def delete_user_account():
    user_id = get_jwt_identity()

    try:
        user = delete_user(user_id)
        return jsonify({
            "status": "SUCCESS",
            "message": "User account successfully deleted.",
            "user": {
                "email": user["email"],
                "created_at": user["created_at"],
                "updated_at": user["updated_at"]
            }
        }), 200
    except ValueError as e:
        return jsonify({
            "status": "ERROR",
            "error": {
                "code": "USER_NOT_FOUND",
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