import string
import secrets
import os

# determine word list path
BASE_DIR = os.path.dirname(os.path.abspath(__file__))
WORD_LIST_PATH = os.path.join(BASE_DIR, '..', 'data', 'word-list.txt')

try:
    with open(WORD_LIST_PATH, 'r') as f:
        WORDS = [line.strip() for line in f if line.strip()]
except FileNotFoundError:
    WORDS = [] # handle any errors

if not WORDS:
    # stop the app with an error -- critical
    raise ValueError("Word list not found or empty at", WORD_LIST_PATH)


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

def generate_passphrase(word_count: int = 4, capitalize: bool = True):
    if not WORDS:
        raise ValueError("Word list is not available or empty.")

    selected_words = [secrets.choice(WORDS) for _ in range(word_count)]

    if capitalize:
        selected_words = [word.capitalize() for word in selected_words]
    else:
        selected_words = [word.lower() for word in selected_words]

    return ' '.join(selected_words)