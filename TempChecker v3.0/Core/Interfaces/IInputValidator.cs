using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempChecker.Core.Interfaces
{
    public interface IInputValidator
    {
        ValidationResult Validate(string input);
    }

    public class ValidationResult
    {
        public bool IsValid { get; }
        public int? Value { get; }
        public string ErrorMessage { get; }

        private ValidationResult(bool isValid, int? value, string errorMessage)
        {
            IsValid = isValid;
            Value = value;
            ErrorMessage = errorMessage;
        }

        public static ValidationResult Success(int value) => new(true, value, string.Empty);
        public static ValidationResult Failure(string errorMessage) => new(false, null, errorMessage);
    }
}