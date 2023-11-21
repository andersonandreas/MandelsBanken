using MandelsBankenConsole.CurrencyConverter;
using MandelsBankenConsole.Data;
using MandelsBankenConsole.InputValidator;
using MandelsBankenConsole.Models;
using MandelsBankenConsole.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MandelsBankenConsole.AccountHandler
{
    public class DepositMoneyFunctions
    {
        private ExchangeCurrency _conversion;
        private readonly IValidateUserInput _validateUserInput;
        private MenuFunctions _menuFunctions = new();

        public DepositMoneyFunctions(ExchangeCurrency conversion, IValidateUserInput validateUserInput)
        {
            _conversion = conversion;
            _validateUserInput = validateUserInput;
        }

        public async void DepositMoney(User loggedInUser)
        {
            using (BankenContext context = new BankenContext())
            {
                string depositDescription = "Deposit";

                //adds the users accounts and its currencies to a list
                List<Account> usersAccounts = context.Accounts
                .Include(account => account.Currency)
                .Where(account => account.UserId == loggedInUser.Id)
                .ToList();

                //adds accountinformation to an array for the menu

                // jag la till currency code nar man valjer konto
                string[] usersAccountsMenuOptions = usersAccounts
                    .Select(account => $"{account.AccountName} - {account.AccountNumber} | {account.Currency.CurrencyCode} ")
                    .ToArray();

                //starts the menu // addded the  _menuFunctions,  instead of the call to the static class (MenuFunctions)
                int selectedMenuOption = _menuFunctions.ShowMenu(usersAccountsMenuOptions, "Which account would you like to deposit to?");
                Account selectedAccount = usersAccounts[selectedMenuOption];

                //gets amount to deposit
                Console.WriteLine("How much money would you like to deposit?");
                decimal depositedMoney = _validateUserInput.Amount();

                //gets currency and checks if it matches a currency in the database
                Console.WriteLine("What currency is it in? Write its 3 letter currency code");
                string currencyInput = Console.ReadLine().ToUpper();
                bool currencyCodeExists = context.Currencies.Any(c => c.CurrencyCode == currencyInput);

                if (!currencyCodeExists) //gives error if it is not in the database and returns to main menu
                {
                    ConsoleHelper.PrintColorRed("Error: Currency code does not exist");
                    //Console.WriteLine("Error: Currency code does not exist");
                    return;
                }
                else if (currencyInput != selectedAccount.Currency.CurrencyCode) //convert currency if not same as in account
                {
                    // andvander _conversion har istallet men do vi anvander ett klass field kan vi inte langre static
                    var (resultIndecimal, infoDescription) = await _conversion.ConvertCurrency(currencyInput, selectedAccount.Currency.CurrencyCode, depositedMoney);

                    selectedAccount.Balance += resultIndecimal; //adds deposited and converted amount to original balance
                    depositDescription = $"{depositDescription}, {infoDescription}"; //adds what was done to description to later add to transaction history
                }
                else //if currency is same as account its added to the original balance here
                {
                    selectedAccount.Balance += depositedMoney;
                }

                //Adds transaction history to the database
                Transaction transaction = new Transaction()
                {
                    TransactionAmount = depositedMoney,
                    Balance = selectedAccount.Balance,
                    Description = depositDescription,
                    Date = DateTime.Now,
                    AccountId = selectedAccount.Id
                };
                context.Transactions.Add(transaction);

                context.SaveChanges(); //saves changes to the database

                //Reciept to the Console of what was done
                //Console.WriteLine($"{depositedMoney} {currencyInput} deposited to account: {selectedAccount.AccountName} - {selectedAccount.AccountNumber}. \nNew balance: {selectedAccount.Balance} {selectedAccount.Currency.CurrencyCode}");
                ConsoleHelper.PrintColorGreen($"{depositedMoney} {currencyInput} deposited to account: {selectedAccount.AccountName} - {selectedAccount.AccountNumber}. \nNew balance: {selectedAccount.Balance} {selectedAccount.Currency.CurrencyCode}");
            }
        }





    }
}




