using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Banking
{
    abstract class Account
    {
        /**************** Attributes ****************/
        // _Count holds the current number of accounts in the system
        private static ulong _Count = 0;

        private ulong _ID;
        public ulong ID
        {
            get { return _ID; }
        }

        protected Customer _Owner;
        public string Owner
        {
            get { return _Owner.ToString(); }
        }

        protected DateTime _OpenedDate;
        public DateTime OpenedDate
        {
            get { return _OpenedDate; }
        }

        protected DateTime _ClosedDate;
        public DateTime ClosedDate
        {
            get {
                    try
                    {
                        Validation.ForGettingClosedDate(this);
                        return _ClosedDate;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }                  
                }
        }

        protected bool _Active;
        public bool Active
        {
            get { return _Active; }
        }

        protected double _Balance = 0;
        public double Balance
        {
            get { return _Balance; }
            set
            {
                try
                {
                    Validation.ForBalance((double)value);
                    _Balance = value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /**************** Methods ****************/
        // Constructor 1
        protected Account(ulong accountID, Customer owner, DateTime openedDate, double initBalance = 0)
        {
            _ID = accountID;
            _Count = Math.Max(_Count, _ID);
            _Owner = owner;
            _Owner.AddAccount(this);
            _OpenedDate = openedDate;
            _Balance = initBalance;
            _Active = true;
        }

        // Constructor 2
        protected Account(Customer owner, DateTime openedDate, double initBalance = 0) : this(_Count + 1, owner, openedDate, initBalance)
        {
        }
        
        // Constructor 3
        protected Account(Customer owner, double initBalance = 0) : this(owner, DateTime.Now, initBalance)
        {
        }

        public void Close()
        {
            _Active = false;
            _ClosedDate = DateTime.Now;
        }

        public override string ToString()
        {
            if (_Active)
                return string.Format("ID: {0,-5} Opened Date: {1,-12} Balance: {2,-10:0,0.0} Owner: {3,-7} {4,-10}", _ID, _OpenedDate.ToShortDateString(), _Balance, _Owner.FirstName, _Owner.LastName);
            else
                return string.Format("ID: {0,-5} Opened Date: {1,-12} Balance: {2,-10:0,0.0} Owner: {3,-7} {4,-10} - Closed on {5}", _ID, _OpenedDate.ToShortDateString(), _Balance, _Owner.FirstName, _Owner.LastName, _ClosedDate.ToShortDateString());
        }

        public void ToStream(StreamWriter sw)
        {
            UpdateBalance();
            if (this.GetType() == typeof(Type1Account))
                sw.WriteLine(1);
            else
                sw.WriteLine(2);
            sw.WriteLine(_ID);
            sw.WriteLine(_Owner.ID);
            sw.WriteLine(_OpenedDate.ToShortDateString());
            if (_Active)
                sw.WriteLine();
            else
                sw.WriteLine(_ClosedDate.ToShortDateString());
            sw.WriteLine(_Balance);
        }

        // Abstract Methods
        public abstract double CalculateInterest();

        // Virtual Methods
        public virtual void Transfer(Account toAccount, double amount)
        {
            try
            {
                ValidationForTransferring(toAccount, amount);
                Balance -= amount;
                toAccount.Balance += amount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void UpdateBalance()
        {
            double interest = CalculateInterest();
            _Balance += interest;
        }


        // Validation methods
        protected virtual void ValidationForTransferring(Account destinationAccount, double amount)
        {
            Validation.ForTransferring(this, destinationAccount, amount);
        }

        // Factory methods
        public static Account CreateAccount(int accountType, Customer owner, DateTime openedDate, double initBalance = 0)
        {
            try
            {
                Validation.ForOpenedDate(openedDate);
                if (accountType == 1)
                    return Type1Account.CreateType1Account(owner, openedDate, initBalance);
                else if (accountType == 2)
                    return Type2Account.CreateType2Account(owner, openedDate, initBalance);
                else
                    throw new Exception(ErrorMessage.InvalidAccountType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Account CreateAccount(int accountType, Customer owner, double initBalance = 0)
        {
            return CreateAccount(accountType, owner, DateTime.Now, initBalance);
        }

        public static Account CreateAccount(int accountType, ulong accountID, Customer owner, DateTime openedDate, double initBalance = 0)
        {
            try
            {
                Validation.ForOpenedDate(openedDate);
                if (accountType == 1)
                    return Type1Account.CreateType1Account(accountID, owner, openedDate, initBalance);
                else if (accountType == 2)
                    return Type2Account.CreateType2Account(accountID, owner, openedDate, initBalance);
                else
                    throw new Exception(ErrorMessage.InvalidAccountType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Account CreateAccount(int accountType, ulong accountID, Customer owner, double initBalance = 0)
        {
            return CreateAccount(accountType, accountID, owner, DateTime.Now, initBalance);
        }

        public static Account CreateAccount(int accountType, List<Customer> customers, ulong ownerID, DateTime openedDate, double initBalance = 0)
        {
            Customer owner = Utility.SearchCustomer(customers, ownerID);
            if (owner == null)
                throw new Exception(ErrorMessage.NoMatchingCustomer);
            return CreateAccount(accountType, owner, openedDate, initBalance);
        }

        public static Account CreateAccount(int accountType, List<Customer> customers, ulong ownerID, double initBalance = 0)
        {
            return CreateAccount(accountType, customers, ownerID, DateTime.Now, initBalance);
        }

        private static Account CreateAccount(int accountType, ulong accountID, List<Customer> customers, ulong ownerID, DateTime openedDate, double initBalance = 0)
        {
            Customer owner = Utility.SearchCustomer(customers, ownerID);
            if (owner == null)
                throw new Exception(ErrorMessage.NoMatchingCustomer);
            return CreateAccount(accountType, accountID, owner, openedDate, initBalance);
        }

        private static Account CreateAccount(int accountType, ulong accountID, List<Customer> customers, ulong ownerID, double initBalance = 0)
        {
            return CreateAccount(accountType, accountID, customers, ownerID, DateTime.Now, initBalance);
        }

        public static Account CreateAccount(StreamReader sr, List<Customer> customers)
        {
            try
            {
                int accountType = Convert.ToInt32(sr.ReadLine());
                ulong accountID = Convert.ToUInt32(sr.ReadLine());
                ulong ownerID = Convert.ToUInt32(sr.ReadLine());
                DateTime openedDate = Convert.ToDateTime(sr.ReadLine());
                string closedDateString = sr.ReadLine();
                DateTime closedDate = DateTime.Now;
                if (closedDateString.Length != 0)
                    closedDate = Convert.ToDateTime(closedDateString);
                double balance = Convert.ToDouble(sr.ReadLine());
                Account acc = CreateAccount(accountType, accountID, customers, ownerID, openedDate, balance);
                if (closedDateString.Length != 0)
                {
                    acc._Active = false;
                    acc._ClosedDate = closedDate;
                }
                return acc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}