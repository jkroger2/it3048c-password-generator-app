from flask import Blueprint, jsonify, request
from flask_jwt_extended import jwt_required, get_jwt_identity

from app.services.account_service import (
    get_account_by_id,
    get_accounts_by_user_id,
    create_account,
    update_account,
    delete_account
)

account_controller = Blueprint("account", __name__)


"""
GET /api/accounts/v1

Returns all accounts for a given user
:return: A list of dictionary representations of the account entries
"""
@account_controller.route("/", methods=["GET"])
@jwt_required()
def get_user_accounts():
    user_id = get_jwt_identity()

    try:
        accounts = get_accounts_by_user_id(user_id)
        return jsonify({
            "status": "SUCCESS",
            "message": "Accounts retrieved successfully.",
            "accounts": accounts
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
GET /api/accounts/v1/<folder_id>
"""
@account_controller.route("/<folder_id>", methods=["GET"])
@jwt_required()
def get_account_by_folder_id(folder_id):
    user_id = get_jwt_identity()
    
    try:
        accounts = get_accounts_by_user_id(user_id, folder_id)
        return jsonify({
            "status": "SUCCESS",
            "message": "Accounts retrieved successfully.",
            "accounts": accounts
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
POST /api/accounts/v1

Creates a new account for a user
:return: A dictionary representation of the newly created account
"""
@account_controller.route("/", methods=["POST"])
@jwt_required()
def create_new_account():
    user_id = get_jwt_identity()
    data = request.get_json()
    try:
        account = create_account(
            user_id=user_id, 
            name=data.get("name"),
            username=data.get("username"), 
            password=data.get("password"),
            url=data.get("url"),
            folder_id=data.get("folder_id")
        )
        return jsonify({
            "status": "SUCCESS",
            "message": "Account created successfully.",
            "account": account
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
PUT /api/accounts/v1/<account_id>

Updates an existing account for a user
:param account_id: The ID of the account to update
:return: A dictionary representation of the updated account
"""
@account_controller.route("/<account_id>", methods=["PUT"])
@jwt_required()
def update_existing_accounts(account_id: str):
    user_id = get_jwt_identity()
    data = request.get_json()

    try:
        if get_account_by_id(account_id)["user_id"] != user_id:
            return jsonify({
                "status": "ERROR",
                "error": {
                    "code": "UNAUTHORIZED",
                    "message": "User does not have permission to update this account."
                }
            }), 403
    
        account = update_account(account_id, data)
        return jsonify({
            "status": "SUCCESS",
            "message": "account updated successfully.",
            "account": account
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
DELETE /api/accounts/v1/<account_id>
:param account_id: The ID of the account to delete
:return: A dictionary representation of the deleted account
"""
@account_controller.route("/<account_id>", methods=["DELETE"])
@jwt_required()
def delete_existing_accounts(account_id: str):
    user_id = get_jwt_identity()

    try:
        if get_account_by_id(account_id)["user_id"] != user_id:
            return jsonify({
                "status": "ERROR",
                "error": {
                    "code": "UNAUTHORIZED",
                    "message": "User does not have permission to delete this account."
                }
            }), 403
        account = delete_account(account_id)
        return jsonify({
            "status": "SUCCESS",
            "message": "Account deleted successfully.",
            "account": account
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