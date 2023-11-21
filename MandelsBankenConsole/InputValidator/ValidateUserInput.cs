using MandelsBankenConsole.Utilities;

namespace MandelsBankenConsole.InputValidator
{
    public class ValidateUserInput : IValidateUserInput
    {
        private readonly IValidator _charValidator;
        private readonly IValidator _numberValidator;
        private readonly IValidator _socialNumber;
        private readonly IValidator _checkLoginSocialAndAdmin;


        public ValidateUserInput(IValidator charValidator, IValidator numericValidator,
            IValidator socialNumber, IValidator checkLoginSocialAndAdmin)
        {
            _charValidator = charValidator;
            _numberValidator = numericValidator;
            _socialNumber = socialNumber;
            _checkLoginSocialAndAdmin = checkLoginSocialAndAdmin;
        }


        public string BaseCurrency() =>
            ValidateInput("base currency", 3, 4, _charValidator);

        public string TargetCurrency() =>
            ValidateInput("target currency", 3, 4, _charValidator);

        public string CodeCurrency() =>
    ValidateInput("currency for account", 3, 4, _charValidator);

        public string AccountName() =>
            ValidateInput("account name", 5, 50, _charValidator);

        public string FullName() =>
            ValidateInput("first and lastname", 3, 50, _charValidator);


        // only if I have time.
        // maybe add some more here to check how many tries the user trues to log in then let he user contect the bank ater some failed tries.
        public string Pin() =>
            ValidateInput("Pid code", 4, 5, _numberValidator);

        public string SocialNumber() =>
            ValidateInput("(social security number YYYYMMDD-XXXX)", 13, 13, _socialNumber);




        public decimal Amount()
        {
            string input = ValidateInput("amount", 1, 11, _numberValidator);

            if (decimal.TryParse(input, out decimal amount))
            {
                return amount;
            }

            // i need to fix this.....
            Console.WriteLine("Invalid input for amout...");
            return 0;
        }


        public decimal Amount(string messange)
        {

            // Enter: fidfjfjdjfdfj
            string input = ValidateInput(messange, 1, 11, _numberValidator);

            if (decimal.TryParse(input, out decimal amount))
            {
                return amount;
            }

            // i need to fix this.....
            Console.WriteLine("Invalid input for amout...");
            return 0;
        }



        // this method takes a message to show the user and a min/max length range valid for the input,
        // and what kind of check (number or character).
        // !! dont use this method directly the above method uses this for diffrent kind of validations. !!
        private string ValidateInput(string promptUser, int minLength, int maxLength, IValidator validator)
        {

            //Console.Clear();
            bool valid = false;
            string input = default;

            Console.Write($"Enter {promptUser}: ");
            input = Console.ReadLine()?.Trim() ?? string.Empty;

            // har vi kollar admin som for en special behadnling StringComparison.OrdinalIgnoreCase gomfor admin utan att bry sig om stora ller smo bokstaver vi kan skriva aDMin etc.
            if (string.Equals(input, "admin", StringComparison.OrdinalIgnoreCase))
            {
                if (input.Length == 5)
                {
                    valid = true;
                }
                else
                {
                    ConsoleHelper.PrintColorRed($"Invalid length for {promptUser}: {input}");
                    return ValidateInput(promptUser, minLength, maxLength, validator);
                }
            }
            else
            {
                while (!valid)
                {
                    int inputLength = input.Length;

                    if (inputLength >= minLength && inputLength <= maxLength && validator.Validate(input))
                    {
                        valid = true;
                    }
                    else
                    {
                        ConsoleHelper.PrintColorRed($"This is not a valid {promptUser}: {input}");
                        return ValidateInput(promptUser, minLength, maxLength, validator);
                    }
                }
            }

            return input;
        }



    }
}
