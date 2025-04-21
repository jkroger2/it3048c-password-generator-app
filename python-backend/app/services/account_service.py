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
        raise ValueError(f"No account found for ID {account_id}.")
    
    return account.to_dict()


"""
Returns all accounts from the database for a given user ID
:param user_id: The ID of the user to retrieve account for
:return: A list of dictionary representations of the accounts
"""
def get_accounts_by_user_id(user_id: str, folder_id: str = None):
    if folder_id:
        accounts = Account.query.filter_by(user_id=user_id, folder_id=folder_id).all()
    else:
        accounts = Account.query.filter_by(user_id=user_id).all()
        
    if not accounts:
        raise ValueError(f"No accounts found for user ID {user_id}.")
    
    return [account.to_dict() for account in accounts]


"""
Creates a new account in the database
:param data: A dictionary containing the account data
:return: A dictionary representation of the newly created account 
"""
def create_account(
        user_id: str,
        name: str,
        username: str,
        password: str,
        url: str = None,
        folder_id: str = None
):
    account = Account(
        user_id=user_id,
        name=name,
        username=username,
        password=password,
        folder_id=folder_id,
    )

    if url:
        account.set_url(url)
        account.set_favicon()

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
        raise ValueError(f"No account found for ID {account_id}.")

    for key, value in data.items():
        setattr(account, key, value)

        if "url" in data.items():
            account.set_favicon()
            
    db.session.commit()
    
    updated_account = Account.query.get(account_id)
    return updated_account.to_dict()


"""
Deletes an account from the database
:param account_id: The ID of the account to delete
:return: A dictionary representation of the deleted account
"""
def delete_account(account_id: str):
    account = Account.query.get(account_id)
    if not account:
        raise ValueError(f"No account not found for ID {account_id}.")

    db.session.delete(account)
    db.session.commit()
    return account.to_dict()