﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudTest.Application.Dtos
{
    public class CustomerRequestDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }
}
