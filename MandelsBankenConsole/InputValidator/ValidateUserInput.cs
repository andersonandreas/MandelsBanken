namespace MandelsBankenConsole.InputValidator
{
    public class ValidateUserInput : IValidateUserInput
    {
        private readonly IValidator _charValidator;
        private readonly IValidator _numberValidator;

        public ValidateUserInput(IValidator charValidator, IValidator numericValidator)
        {
            _charValidator = charValidator;
            _numberValidator = numericValidator;
        }


        public string BaseCurrency() =>
            ValidateInput("base currency", 3, 4, _charValidator);

        public string TargetCurrency() =>
            ValidateInput("target currency", 3, 4, _charValidator);

        public decimal Amount()
        {
            string input = ValidateInput("amount", 1, 20, _numberValidator);

            if (decimal.TryParse(input, out decimal amount))
            {
                return amount;
            }

            // need to fix this.....
            Console.WriteLine("Invalid input for amout...");
            return 0;
        }


        // this method takes a message to show the user and a min/max length range valid for the input,
        // and what kind of check (number or character).
        // dont use this method directly the above method uses this for diffrent kind of validations.
        private string ValidateInput(string promptUser, int minLength, int maxLength, IValidator validator)
        {
            Console.Clear();

            bool valid = false;
            string input = string.Empty;

            while (!valid)
            {
                Console.Write($"Enter {promptUser}: ");

                input = Console.ReadLine()?.Trim() ?? string.Empty;

                if (!string.IsNullOrEmpty(input) && input.Length >= minLength
                    && input.Length < maxLength && validator.Validate(input))
                {
                    valid = true;
                }
                else
                {
                    Console.WriteLine($"This is not a valid {promptUser}: {input}");
                }
            }

            return input;
        }


    }
}
