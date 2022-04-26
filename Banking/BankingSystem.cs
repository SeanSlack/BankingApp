using System;
using System.Collections.Generic;
using System.Text;

namespace Banking
{
    class BankingSystem
    {
        string _CustomerFileName = "customers.txt";
        string _AccountFileName = "accounts.txt";
        BusinessModel _BusinessModel;
        View _View;

        public BankingSystem()
        {
            try
            {
                _BusinessModel = new BusinessModel(_CustomerFileName, _AccountFileName, this);
                _View = new View(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Start()
        {
            _View.MainMenu();
        }

        public void AddCustomer(string firstName, string lastName, string address, string dob, string contact, string email)
        {
            try
            {
                _BusinessModel.AddCustomer(firstName, lastName, address, dob, contact, email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Customer> SearchCustomers(string firstName, string lastName, string address, string dob, string contact, string email)
        {
            try
            {
                return _BusinessModel.SearchCustomers(firstName, lastName, address, dob, contact, email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddAccount(string ownerID, string accountType, string balance)
        {
            try
            {
                _BusinessModel.AddAccount(ownerID, accountType, balance);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Account SearchAccount(string accountID)
        {
            try
            {
                return _BusinessModel.SearchAccount(accountID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Transfer(string sourceAccountID, string destinationAccountID, string amount)
        {
            try
            {
                _BusinessModel.Transfer(sourceAccountID, destinationAccountID, amount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Deposit(string accountID, string amount)
        {
            try
            {
                _BusinessModel.Deposit(accountID, amount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Withdraw(string accountID, string amount)
        {
            try
            {
                _BusinessModel.Withdraw(accountID, amount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetMonthlyDeposit(string accountID, string monthlyDeposit)
        {
            try
            {
                _BusinessModel.SetMonthlyDeposit(accountID, monthlyDeposit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void End()
        {
            try
            {
                _BusinessModel.End();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
