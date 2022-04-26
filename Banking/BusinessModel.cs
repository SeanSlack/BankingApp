using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Banking
{
    class BusinessModel
    {
        private List<Customer> _Customers;
        private List<Account> _Accounts;
        private string _CustomerFileName;
        private string _AccountFileName;

        private BankingSystem _BankingSystem;

        // load data
        private void ReadDataFromFiles()
        {
            try
            {
                ReadCustomers();
                ReadAccounts();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Read customers
        private void ReadCustomers()
        {
            FileStream fs = new FileStream(_CustomerFileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            try
            {
                try
                { 
                    uint nCustomers = Convert.ToUInt32(sr.ReadLine());
                    for (uint i = 0; i < nCustomers; i++)
                        _Customers.Add(Customer.CreateCustomer(sr));
                }
                catch (Exception ex)
                {
                    sr.Close();
                    fs.Close();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            sr.Close();
            fs.Close();
        }

        // Load accounts
        private void ReadAccounts()
        {
            FileStream fs = null;
            StreamReader sr = null;
            try
            {
                fs = new FileStream(_AccountFileName, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(fs);
                try
                {
                    uint nAccounts = Convert.ToUInt32(sr.ReadLine());
                    for (uint i = 0; i < nAccounts; i++)
                        _Accounts.Add(Account.CreateAccount(sr, _Customers));
                }
                catch (Exception ex)
                {
                    sr.Close();
                    fs.Close();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            sr.Close();
            fs.Close();
        }

        // Write customers
        private void WriteCustomers()
        {
            FileStream fs = new FileStream(_CustomerFileName, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                try
                {
                    sw.WriteLine(_Customers.Count);
                    foreach (Customer c in _Customers)
                        c.ToStream(sw);
                }
                catch (Exception ex)
                {
                    sw.Close();
                    fs.Close();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            sw.Close();
            fs.Close();
        }

        // Write accounts
        private void WriteAccounts()
        {
            FileStream fs = new FileStream(_AccountFileName, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                try
                {
                    sw.WriteLine(_Accounts.Count);
                    foreach (Account a in _Accounts)
                        a.ToStream(sw);
                }
                catch (Exception ex)
                {
                    sw.Close();
                    fs.Close();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            sw.Close();
            fs.Close();
        }

        // Write data
        private void WriteDataToFiles()
        {
            try
            {
                WriteCustomers();
                WriteAccounts();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BusinessModel(string customerFileName, string accountFileName, BankingSystem bankingSystem)
        {
            _CustomerFileName = customerFileName;
            _AccountFileName = accountFileName;
            _BankingSystem = bankingSystem;

            _Customers = new List<Customer>();
            _Accounts = new List<Account>();
            try
            {
                ReadDataFromFiles();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddCustomer(string firstName, string lastName, string address, string dob, string contact, string email)
        {
            if (Utility.DoesExist(_Customers, firstName, lastName))
                throw new Exception(ErrorMessage.ExistingCustomer);
            else
            {
                try
                {
                    _Customers.Add(Customer.CreateCustomer(firstName, lastName, address, Convert.ToDateTime(dob), contact, email));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<Customer> SearchCustomers(string firstName, string lastName, string address, string dob, string contact, string email)
        {
            try
            {
                return Utility.SearchCustomers(_Customers, firstName, lastName, address, Convert.ToDateTime(dob), contact, email);
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
                _Accounts.Add(Account.CreateAccount(Convert.ToInt32(accountType), _Customers, Convert.ToUInt32(ownerID), Convert.ToDouble(balance)));
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
                return Utility.SearchAccount(_Accounts, Convert.ToUInt32(accountID));
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
                Account sourceAccount = SearchAccount(sourceAccountID);
                Account destinationAccount = SearchAccount(destinationAccountID);
                sourceAccount.Transfer(destinationAccount, Convert.ToDouble(amount));
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
                Account account = SearchAccount(accountID);
                if (account.GetType() != typeof(Type1Account))
                    throw new Exception(ErrorMessage.InvalidDeposit);
                ((Type1Account)account).Deposit(Convert.ToDouble(amount));
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
                Account account = SearchAccount(accountID);
                if (account.GetType() != typeof(Type1Account))
                    throw new Exception(ErrorMessage.InvalidWithdrawal);
                ((Type1Account)account).Withdraw(Convert.ToDouble(amount));
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
                Account account = SearchAccount(accountID);
                if (account.GetType() != typeof(Type2Account))
                    throw new Exception(ErrorMessage.InvalidMonthlyDeposit);
                ((Type2Account)account).MonthlyDeposit = Convert.ToDouble(monthlyDeposit);
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
                WriteDataToFiles();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
