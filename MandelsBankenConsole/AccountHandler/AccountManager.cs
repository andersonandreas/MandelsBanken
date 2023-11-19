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

        public AccountManager() { }

        public AccountManager(IValidateUserInput validateUserInput,
            BankenContext bankenContext, Random random)
        {
            _validateUserInput = validateUserInput;
            _bankenContext = bankenContext;
            _random = random;
        }


        public void CreateAccount(User currentUser)
        {
            var number = GenerateAccountNum();
            var name = _validateUserInput.AccountName();
            var type = AccountTypeChoice();
            var initialDepo = _validateUserInput.Amount();
            var currencyId = IdCurrency();

            RunAccountCreation(currentUser, number, name, type, initialDepo, currencyId);
        }


        public void StartUpNewUserAccount(User currentUser)
        {
            var sekCurrency = _bankenContext.Currencies
                .Where(u => u.CurrencyCode == "SEK")
                .Select(u => u.Id)
                .FirstOrDefault();




            var number = GenerateAccountNum();
            var name = "Defualt checking account";
            var type = AccountType.Checking;
            var initialDepo = 0m;
            var currencyId = sekCurrency;

            RunAccountCreation(currentUser, number, name, type, initialDepo, currencyId);
        }


        private void RunAccountCreation(User user, int accountNumber,
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
                Console.WriteLine($"{user.CustomerName} you opened a new account ({accountName} " +
                    $"with account number {accountNumber}).");
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


        private int IdCurrency()
        {
            var currencyId = _validateUserInput.CodeCurrency();

            return _bankenContext.Currencies
             .Where(c => c.CurrencyCode == currencyId)
             .Select(c => c.Id)
             .FirstOrDefault();
        }

    }
}

