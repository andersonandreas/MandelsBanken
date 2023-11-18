using MandelsBankenConsole.API;
using MandelsBankenConsole.CurrencyConverter;
using MandelsBankenConsole.Data;
using MandelsBankenConsole.InputValidator;
using MandelsBankenConsole.Models;
using Microsoft.EntityFrameworkCore;

namespace MandelsBankenConsole
{
    public class DepositMoneyFunctions
    {
        public static async void DepositMoney(User loggedInUser)
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
                string[] usersAccountsMenuOptions = usersAccounts
                    .Select(account => $"{account.AccountName} - {account.AccountNumber}")
                    .ToArray();

                //starts the menu
                int selectedMenuOption = MenuFunctions.ShowMenu(usersAccountsMenuOptions, "Which account would you like to deposit to?");
                Account selectedAccount = usersAccounts[selectedMenuOption];

                //gets amount to deposit
                Console.WriteLine("How much money would you like to deposit?");
                decimal depositedMoney = Decimal.Parse(Console.ReadLine());

                //gets currency and checks if it matches a currency in the database
                Console.WriteLine("What currency is it in? Write its 3 letter currency code");
                string currencyInput = Console.ReadLine().ToUpper();
                bool currencyCodeExists = context.Currencies.Any(c => c.CurrencyCode == currencyInput);

                if (!currencyCodeExists) //gives error if it is not in the database and returns to main menu
                {
                    Console.WriteLine("Error: Currency code does not exist");
                    return;
                }
                else if (currencyInput != selectedAccount.Currency.CurrencyCode) //convert currency if not same as in account
                {
                    IAPIDataReaderCurrency apiDataReader = new APIDataReaderCurrency();
                    IValidateUserInput userInputValidator = new ValidateUserInput(
                        new CharValidator(),
                        new NumberValidator());

                    CurrencyHandler exchangeHandler = new CurrencyHandler(
                        userInputValidator,
                        apiDataReader);

                    ExchangeCurrency conversion = new ExchangeCurrency(exchangeHandler);

                    var (resultIndecimal, infoDescription) = await conversion.ConvertCurrency(currencyInput, selectedAccount.Currency.CurrencyCode, depositedMoney);
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
                Console.WriteLine($"{depositedMoney} {currencyInput} deposited to account: {selectedAccount.AccountName} - {selectedAccount.AccountNumber}. \nNew balance: {selectedAccount.Balance} {selectedAccount.Currency.CurrencyCode}");
            }
        }
    }
}