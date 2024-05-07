using System.ComponentModel.DataAnnotations;
using Garage3.Models.Entities;
using Humanizer;

namespace Garage3.Validation
{
    public class CheckFirstName : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            const string errorMessage = "First name and last name can't be the same!";


            if (value is string input)
            {

                if (validationContext.ObjectInstance is Customer customer)
                {
                    return string.Equals(customer.LastName, input, StringComparison.OrdinalIgnoreCase) ? new ValidationResult(errorMessage) : ValidationResult.Success;
                }
            }

            return new ValidationResult("Something went wrong!");
        }

    }
}
