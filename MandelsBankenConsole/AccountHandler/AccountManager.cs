using MandelsBankenConsole.Data;
using MandelsBankenConsole.InputValidator;
using MandelsBankenConsole.Models;

namespace MandelsBankenConsole.UserHandler
{
    public class AccountManager
    {

        private readonly IValidateUserInput _validateUserInput;
        private readonly BankenContext _bankenContext;
        private readonly Random _random;

        const int MinAccountNum = 10000000;
        const int MaxAccountNum = 100000000;


        public AccountManager(IValidateUserInput validateUserInput,
            BankenContext bankenContext, Random random)
        {
            _validateUserInput = validateUserInput;
            _bankenContext = bankenContext;
            _random = random;
        }


        public void RunAccountCreation(User currentUser)
        {
            var number = GenerateAccountNum();
            var name = NameAccount();
            var type = AccountTypeChoice();
            var initialDepo = _validateUserInput.Amount();
            var currencyId = IntCurrencyId();

            CreateAccount(currentUser, number, name, type, initialDepo, currencyId);
        }


        private void CreateAccount(User user, int accountNumber,
            string accountName, AccountType type, decimal initialDepo, int currencyId)
        {
            try
            {
                var newAccount = new Account()
                {
                    UserId = user.Id,
                    AccountNumber = accountNumber,
                    AccountName = accountName,
                    Type = type,
                    Balance = initialDepo,
                    CurrencyId = currencyId

                };

                _bankenContext.Accounts.Add(newAccount);
                _bankenContext.SaveChanges();
                Console.WriteLine($"{user.CustomerName} you opened a new account ({accountName} with account number {accountNumber}).");
            }

            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }



        private int GenerateAccountNum()
        {
            int accountNumber = _random.Next(MinAccountNum, MaxAccountNum);
            return accountNumber;
        }

        // validates the account name only characters allowed with 5 - 50 length min/max.
        public string NameAccount() => _validateUserInput.AccountName();


        private AccountType AccountTypeChoice()
        {

            Console.WriteLine("Account type:");
            Console.WriteLine("1 = Checking");
            Console.WriteLine("2 = Savings");

            switch (Console.ReadLine())
            {
                case "1":
                    return AccountType.Checking;
                case "2":
                    return AccountType.Savings;
                default:
                    return AccountType.Savings;
            }

        }


        private int IntCurrencyId()
        {

            var currencyId = _validateUserInput.CodeCurrency();

            return _bankenContext.Currencies
             .Where(c => c.CurrencyCode == currencyId)
             .Select(c => c.Id)
             .FirstOrDefault();
        }

    }
}

