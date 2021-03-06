using Mc2.CrudTest.Application.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudTest.Application.Dtos
{
    public class CustomerRequestDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
   
        [PhoneNumberValidation(countryCode:"IR")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [BankAccountNumberValidation]
        public string BankAccountNumber { get; set; }
    }
}
