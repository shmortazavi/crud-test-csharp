using System;
using System.ComponentModel.DataAnnotations;
using PhoneNumbers;

namespace Mc2.CrudTest.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class PhoneNumberValidationAttribute : ValidationAttribute
    {
        public readonly string _countryCode;

        public PhoneNumberValidationAttribute(string countryCode)
        {
            _countryCode = countryCode;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (string.IsNullOrEmpty(value.ToString()))
                return new ValidationResult("PhoneNumber is invalid!");

            if ((string.IsNullOrEmpty(_countryCode)) || ((_countryCode.Length != 2) && (_countryCode.Length != 3)))
                return new ValidationResult("CountryCode is invalid!");

            var phoneUtil = PhoneNumberUtil.GetInstance();

            try
            {
                var phoneNumber = phoneUtil.Parse(value.ToString(), _countryCode);

                if (phoneUtil.IsValidNumber(phoneNumber))
                    return ValidationResult.Success;
                else
                    return new ValidationResult("PhoneNumber is invalid!");
            }
            catch (Exception ex)
            {
                return new ValidationResult(ex.Message);
            }
        }
    }
}
