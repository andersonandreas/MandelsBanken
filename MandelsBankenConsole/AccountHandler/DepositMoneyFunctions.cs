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

            //adds the users accounts and its currencies to a list
            List<Account> userAccounts = DbHelper.GetAllAccounts(_bankenContext, loggedInUser);

            //adds accountinformation to an array for the menu
            List<string> userAccountsDescription = DbHelper.GetAccountInformation(userAccounts);

            //starts the menu // addded the  _menuFunctions,  instead of the call to the static class (MenuFunctions)
            int selectedMenuOption = _menuFunctions.ShowMenu(userAccountsDescription, "Which account would you like to deposit to?");
            Account selectedAccount = userAccounts[selectedMenuOption];

            //gets amount to deposit
            decimal depositedMoney = _validateUserInput.Amount("how much money would you like to deposit?");

            //gets currency and checks if it matches a currency in the database
            string currencyInput = _validateUserInput.CurrencyCodeUserInput();

            //convert currency if not same as in account, changes deposited amount to same currency, adds convertioninformation to the description
            if (currencyInput != selectedAccount.Currency.CurrencyCode)
            {

                var conversionResult = Task.Run(() => _conversion.ConvertCurrency(currencyInput, selectedAccount.Currency.CurrencyCode, depositedMoney)).Result;

                var (resultIndecimal, infoDescription) = conversionResult;

                depositedMoney = resultIndecimal;
                depositDescription = $"{depositDescription}, {infoDescription}";
            }

            //Makes transaction, adds transaction history and prints reciept to the Console of what was done
            if (DbHelper.MakeTransaction(_bankenContext, selectedAccount, depositedMoney, depositDescription))
            {
                Console.Clear();
                ConsoleHelper.PrintColorGreen($"{depositedMoney:# ##0.##} {currencyInput} deposited to account: {selectedAccount.AccountNumber} - {selectedAccount.AccountName}. \nNew balance: {selectedAccount.Balance:# ##0.##} {selectedAccount.Currency.CurrencyCode}");
            }

            Console.WriteLine("Press enter to return to main menu.");
        }


    }
}