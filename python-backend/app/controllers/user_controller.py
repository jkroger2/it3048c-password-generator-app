from flask import Blueprint, jsonify, request
from flask_jwt_extended import jwt_required, get_jwt_identity
from app.services.user_service import (
    get_user, 
    create_user,
    update_user,
    delete_user,
    authenticate_user
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
        user = authenticate_user(data)
        return jsonify(user)
    except ValueError as e:
        return jsonify({"error": str(e)}), 401


"""
POST /api/users/v1/register

Registers a new user in the system
:param data: A dictionary containing the user's email and password
:return: A dictionary representation of the newly created user
"""
@user_controller.route("/register", methods=["POST"])
def create_new_user():
    data = request.get_json()
    try:
        user = create_user(data)
        return jsonify(user), 201
    except ValueError as e:
        return jsonify({"error": str(e)}), 400
    

"""
PUT /api/users/v1

Updates an existing user's information
:param data: A dictionary containing the fields to update
:return: A dictionary representation of the updated user
"""
@user_controller.route("/", methods=["PUT"])
@jwt_required()
def update_user_info():
    user_id = get_jwt_identity()
    data = request.get_json()
    
    if data.get("id") != user_id:
        return jsonify({"error": "Unauthorized"}), 401
    
    try:
        user = update_user(user_id, data)
        return jsonify(user)
    except ValueError as e:
        return jsonify({"error": str(e)}), 404
    

"""
DELETE /api/users/v1/

Deletes a user's account from the system
:param data: A dictionary containing the user's ID
:return: A dictionary representation of the deleted user
"""
@user_controller.route("/", methods=["DELETE"])
@jwt_required()
def delete_user_account():
    user_id = get_jwt_identity()
    data = request.get_json()
    
    if data.get("id") != user_id:
        return jsonify({"error": "Unauthorized"}), 401

    try:
        user = delete_user(user_id)
        return jsonify(user)
    except ValueError as e:
        return jsonify({"error": str(e)}), 404