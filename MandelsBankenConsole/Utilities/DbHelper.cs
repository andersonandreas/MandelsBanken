using MandelsBankenConsole.Data;
using MandelsBankenConsole.Models;
using Microsoft.EntityFrameworkCore;

namespace MandelsBankenConsole.Utilities
{
    internal static class DbHelper
    {
        public static User GetUserByLogInInput(BankenContext context, string userLogInInput)
        {
            return context.Users
                .SingleOrDefault(u => u.SocialSecurityNumber == userLogInInput);
        }

        public static List<User> GetAllUsers(BankenContext context)
        {
            List<User> users = context.Users.ToList();
            return users;
        }

        public static bool AddUser(BankenContext context, User user)
        {
            context.Users.Add(user);
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                ConsoleHelper.PrintColorRed($"Error adding user: {e}");
                return false;
            }
            return true;
        }

        public static List<Account> GetAllAccounts(BankenContext context, User user)
        {

            return context.Accounts
               .Include(acc => acc.Currency)
               .Where(acc => acc.UserId == user.Id)
               .ToList();

        }

        public static List<string> GetAccountInformation(List<Account> accounts, bool balanceOverOwner = true)
        {
            // Creates a list of string with descriptive information on the chosen accounts

            if (balanceOverOwner)
            {
                return accounts
                    .Select(a => $"Account: {a.AccountNumber} - {a.AccountName} | Balance: {a.Balance:# ##0.##} {a.Currency.CurrencyCode}")
                    .ToList();
            }

            return accounts
                .Select(a => $"Account: {a.AccountNumber} - {a.AccountName} | Owner: {a.User.CustomerName} | Currency: {a.Currency.CurrencyCode}")
                .ToList();
        }
        public static bool UpdateBalance(BankenContext context, Account account, decimal amount)
        {

            // Updates balance of an account after a transaction
            // amount is negative if money is drawn from the account

            account.Balance = Math.Round(account.Balance + amount, 2);

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                ConsoleHelper.PrintColorRed($"Error updating balance: {e}");
                return false;
            }

            return true;

        }
        public static bool AddTransaction(BankenContext context, Account account, decimal amount, string transactionInfo)
        {

            // Adds transaction to the Db after account's balance was updated

            Transaction newTransaction = new Transaction()
            {
                TransactionAmount = amount,
                Balance = account.Balance,
                Description = transactionInfo,
                Date = DateTime.Now,
                AccountId = account.Id,

            };
            context.Transactions.Add(newTransaction);
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                ConsoleHelper.PrintColorRed($"Error adding transaction: {e}");
                return false;
            }
            return true;
        }
        public static bool MakeTransaction(BankenContext context, Account account, decimal amount, string transactionInfo)
        {
            // Validate if user has enough money for the transaction, updates account's balance, adds a transaction

            if (account.Balance + amount < 0)
            {
                ConsoleHelper.PrintColorRed($"You don't have enough funds in your account to perform this transaction.");
                ConsoleHelper.PrintColorRed($"Your balance is {account.Balance:# ##0.##} {account.Currency.CurrencyCode}, you try to use {-amount:# ##0.##} {account.Currency.CurrencyCode}.");
                return false;
            }

            if (!UpdateBalance(context, account, amount))
            {
                return false;
            }

            if (!AddTransaction(context, account, amount, transactionInfo))
            {
                return false;
            }

            return true;
        }


        // just a linq queryy to start up a connetion behind the scenes.
        // so when the user is provding succeful login details, the user are directly loged in so the user not waiting for the database connection to appeare.

        public static void FastenUp()
        {
            using (BankenContext context = new BankenContext())
            {
                context.Users
                   .Any();
            }
        }



    }
}