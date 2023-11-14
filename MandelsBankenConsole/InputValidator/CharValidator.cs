using System.Text.RegularExpressions;

namespace MandelsBankenConsole.InputValidator
{
    public class CharValidator : IValidator
    {
        // validate the input, only characters allowed
        public bool Validate(string input) =>
            Regex.IsMatch(input, @"^[a-zA-Z\s]+$");
    }
}
