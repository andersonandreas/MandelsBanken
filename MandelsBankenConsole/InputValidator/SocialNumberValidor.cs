using System.Text.RegularExpressions;

namespace MandelsBankenConsole.InputValidator
{
    public class SocialNumberValidor : IValidator
    {

        // validate the socialnumber input its only allowing input like this = 19950414-3484.
        public bool Validate(string input)
        {
            return Regex.IsMatch(input, @"^\d{8}-\d{4}$");
        }


    }


}

