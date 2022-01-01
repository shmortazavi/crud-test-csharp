using Mc2.CrudTest.Domain.SeedWorks;
using System;

namespace Mc2.CrudTest.Domain.Customers
{
    public class Customer : Entity, IAggregateRoot
    {
        #region Fields
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string BankAccountNumber { get; private set; }
        #endregion

        #region Ctor
        private Customer(string firstName, string lastName, DateTime dateOfBirth,
                         string phoneNumber, string email, string bankAccountNumber)
        {
            CheckInveriants(firstName, lastName, dateOfBirth, phoneNumber, email, bankAccountNumber);

            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Email = email;
            BankAccountNumber = bankAccountNumber;
        }
        #endregion

        #region Methods
        public static Customer Create(string firstName, string lastName, DateTime dateOfBirth,
                                      string phoneNumber, string email, string bankAccountNumber)
        {
            return new Customer(firstName, lastName, dateOfBirth, phoneNumber, email, bankAccountNumber)
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now
            };
        }

        public static Customer Update(Customer customer, string firstName, string lastName,
                                      DateTime dateOfBirth, string phoneNumber, string email,
                                      string bankAccountNumber)
        {
            CheckInveriants(firstName, lastName, dateOfBirth, phoneNumber, email, bankAccountNumber);

            customer.FirstName = firstName;
            customer.LastName = lastName;
            customer.DateOfBirth = dateOfBirth;
            customer.PhoneNumber = phoneNumber;
            customer.Email = email;
            customer.UpdatedDate = DateTime.Now;
            customer.BankAccountNumber = bankAccountNumber;

            return customer;
        }

        private static void CheckInveriants(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string email, string bankAccountNumber)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new Exception("firstName is required!");

            if (string.IsNullOrEmpty(lastName))
                throw new Exception("lastName is required!");

            if (dateOfBirth > DateTime.Now)
                throw new Exception("dateOfBirth should be less than now!");

            if (string.IsNullOrEmpty(phoneNumber))
                throw new Exception("phoneNumber is required!");

            if (string.IsNullOrEmpty(email))
                throw new Exception("email is required!");

            if (string.IsNullOrEmpty(bankAccountNumber))
                throw new Exception("bankAccountNumber is required!");
        }
        #endregion
    }
}
