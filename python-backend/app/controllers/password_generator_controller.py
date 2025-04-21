from flask import Blueprint, jsonify, request

from app.services.password_generator_service import generate_password, generate_passphrase

password_generator_controller = Blueprint("password_generator_controller", __name__)

"""
GET /api/password/v1/generate
"""
@password_generator_controller.route("/", methods=["POST"])
def generate_password_endpoint():
    data = request.get_json()

    length = data.get("length", 12)
    use_numbers = data.get("numbers", True)
    use_lowercase = data.get("lowercase", True)
    use_uppercase = data.get("uppercase", True)
    use_symbols = data.get("symbols", True)

    try:
        password = generate_password(
            length, 
            use_numbers, 
            use_lowercase, 
            use_uppercase, 
            use_symbols
        )
        return jsonify({
            "status": "SUCCESS",
            "message": "Password generated successfully.",
            "password": password
        })
    except ValueError as e:
        return jsonify({
            "status": "ERROR",
            "error": {
                "code": "INVALID_INPUT",
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
POST /api/password/v1/generate/passphrase
"""
@password_generator_controller.route("/passphrase", methods=["POST"])
def generate_passphrase_endpoint():
    data = request.get_json()

    word_count = data.get("word_count", 4)
    capitalize = data.get("capitalize", True)

    try:
        passphrase = generate_passphrase(
            word_count=word_count, 
            capitalize=capitalize
        )
        return jsonify({
            "status": "SUCCESS",
            "message": "Passphrase generated successfully.",
            "passphrase": passphrase
        })
    except ValueError as e:
        return jsonify({
            "status": "ERROR",
            "error": {
                "code": "INVALID_INPUT",
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