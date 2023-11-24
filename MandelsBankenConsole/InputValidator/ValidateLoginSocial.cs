using System.Text.RegularExpressions;

namespace MandelsBankenConsole.InputValidator
{
    public class ValidateLoginSocial : IValidator
    {
        // validates the socialnumber on the main page, accepting only the string "admin" or socialnumber when written YYYYMMDD-XXXX
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
