using System.Text.RegularExpressions;

namespace MandelsBankenConsole.InputValidator
{
    public class NumberValidator : IValidator
    {
        // validate the the input, only numbers allowed
        public bool Validate(string input) =>
            Regex.IsMatch(input, @"^[0-9]+$");
    }
}
