using System;
using System.Collections.Generic;
using System.Text;

namespace Banking
{
    class Type1Account : Account
    {
        /**************** Attributes ****************/
        private static double _InterestRate = 2.0;
        public static double InterestRate
        {
            get { return _InterestRate; }
        }

        /**************** Methods ****************/
        // Constructor 1
        private Type1Account(ulong accountID, Customer owner, DateTime openedDate, double initBalance = 0) : base(accountID, owner, openedDate, initBalance)
        {
        }

        // Constructor 2
        private Type1Account(Customer owner, DateTime openedDate, double initBalance = 0) : base(owner, openedDate, initBalance)
        {
        }

        // Constructor 3
        private Type1Account(Customer owner, double initBalance = 0) : base(owner, initBalance)
        {
        }

        public void Deposit(double amount)
        {
            try
            {
                Validation.ForDeposit(this, amount);
                Balance += amount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Withdraw(double amount)
        {
            try
            {
                Validation.ForWithdrawal(this, amount);
                Balance -= amount;
            }
            catch (Exception ex)
            {
                throw ex;
            }    
        }

        public override double CalculateInterest()
        {
            try
            {
                Validation.ForCalculatingInterest(this);
            
                // reference date - the 1st day of the current month.
                DateTime refDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                TimeSpan span;

                if (DateTime.Compare(OpenedDate, refDate) < 0) // if opened date is before refDate
                    span = DateTime.Now.Subtract(refDate);
                else
                    span = DateTime.Now.Subtract(OpenedDate);

                return _InterestRate / 365 / 100 * span.Days * Balance;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Factory methods
        public static Type1Account CreateType1Account(Customer owner, DateTime openedDate, double initBalance = 0)
        {
            return new Type1Account(owner, openedDate, initBalance);
        }

        public static Type1Account CreateType1Account(Customer owner, double initBalance = 0)
        {
            return new Type1Account(owner, initBalance);
        }

        public static Type1Account CreateType1Account(ulong accountID, Customer owner, DateTime openedDate, double initBalance = 0)
        {
            return new Type1Account(accountID, owner, openedDate, initBalance);
        }

        public static Type1Account CreateType1Account(ulong accountID, Customer owner, double initBalance = 0)
        {
            return new Type1Account(accountID, owner, DateTime.Now, initBalance);
        }

        public static Type1Account CreateType1Account(List<Customer> customers, ulong ownerID, DateTime openedDate, double initBalance = 0)
        { 
            return (Type1Account) Account.CreateAccount(1, customers, ownerID, openedDate, initBalance);
        }

        public static Type1Account CreateType1Account(List<Customer> customers, ulong ownerID, double initBalance = 0)
        {
            return CreateType1Account(customers, ownerID, DateTime.Now, initBalance);
        }
    }
}
