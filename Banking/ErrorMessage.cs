using System;
using System.Collections.Generic;
using System.Text;

namespace Banking
{
    class ErrorMessage
    {
        private static string _InvalidYOB = "Customer must be at least 16 years old!";
        public static string InvalidYOB
        {
            get { return _InvalidYOB; }
        }

        private static string _InvalidDOBFormat = "Invalid DOB!";
        public static string InvalidDOBFormat
        {
            get { return _InvalidDOBFormat; }
        }

        private static string _InvalidContact = "Invalid contact!";
        public static string InvalidContact
        {
            get { return _InvalidContact; }
        }

        private static string _InvalidFirstName = "Invalid first name";
        public static string InvalidFirstName
        {
            get { return _InvalidFirstName; }
        }

        private static string _InvalidLastName = "Invalid last name!";
        public static string InvalidLastName
        {
            get { return _InvalidLastName; }
        }

        private static string _InvalidAddress = "Invalid address!";
        public static string InvalidAddress
        {
            get { return _InvalidAddress; }
        }

        private static string _InactiveAccount = "This transaction cannot be applied to inactive accounts!";
        public static string InactiveAccount
        {
            get { return _InactiveAccount; }
        }

        private static string _ActiveAccount = "This account is still active!";
        public static string ActiveAccount
        {
            get { return _ActiveAccount; }
        }

        private static string _NegativeAmount = "Amount must be greater than 0!";
        public static string NegativeAmount
        {
            get { return _NegativeAmount; }
        }

        private static string _ExceededAmount = "Amount cannot exceed balance!";
        public static string ExceededAmount
        {
            get { return _ExceededAmount; }
        }

        private static string _NegativeBalance = "Balance must be greater than 0!";
        public static string NegativeBalance
        {
            get { return _NegativeBalance; }
        }

        private static string _InvalidOpenedDate = "Opened date must be on or before the current date!";
        public static string InvalidOpenedDate
        {
            get { return _InvalidOpenedDate; }
        }

        private static string _InvalidCustomer = "Cannot transfer to another account of a different customer!";
        public static string InvalidCustomer
        {
            get { return _InvalidCustomer; }
        }

        private static string _InvalidDeposit = "Deposit cannot be applied to a Type 2 account!";
        public static string InvalidDeposit
        {
            get { return _InvalidDeposit; }
        }

        private static string _InvalidWithdrawal = "Withdrawal cannot be applied to a Type 2 account!";
        public static string InvalidWithdrawal
        {
            get { return _InvalidWithdrawal; }
        }

        private static string _InvalidMonthlyDeposit = "Monthly deposit cannot be applied to a Type 1 account!";
        public static string InvalidMonthlyDeposit
        {
            get { return _InvalidMonthlyDeposit; }
        }

        private static string _InvalidTransferredAccount = "Cannot transfer to a Type 2 account!";
        public static string InvalidTransferredAccount
        {
            get { return _InvalidTransferredAccount; }
        }

        private static string _InvalidAccountType = "Account type must be 1 or 2!";
        public static string InvalidAccountType
        {
            get { return _InvalidAccountType; }
        }

        private static string _NoMatchingCustomer = "No matching customer!";
        public static string NoMatchingCustomer
        {
            get { return _NoMatchingCustomer; }
        }

        private static string _ExistingCustomer = "This customer exists!";
        public static string ExistingCustomer
        {
            get { return _ExistingCustomer; }
        }
    }
}
