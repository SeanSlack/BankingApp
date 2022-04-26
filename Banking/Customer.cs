using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Banking
{
    class Customer
    {
        /**************** Attributes ****************/
        // _Count holds the current number of accounts in the system
        private static ulong _Count = 0;

        private ulong _ID;
        public ulong ID
        {
            get { return _ID; }
        }

        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        private string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        private string _Address;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private DateTime _DOB;
        public DateTime DOB
        {
            get { return _DOB; }
            set
            {
                try
                {
                    Validation.ForYOB(value);
                    _DOB = value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private string _Contact;
        public string Contact
        {
            get { return _Contact; }
            set {
                    try
                    {
                        Validation.ForContact(value);
                        _Contact = value;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
        }

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private List<Account> _Accounts;

        /**************** Methods ****************/
        // Constructor 1
        private Customer(ulong customerID, string firstName, string lastName, string address, DateTime dob, string contact = "", string email = "")
        {
            _ID = customerID;
            _Count = Math.Max(_Count, _ID);
            _FirstName = firstName;
            _LastName = lastName;
            _Address = address;
            _DOB = dob;
            _Contact = contact;
            _Email = email;
            _Accounts = new List<Account>();
        }

        // Constructor 2
        private Customer(string firstName, string lastName, string address, DateTime dob, string contact = "", string email = "") : this(_Count + 1, firstName, lastName, address, dob, contact, email)
        {
        }

        // Copy constructor
        private Customer(Customer c) : this(c.FirstName, c.LastName, c.Address, c.DOB, c.Contact, c.Email)
        {
            _Accounts = new List<Account>(c._Accounts);
        }

        public void AddAccount(Account account)
        {
            _Accounts.Add(account);
        }

        public double SumBalance()
        {
            double sumBalance = 0;
            for (int i = 0; i < _Accounts.Count; i++)
                sumBalance += _Accounts[i].Balance;
            return sumBalance;
        }

        public override string ToString()
        {
            double totalBalance = SumBalance();
            return string.Format("ID: {0,-3} Name: {1,-7} {2,-10} Address: {3,-14} DOB: {4,-12} Contact: {5,-12} Email: {6,-18} Total balance: {7,-10:0,0.0}", _ID, _FirstName, _LastName, _Address, _DOB.ToShortDateString(), _Contact, _Email, totalBalance);
        }

        public void ToStream(StreamWriter sw)
        {
            sw.WriteLine(_ID);
            sw.WriteLine(_FirstName);
            sw.WriteLine(_LastName);
            sw.WriteLine(_Address);
            sw.WriteLine(_DOB.ToShortDateString());
            sw.WriteLine(_Contact);
            sw.WriteLine(_Email);
        }

        // Factory methods
        public static Customer CreateCustomer(string firstName, string lastName, string address, DateTime dob, string contact = "", string email = "")
        {
            try
            {
                Validation.ForCreatingCustomer(firstName, lastName, address, dob, contact, email);
                return new Customer(firstName, lastName, address, dob, contact, email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Customer CreateCustomer(StreamReader sr)
        {
            try
            {
                ulong customerID = Convert.ToUInt32(sr.ReadLine());
                string firstName = sr.ReadLine();
                string lastName = sr.ReadLine();
                string address = sr.ReadLine();
                DateTime dob = Convert.ToDateTime(sr.ReadLine());
                string contact = sr.ReadLine();
                string email = sr.ReadLine();
                Validation.ForCreatingCustomer(firstName, lastName, address, dob, contact, email);
                return new Customer(customerID, firstName, lastName, address, dob, contact, email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
