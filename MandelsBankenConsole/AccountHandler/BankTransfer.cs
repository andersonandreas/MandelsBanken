using MandelsBankenConsole.CurrencyConverter;
using MandelsBankenConsole.Data;
using MandelsBankenConsole.InputValidator;
using MandelsBankenConsole.Models;
using MandelsBankenConsole.Utilities;

namespace MandelsBankenConsole.AccountHandler
{
    public class BankTransfer
    {

        private readonly BankenContext _bankenContext;
        private readonly ExchangeCurrency _conversion;
        private readonly IValidateUserInput _validateUserInput;
        private readonly MenuFunctions _menuFunctions = new MenuFunctions();


        public BankTransfer(BankenContext bankenContext, ExchangeCurrency conversion, IValidateUserInput validateUserInput)
        {
            _bankenContext = bankenContext;
            _conversion = conversion;
            _validateUserInput = validateUserInput;
        }



        public async void MakeTransfer(User loggedInUser)
        {
            // "Transfer between accounts" has been chosen

            // User gets a choice to either transfer between user's own accounts or to another person
            // Transaction is always made in currency of the receiving account

            List<Account> userAccounts = DbHelper.GetAllAccounts(_bankenContext, loggedInUser);
            List<string> userAccountsDescription = DbHelper.GetAccountInformation(userAccounts);
            int choiceFrom = 0, choiceTo = 0, choiceToOther = 0;


            // Choose the account to make transfer from

            // if no accounts, go back to menu
            // if more than one account, let user choose from the list
            // if one account, choose automatically this one

            if (userAccounts.Count == 0)
            {
                ConsoleHelper.PrintColorRed("You don't have any accounts yet :(");
            }
            else if (userAccounts.Count > 1)
            {
                choiceFrom = _menuFunctions.ShowMenu(userAccountsDescription, "Which account do you want to transfer from?");
            }
            // if user only has 1 account, this is the default "from-account"=0, otherwise the chosen one
            Account chosenFromAccount = userAccounts[choiceFrom];


            // Choose the account to make transfer to

            // list of user's other accounts (apart from the one to make transfer from)
            // option to transfer to another person (then choose from a list of all other users' accounts)

            userAccountsDescription.RemoveAt(choiceFrom);
            userAccounts.RemoveAt(choiceFrom);
            userAccountsDescription.Add("Transfer to another person");
            choiceTo = _menuFunctions.
                ShowMenu(userAccountsDescription, $"Transfer from account {chosenFromAccount.AccountNumber} - {chosenFromAccount.AccountName} to?");
            Account chosenToAccount = new Account();

            // if transfer to another person (last option on the list), get a list of all other users' accounts
            List<Account> allOtherAccounts = new List<Account>();
            if (choiceTo == userAccountsDescription.Count - 1)
            {
                List<User> allUsers = DbHelper.GetAllUsers(_bankenContext);
                foreach (User user in allUsers)
                {
                    if (user.Id != loggedInUser.Id)
                    {
                        Console.WriteLine(user.CustomerName);
                        allOtherAccounts.AddRange(DbHelper.GetAllAccounts(_bankenContext, user));
                    }
                }
                List<string> allOtherAccountsDesc = DbHelper.GetAccountInformation(allOtherAccounts, false);
                choiceToOther = _menuFunctions.ShowMenu(allOtherAccountsDesc, $"Transfer from account {chosenFromAccount.AccountNumber} {chosenFromAccount.AccountName} to?");
                chosenToAccount = allOtherAccounts[choiceToOther];
            }
            // otherwise assign own to-account
            else
            {
                chosenToAccount = userAccounts[choiceTo];
            }


            // Choose the amount for transfer (using receiving account's currency)


            Console.WriteLine($"Transfer from {chosenFromAccount.AccountNumber} - {chosenFromAccount.AccountName} ({chosenFromAccount.Currency.CurrencyCode}) to {chosenToAccount.AccountNumber} - {chosenToAccount.AccountName} ({chosenToAccount.Currency.CurrencyCode})");
            Console.Write($"Enter amount to transfer (in {chosenToAccount.Currency.CurrencyCode}): ");
            decimal amount = _validateUserInput.Amount();
            decimal amountFrom = 0, amountTo = 0;
            string transactionInfoFrom, transactionInfoTo;

            // amount in the outgoing's and incoming accounts currencies
            if (chosenToAccount.Currency.CurrencyCode == chosenFromAccount.Currency.CurrencyCode)
            {
                amountFrom = amount;
                amountTo = amount;
            }
            else
            {
                // method for currency exchange
                var conversionResult = Task.Run(() => _conversion.ConvertCurrency(chosenToAccount.Currency.CurrencyCode, chosenFromAccount.Currency.CurrencyCode, amount)).Result;
                var (resultIndecimal, infoDescription) = conversionResult;

                amountFrom = Math.Round(resultIndecimal, 2);// nice bank, not stealing the roundups
                amountTo = amount;
            }

            // Make transaction

            // Two transactions are happening and both need to be successful
            // First transactions is associated with the outgoing account
            // Second transaction is associated with the receiving account

            transactionInfoFrom = $"Transfer {amountFrom} {chosenFromAccount.Currency.CurrencyCode} to account {chosenToAccount.AccountNumber} {chosenToAccount.AccountName}";
            transactionInfoTo = $"Transfer {amountTo} {chosenToAccount.Currency.CurrencyCode} from account {chosenFromAccount.AccountNumber} {chosenFromAccount.AccountName}";

            bool transactionSucceeded = DbHelper.MakeTransaction(_bankenContext, chosenFromAccount, -amountFrom, transactionInfoFrom);
            if (transactionSucceeded)
            {
                if (DbHelper.MakeTransaction(_bankenContext, chosenToAccount, amountTo, transactionInfoTo))
                {
                    ConsoleHelper.PrintColorGreen("Bank transfer successful!");
                    ConsoleHelper.PrintColorGreen($"{DbHelper.GetAccountInformation(new List<Account>() { chosenFromAccount })[0]}");
                    ConsoleHelper.PrintColorGreen($"{DbHelper.GetAccountInformation(new List<Account>() { chosenToAccount })[0]}");
                }
            }
        }
    }
}

