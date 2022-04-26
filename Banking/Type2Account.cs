using System;
using System.Collections.Generic;
using System.Text;

namespace Banking
{
    class Type2Account : Account
    {
        /**************** Attributes ****************/
        private static double _BasicInterestRate = 3.0;
        public static double BasicInterestRate
        {
            get { return _BasicInterestRate; }
        }

        private static double _DepositInterestRate = 4.0;
        public static double DepositInterestRate
        {
            get { return _DepositInterestRate; }
        }

        private double _MonthlyDeposit = 0;
        public double MonthlyDeposit
        {
            get { return _MonthlyDeposit; }
            set {
                try
                {
                    Validation.ForMonthlyDeposit(this, value);
                    _MonthlyDeposit = value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /**************** Methods ****************/
        // Constructor 1
        public Type2Account(ulong accountID, Customer owner, DateTime openedDate, double initBalance = 0) : base(accountID, owner, openedDate, initBalance)
        {
        }

        // Constructor 2
        public Type2Account(Customer owner, DateTime openedDate, double initBalance = 0) : base(owner, openedDate, initBalance)
        {
        }

        // Constructor 3
        public Type2Account(Customer owner, double initBalance = 0) : base(owner, initBalance)
        {
        }

        public override double CalculateInterest()
        {
            try
            {
                Validation.ForCalculatingInterest(this);

                // reference date
                DateTime refDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                TimeSpan span;

                if (DateTime.Compare(OpenedDate, refDate) < 0)
                    span = DateTime.Now.Subtract(refDate);
                else
                    span = DateTime.Now.Subtract(OpenedDate);

                return _BasicInterestRate / 365 / 100 * span.Days * Balance + _DepositInterestRate / 365 / 100 * span.Days * _MonthlyDeposit;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void ValidationForTransferring(Account destinationAccount, double amount)
        {
            base.ValidationForTransferring(destinationAccount, amount);
            if (destinationAccount.Owner != this.Owner)
                throw new Exception(ErrorMessage.InvalidCustomer);
            if (destinationAccount.GetType() != typeof(Type1Account))
                throw new Exception(ErrorMessage.InvalidTransferredAccount);
        }

        public override void UpdateBalance()
        {
            base.UpdateBalance(); // this calls the UpdateBalance in Account class
            _MonthlyDeposit = 0;
        }

        // Factory methods
        public static Type2Account CreateType2Account(Customer owner, DateTime openedDate, double initBalance = 0)
        {
            return new Type2Account(owner, openedDate, initBalance);
        }

        public static Type2Account CreateType2Account(Customer owner, double initBalance = 0)
        {
            return new Type2Account(owner, initBalance);
        }

        public static Type2Account CreateType2Account(ulong accountID, Customer owner, DateTime openedDate, double initBalance = 0)
        {
            return new Type2Account(accountID, owner, openedDate, initBalance);
        }

        public static Type2Account CreateType2Account(ulong accountID, Customer owner, double initBalance = 0)
        {
            return new Type2Account(accountID, owner, DateTime.Now, initBalance);
        }

        public static Type2Account CreateType2Account(List<Customer> customers, ulong ownerID, DateTime openedDate, double initBalance = 0)
        {
            return (Type2Account) Account.CreateAccount(2, customers, ownerID, openedDate, initBalance);
        }

        public static Type2Account CreateType2Account(List<Customer> customers, ulong ownerID, double initBalance = 0)
        {
            return CreateType2Account(customers, ownerID, DateTime.Now, initBalance);
        }
    }
}
