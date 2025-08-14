using System;
using System.ComponentModel.DataAnnotations;

namespace QuizManagment.Models.Validation
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public FutureDateAttribute() : base("The date must be today or in the future.")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime)
            {
                // Compare only the date part (ignoring time)
                if (dateTime.Date < DateTime.Now.Date)  // Disallow past dates but allow today
                {
                    return new ValidationResult(ErrorMessage ?? "The date must be today or in the future.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
