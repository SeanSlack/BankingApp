using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Banking
{
    class Utility
    {
        public static Customer SearchCustomer(List<Customer> customers, ulong ID)
        {
            foreach (Customer c in customers)
                if (c.ID == ID)
                    return c;
            return null;
        }

        public static Account SearchAccount(List<Account> accounts, ulong ID)
        {
            foreach (Account a in accounts)
                if (a.ID == ID)
                    return a;
            return null;
        }

        public static bool DoesExist(List<Customer> customers, string firstName, string lastName)
        {
            foreach (Customer c in customers)
                if (c.FirstName.ToLower() == firstName.ToLower() && c.LastName.ToLower() == lastName.ToLower())
                    return true;
            return false;
        }

        public static List<Customer> SearchCustomers(List<Customer> customers, string firstName, string lastName, string address, DateTime dob, string contact, string email)
        {
            List<Customer> results = new List<Customer>();
            foreach (Customer c in customers)
                if (c.FirstName == firstName || c.LastName == lastName || c.Address == address || c.DOB == dob || c.Contact == contact || c.Email == email)
                    results.Add(c);
            return results;
        }
    }
}
