using System;
using System.Collections.Generic;
using System.Text;

namespace Banking
{
    // This class implements generic validations
    class Validation
    {
        /************** Validation of attributes *******************/
        public static void ForYOB(DateTime dob)
        {
            if (DateTime.Now.Year - dob.Year < 16)
                throw new Exception(ErrorMessage.InvalidYOB);
        }

        public static void ForDOB(string inputDOB)
        {
            try
            {
                DateTime dob = Convert.ToDateTime(inputDOB);
                ForYOB(dob);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ForFirstName(string firstName)
        {
            if (firstName.Length == 0)
                throw new Exception(ErrorMessage.InvalidFirstName);
        }

        public static void ForLastName(string lastName)
        {
            if (lastName.Length == 0)
                throw new Exception(ErrorMessage.InvalidLastName);
        }

        public static void ForAddress(string address)
        {
            if (address.Length == 0)
                throw new Exception(ErrorMessage.InvalidAddress);
        }

        public static void ForContact(string contact)
        {
            bool valid = true;
            if (contact.Length > 0 && contact.Length != 10)
                valid = false;
            else
            {
                for (int i = 0; i < contact.Length; i++)
                    if (!Char.IsDigit(contact[i]))
                    {
                        valid = false;
                        break;
                    }
            }
            if (!valid)
                throw new Exception(ErrorMessage.InvalidContact);
        }

        public static void ForBalance(double balance)
        {
            if (balance < 0)
                throw new Exception(ErrorMessage.NegativeBalance);
        }

        public static void ForOpenedDate(DateTime date)
        {
            if (DateTime.Compare(DateTime.Now, date) < 0)
                throw new Exception(ErrorMessage.InvalidOpenedDate);
        }
        /************** Validation of attributes *******************/

        /************** Validation for opertions/transactions ******/
        /************** Validations related to Customer ************/
        public static void ForCreatingCustomer(string firstName, string lastName, string address, DateTime dob, string contact, string email)
        {
            try
            {
                ForFirstName(firstName);
                ForLastName(lastName);
                ForAddress(address);
                ForYOB(dob);
                ForContact(contact);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /************** Validations related to Customer ************/

        /************** Validations related to Account ************/
        public static void ForCalculatingInterest(Account account)
        {
            if (!account.Active)
                throw new Exception(ErrorMessage.InactiveAccount);
        }

        public static void ForGettingClosedDate(Account account)
        {
            if (account.Active)
                throw new Exception(ErrorMessage.ActiveAccount);
        }

        public static void ForTransferring(Account sourceAccount, Account destinationAccount, double amount)
        {
            if (!sourceAccount.Active || !destinationAccount.Active)
                throw new Exception(ErrorMessage.InactiveAccount);
            if (amount <= 0)
                throw new Exception(ErrorMessage.NegativeAmount);
            if (amount > sourceAccount.Balance)
                throw new Exception(ErrorMessage.ExceededAmount);
        }
        /************** Validations related to Account ************/

        /************** Validations related to Type1Account ************/
        public static void ForDeposit(Type1Account account, double amount)
        {
            if (!account.Active)
                throw new Exception(ErrorMessage.InactiveAccount);
            if (amount <= 0)
                throw new Exception(ErrorMessage.NegativeAmount);
        }

        public static void ForWithdrawal(Type1Account account, double amount)
        {
            if (!account.Active)
                throw new Exception(ErrorMessage.InactiveAccount);
            if (amount <= 0)
                throw new Exception(ErrorMessage.NegativeAmount);
            if (amount > account.Balance)
                throw new Exception(ErrorMessage.ExceededAmount);
        }
        /************** Validations related to Type1Account ************/

        /************** Validations related to Type2Account ************/
        public static void ForMonthlyDeposit(Type2Account account, double amount)
        {
            if (!account.Active)
                throw new Exception(ErrorMessage.InactiveAccount);
            if (amount <= 0)
                throw new Exception(ErrorMessage.NegativeAmount);
        }
        /************** Validations related to Type1Account ************/
        /************** Validation for opertions/transactions ******/
    }
}
