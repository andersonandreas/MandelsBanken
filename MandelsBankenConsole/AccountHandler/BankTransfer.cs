using MandelsBankenConsole.CurrencyConverter;
using MandelsBankenConsole.Data;
using MandelsBankenConsole.Models;
using MandelsBankenConsole.Utilities;

namespace MandelsBankenConsole.AccountHandler
{
    public class BankTransfer
    {

        private readonly BankenContext _bankenContext;
        private readonly ExchangeCurrency _conversion;
        private readonly MenuFunctions _menuFunctions = new MenuFunctions();


        public BankTransfer(BankenContext bankenContext, ExchangeCurrency conversion)
        {
            _bankenContext = bankenContext;
            _conversion = conversion;
        }





        public async void MakeTransfer(User loggedInUser)
        {
            // user has chosen "Transfer between accounts"
            // we give choice to either transfer between user's own accounts or to another person

            // Magda. idea
            // first: menu/list of accounts to transfer FROM (all of the user's accounts if more than one)
            // title "transfer from account NUMBER + NAME.." (either the chosen one or user's only one)
            // choose account to transfer to (list of the other accounts + "transfer to another person"
            // questions: validation for another person: do we need personnummer or do we need to know account number?


            List<Account> userAccounts = DbHelper.GetAllAccounts(_bankenContext, loggedInUser);
            List<string> userAccountsDesc = DbHelper.GetAccountInformation(userAccounts);
            // if user only has 1 account, this is the default "from-account-nr"=0
            int choiceFrom = 0, choiceTo = 0, choiceToOther = 0;

            // if no accounts, go back to menu
            if (userAccounts.Count == 0)
            {
                Console.WriteLine("You dont have any accounts yet :(");
            }
            // if more than 1 account, choose from the list
            else if (userAccounts.Count > 1)
            {
                choiceFrom = _menuFunctions.ShowMenu(userAccountsDesc.ToArray(), "Which account do you want to transfer from?");
            }

            // if user only has 1 account, this is the default "from-account"=0, otherwise the chosen one
            Account chosenFromAccount = userAccounts[choiceFrom];

            // list of other accounts to transfer TO + option to transfer to another person
            userAccountsDesc.RemoveAt(choiceFrom);
            userAccounts.RemoveAt(choiceFrom);
            userAccountsDesc.Add("Transfer to another person");
            choiceTo = _menuFunctions.ShowMenu(userAccountsDesc.ToArray(), $"Transfer from account {chosenFromAccount.AccountNumber} {chosenFromAccount.AccountName} to?");
            Account chosenToAccount = new Account();

            // if transfer to another person (last option on the list), get a list of all other users' accounts
            List<Account> allOtherAccounts = new List<Account>();
            if (choiceTo == userAccountsDesc.Count - 1)
            {
                List<User> allUsers = DbHelper.GetAllUsers(_bankenContext);
                foreach (User user in allUsers)
                {
                    if (user.Id != loggedInUser.Id)
                    // efter att vi fångat loggedInUser så kunde jag inte längre jämföra user!=loggedInUser för den första innehöll Accounts och den andra inte och jämflörelsen funkade inte längre. Fundera på det
                    {
                        Console.WriteLine(user.CustomerName);
                        allOtherAccounts.AddRange(DbHelper.GetAllAccounts(_bankenContext, user));
                    }
                }
                List<string> allOtherAccountsDesc = DbHelper.GetAccountInformation(allOtherAccounts);
                choiceToOther = _menuFunctions.ShowMenu(allOtherAccountsDesc.ToArray(), $"Transfer from account {chosenFromAccount.AccountNumber} {chosenFromAccount.AccountName} to?");
                chosenToAccount = allOtherAccounts[choiceToOther];
            }
            // otherwise assign own to-account but remember we removed one index from the list
            else
            {
                chosenToAccount = userAccounts[choiceTo];
            }
            Console.WriteLine($"\nTransfer from {chosenFromAccount.AccountName} ({chosenFromAccount.Currency.CurrencyCode}) to {chosenToAccount.AccountName} ({chosenToAccount.Currency.CurrencyCode})");
            Console.Write($"Enter amount to transfer (in {chosenToAccount.Currency.CurrencyCode}): ");
            int amount = int.Parse(Console.ReadLine());
            // först validate it is an amount!!
            // -- has Andreas a function for that?
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
                // ---------------------------------------------------
                // ---------------------------------------------------
                //Console.WriteLine("testar omvandlingen");
                // ---------------------------------------------------
                // ---------------------------------------------------


                // method for converting 
                var (resultIndecimal, infoDescription) = await _conversion.ConvertCurrency(chosenToAccount.Currency.CurrencyCode, chosenFromAccount.Currency.CurrencyCode, amount);

                //await Console.Out.WriteLineAsync(resultIndecimal.ToString());
                //await Console.Out.WriteLineAsync(infoDescription);


                // ---------------------------------------------------
                // ---------------------------------------------------

                amountFrom = Math.Round(resultIndecimal, 2);// nice bank, not stealing the roundups
                amountTo = amount;
            }



            transactionInfoFrom = $"Transfer {amountFrom} {chosenFromAccount.Currency.CurrencyCode} from this account";
            transactionInfoTo = $"Transfer {amountTo} {chosenToAccount.Currency.CurrencyCode} to this account";
            //Console.WriteLine(transactionInfoFrom);
            //Console.WriteLine(transactionInfoTo);

            bool transactionSucceeded = DbHelper.MakeTransaction(_bankenContext, chosenFromAccount, -amountFrom, transactionInfoFrom);
            if (transactionSucceeded)
            {
                if (DbHelper.MakeTransaction(_bankenContext, chosenToAccount, amountTo, transactionInfoTo))
                {
                    Console.WriteLine("Hurray!");
                    Console.WriteLine($"{DbHelper.GetAccountInformation(new List<Account>() { chosenFromAccount })[0]}");
                    Console.WriteLine($"{DbHelper.GetAccountInformation(new List<Account>() { chosenToAccount })[0]}");

                }
            }



        }

        // actually i do want to get an "int" from ShowMenu
        public static bool ChooseAccount(int optionIndex)
        {
            Console.WriteLine("Chosen account: " + optionIndex);
            return true;

        }

    }
}

