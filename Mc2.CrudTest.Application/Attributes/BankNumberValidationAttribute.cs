using System;
using System.ComponentModel.DataAnnotations;
using IbanNet;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Mc2.CrudTest.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class BankAccountNumberValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var validator = new IbanValidator();
            var validationResult = validator.Validate(value.ToString());
            if (validationResult.IsValid)
                return ValidationResult.Success; // ("NL91 ABNA 0417 1643 00");
            else
                return new ValidationResult("BankAccountNumber is invalid!");
        }
    }
}
