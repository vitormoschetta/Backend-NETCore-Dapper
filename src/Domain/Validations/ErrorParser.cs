using System.Linq;
using FluentValidation.Results;

namespace Domain.Validations
{
    public class ErrorParser
    {
        public static string GetErrorMessage(ValidationResult result)
        {
            string[] errors = new string[result.Errors.Count];

            for (int i = 0; i < result.Errors.Count; i++)
            {
                errors[i] = "Error: " + result.Errors[i].ErrorMessage;
            }

            return string.Join(" | ", errors);
        }
    }
}