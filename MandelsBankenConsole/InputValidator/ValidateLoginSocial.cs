using System.Text.RegularExpressions;

namespace MandelsBankenConsole.InputValidator
{
    public class ValidateLoginSocial : IValidator
    {
        // valide the socialnumber on the main page execpting only 19950414-3484 and the word admin;
        public bool Validate(string input)
        {
            if (input.Trim().ToLower() == "admin")
            {

                return true;
            }
            return Regex.IsMatch(input, @"^\d{8}-\d{4}$");
        }
    }
}
