using MandelsBankenConsole.Data;
using MandelsBankenConsole.InputValidator;
using MandelsBankenConsole.Models;
using MandelsBankenConsole.Utilities;

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
            var name = "Checking account";
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


        private AccountType AccountTypeChoice() => AccountType.Savings;


        //  check if userinput exits in thedatabase from the userinput and if so returning a index of the currencycode and
        // should be making just one and with a overload insead of two methods..
        private int IdCurrency()
        {
            int currencyId = default;
            bool currencyExists;

            do
            {
                var currencyCode = _validateUserInput.CodeCurrency();

                currencyExists = _bankenContext.Currencies
                    .Any(c => c.CurrencyCode == currencyCode);

                if (!currencyExists)
                {
                    ConsoleHelper.PrintColorRed("Invalid currency code. Please try again.");
                }
                else
                {
                    currencyId = _bankenContext.Currencies
                        .Where(c => c.CurrencyCode == currencyCode)
                        .Select(c => c.Id)
                        .FirstOrDefault();
                }

            } while (!currencyExists);

            return currencyId;
        }




    }
}

