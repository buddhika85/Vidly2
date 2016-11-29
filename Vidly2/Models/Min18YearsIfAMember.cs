using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly2.Models
{
    public sealed class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var customer = (CustomerFormViewModel)validationContext.ObjectInstance;
                // membership type not selected or pay as you go selected
                if (customer.MembershipTypeId == (byte)MembershipTypesEnum.Unknown ||
                    customer.MembershipTypeId == (byte)MembershipTypesEnum.PayAsYouGo)
                {
                    return ValidationResult.Success;
                }

                // not pay as you go and birthday not enterted
                if (!customer.BirthDate.HasValue)
                {
                    return new ValidationResult("Birthdate is required if membership type is not pay as you go");
                }

                // not pay as you go and birthday enterted
                var age = DateTime.Today.Year - customer.BirthDate.Value.Year;
                return (age >= 18) ?
                    ValidationResult.Success :
                    new ValidationResult("Customer should be atleast 18 years old to go on a membership");
            }
            catch (InvalidCastException)
            {
                var customer = (Customer)validationContext.ObjectInstance;
                // membership type not selected or pay as you go selected
                if (customer.MembershipTypeId == (byte)MembershipTypesEnum.Unknown ||
                    customer.MembershipTypeId == (byte)MembershipTypesEnum.PayAsYouGo)
                {
                    return ValidationResult.Success;
                }

                // not pay as you go and birthday not enterted
                if (!customer.BirthDate.HasValue)
                {
                    return new ValidationResult("Birthdate is required if membership type is not pay as you go");
                }

                // not pay as you go and birthday enterted
                var age = DateTime.Today.Year - customer.BirthDate.Value.Year;
                return (age >= 18) ?
                    ValidationResult.Success :
                    new ValidationResult("Customer should be atleast 18 years old to go on a membership");
            }
            
        }
    }
}