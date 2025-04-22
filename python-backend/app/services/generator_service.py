import string
import secrets


def generate_password(length: int = 12,
                      use_numbers: bool = True,
                      use_lowercase: bool = True,
                      use_uppercase: bool = True,
                      use_symbols: bool = True,
):
    lowercase_chars = string.ascii_lowercase if use_lowercase else ''
    uppercase_chars = string.ascii_uppercase if use_uppercase else ''
    number_chars = string.digits if use_numbers else ''
    symbol_chars = string.punctuation if use_symbols else ''

    all_chars = lowercase_chars + uppercase_chars + number_chars + symbol_chars

    if not all_chars:
        raise ValueError("At least one character set must be selected.")
    
    password = ''.join(secrets.choice(all_chars) for _ in range(length))
    
    return password