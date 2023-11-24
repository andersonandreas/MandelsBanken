using MandelsBankenConsole.CurrencyConverter;
using MandelsBankenConsole.Data;
using MandelsBankenConsole.InputValidator;
using MandelsBankenConsole.MenuInteraction;
using MandelsBankenConsole.Models;
using MandelsBankenConsole.Utilities;

namespace MandelsBankenConsole.AccountHandler
{
    public class DepositMoneyFunctions
    {
        private readonly BankenContext _bankenContext;
        private ExchangeCurrency _conversion;
        private readonly IValidateUserInput _validateUserInput;
        private MenuFunctions _menuFunctions = new();

        public DepositMoneyFunctions(BankenContext bankenContext, ExchangeCurrency conversion, IValidateUserInput validateUserInput)
        {
            _conversion = conversion;
            _bankenContext = bankenContext;
            _validateUserInput = validateUserInput;
        }

        public async void DepositMoney(User loggedInUser)
        {
            string depositDescription = "Deposit";

            List<Account> userAccounts = DbHelper.GetAllAccounts(_bankenContext, loggedInUser);

            //menu
            List<string> userAccountsDescription = DbHelper.GetAccountInformation(userAccounts);
            int selectedMenuOption = _menuFunctions.ShowMenu(userAccountsDescription, "Which account would you like to deposit to?");
            Account selectedAccount = userAccounts[selectedMenuOption];

            //stores info about deposited money
            decimal depositedMoney = _validateUserInput.Amount("how much money would you like to deposit?");
            decimal amount = depositedMoney;
            string currencyInput = _validateUserInput.CurrencyCodeUserInput().ToUpper();

            //currency exchange if not same currency as in account
            bool currencyConverted = false;
            if (currencyInput != selectedAccount.Currency.CurrencyCode)
            {
                currencyConverted = true;

                var conversionResult = Task.Run(() => _conversion.ConvertCurrency(currencyInput, selectedAccount.Currency.CurrencyCode, amount)).Result;

                var (resultIndecimal, infoDescription) = conversionResult;

                amount = resultIndecimal;
                depositDescription = $"{depositDescription}, {infoDescription}";
            }

            //Makes transaction, adds transaction history and prints reciept to the Console of what was done
            decimal balanceBefore = selectedAccount.Balance;
            if (DbHelper.MakeTransaction(_bankenContext, selectedAccount, amount, depositDescription))
            {
                Console.Clear();
                if (currencyConverted)
                {
                    ConsoleHelper.PrintColorGreen($"Deposit amount: {depositedMoney:# ##0.##} {currencyInput}\nConverted to: {amount:# ##0.##} {selectedAccount.Currency.CurrencyCode}\nDeposited to account: {selectedAccount.AccountNumber} - {selectedAccount.AccountName}\nBalance before: {balanceBefore:# ##0.##} {selectedAccount.Currency.CurrencyCode}\nNew balance: {selectedAccount.Balance:# ##0.##} {selectedAccount.Currency.CurrencyCode}");
                }
                else
                {
                    ConsoleHelper.PrintColorGreen($"Deposit amount: {depositedMoney:# ##0.##} {selectedAccount.Currency.CurrencyCode}\nDeposited to account: {selectedAccount.AccountNumber} - {selectedAccount.AccountName}\nBalance before: {balanceBefore:# ##0.##} {selectedAccount.Currency.CurrencyCode}\nNew balance: {selectedAccount.Balance:# ##0.##} {selectedAccount.Currency.CurrencyCode}");
                }
            }

            Console.WriteLine("Press enter to return to main menu.");
        }
    }
}