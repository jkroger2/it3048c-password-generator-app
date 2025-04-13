from flask import Blueprint, jsonify, request

from app.services.password_generator_service import generate_password

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