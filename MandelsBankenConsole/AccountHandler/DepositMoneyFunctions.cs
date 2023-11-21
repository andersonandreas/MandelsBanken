using MandelsBankenConsole.CurrencyConverter;
using MandelsBankenConsole.Data;
using MandelsBankenConsole.Models;
using MandelsBankenConsole.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Principal;

namespace MandelsBankenConsole.AccountHandler
{
    public class DepositMoneyFunctions
    {
        private readonly BankenContext _bankenContext;
        private ExchangeCurrency _conversion;
        private MenuFunctions _menuFunctions = new();

        public DepositMoneyFunctions(BankenContext bankenContext, ExchangeCurrency conversion)
        {
            _conversion = conversion;
            _bankenContext = bankenContext;
        }

        public async void DepositMoney(User loggedInUser)
        {
            string depositDescription = "Deposit";

            //adds the users accounts and its currencies to a list
            List<Account> userAccounts = DbHelper.GetAllAccounts(_bankenContext, loggedInUser);

            //adds accountinformation to an array for the menu
            string[] usersAccountsMenuOptions = userAccounts
                .Select(account => $"{account.AccountName} - {account.AccountNumber} | {account.Currency.CurrencyCode} ")
                .ToArray();

            //starts the menu // addded the  _menuFunctions,  instead of the call to the static class (MenuFunctions)
            int selectedMenuOption = _menuFunctions.ShowMenu(usersAccountsMenuOptions, "Which account would you like to deposit to?");
            Account selectedAccount = userAccounts[selectedMenuOption];

            //gets amount to deposit
            Console.WriteLine("How much money would you like to deposit?");
            decimal depositedMoney = decimal.Parse(Console.ReadLine());

            //gets currency and checks if it matches a currency in the database
            Console.WriteLine("What currency is it in? Write its 3 letter currency code");
            string currencyInput = Console.ReadLine();

            //convert currency if not same as in account, changes deposited amount to same currency, adds convertioninformation to the description
            if (currencyInput != selectedAccount.Currency.CurrencyCode)
            {
                var (resultIndecimal, infoDescription) = await _conversion.ConvertCurrency(currencyInput, selectedAccount.Currency.CurrencyCode, depositedMoney);

                depositedMoney = resultIndecimal;
                depositDescription = $"{depositDescription}, {infoDescription}";
            }

            //Makes transaction, adds transaction history and prints reciept to the Console of what was done
            if (DbHelper.MakeTransaction(_bankenContext, selectedAccount, depositedMoney, depositDescription))
            {
                Console.WriteLine($"{depositedMoney} {currencyInput} deposited to account: {selectedAccount.AccountNumber} - {selectedAccount.AccountName}. \nNew balance: {selectedAccount.Balance} {selectedAccount.Currency.CurrencyCode}");
            }
        }
    }
}