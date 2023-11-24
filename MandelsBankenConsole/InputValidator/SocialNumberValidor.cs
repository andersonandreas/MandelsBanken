using System.Text.RegularExpressions;

namespace MandelsBankenConsole.InputValidator
{
    public class SocialNumberValidor : IValidator
    {

        // validates the socialnumber input, its only allowing input like this = YYYYMMDD-XXXX.
        public bool Validate(string input)
        {
            return Regex.IsMatch(input, @"^\d{8}-\d{4}$");
        }


    }


}

