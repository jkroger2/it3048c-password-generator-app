from typing import Dict

from app.models.account import Account
from app.database import db


"""
Returns an account from the database for a given account ID
:param account_id: The ID of the account to retrieve
:return: A dictionary representation of the account
"""
def get_account_by_id(account_id: str):
    account = Account.query.get(account_id)
    if not account:
        raise ValueError("account not found.")
    
    return account.to_dict()


"""
Returns all accounts from the database for a given user ID
:param user_id: The ID of the user to retrieve account for
:return: A list of dictionary representations of the accounts
"""
def get_accounts_by_user_id(user_id: str):
    accounts = Account.query.filter_by(user_id=user_id).all()
    if not accounts:
        raise ValueError(f"No accounts found for user ID {user_id}.")
    
    return [account.to_dict() for account in accounts]


"""
Creates a new account in the database
:param data: A dictionary containing the account data
:return: A dictionary representation of the newly created account 
"""
def create_account(data: Dict):
    account = account(
        user_id=data["user_id"],
        username=data["username"],
        account=data["account"],
        url=data["url"],
        favicon=data["favicon"],
        folder_id=data["folder_id"],
        created_at=data["created_at"],
        updated_at=data["updated_at"]
    )
    db.session.add(account)
    db.session.commit()
    return account.to_dict()


"""
Updates an existing account in the database
:param account: The ID of the account to update
:param data: A dictionary containing the fields to update
:return: A dictionary representation of the updated account
"""
def update_account(account_id: str, data: Dict):
    account = Account.query.get(account_id)
    if not account:
        raise ValueError(f"No account found with ID {account_id}.")

    for key, value in data.items():
        setattr(account, key, value)
    db.session.commit()
    return account.to_dict()


"""
Deletes an account from the database
:param account_id: The ID of the account to delete
:return: A dictionary representation of the deleted account
"""
def delete_account(account_id: str):
    account = Account.query.get(account_id)
    if not account:
        raise ValueError(f"No account not found with ID {account_id}.")

    db.session.delete(account)
    db.session.commit()
    return account.to_dict()