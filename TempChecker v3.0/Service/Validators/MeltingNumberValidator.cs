using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using TempChecker.Core.Interfaces;

namespace TempChecker.Services.Validators
{
    public class MeltingNumberValidator : IInputValidator
    {
        private const int MAX_LENGTH = 9;
        private const int MIN_LENGTH = 1;
        private readonly Regex _digitPattern = new(@"^\d+$");

        public ValidationResult Validate(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return ValidationResult.Failure("Номер плавки не может быть пустым.");

            if (!_digitPattern.IsMatch(input))
                return ValidationResult.Failure("Номер плавки должен содержать только цифры.");

            if (input.Length < MIN_LENGTH || input.Length > MAX_LENGTH)
                return ValidationResult.Failure($"Номер плавки должен содержать от {MIN_LENGTH} до {MAX_LENGTH} цифр.");

            if (!int.TryParse(input, out int result))
                return ValidationResult.Failure("Некорректный формат номера плавки.");

            return ValidationResult.Success(result);
        }
    }
}