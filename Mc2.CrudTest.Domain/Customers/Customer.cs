﻿using Mc2.CrudTest.Domain.SeedWorks;
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
        #endregion

        #region Ctor
        public Customer(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string email)
        {
            CheckInveriants(firstName, lastName, dateOfBirth, phoneNumber, email);

            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;

        }
        #endregion

        #region Privates
        private static void CheckInveriants(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string email)
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
        }
        #endregion
    }
}