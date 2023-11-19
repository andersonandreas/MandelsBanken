using MandelsBankenConsole.Data;
using MandelsBankenConsole.Models;
using Microsoft.EntityFrameworkCore;

namespace MandelsBankenConsole.Utilities
{
    internal static class DbHelper
    {
        public static User GetUserByLogInInput(BankenContext context, string userLogInInput)
        {
            return context.Users.SingleOrDefault(u => u.SocialSecurityNumber == userLogInInput);
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
                Console.WriteLine($"Error adding user: {e}");
                return false;
            }
            return true;
        }

        public static List<Account> GetAllAccounts(BankenContext context, User user)
        {

            return context.Accounts
               .Include(acc => acc.Currency) // ChatGPT helped with adding Currency here cause of nullable fields
               .Where(acc => acc.UserId == user.Id)
               .ToList();

        }

        public static List<string> GetAccountInformation(List<Account> accounts, bool balance = true)
        {
            if (balance)
            {
                return accounts
                    .Select(a => $"Account: {a.AccountNumber} {a.AccountName}\tBalance: {a.Balance} {a.Currency.CurrencyCode}")
                    .ToList();
            }

            return accounts
                .Select(a => $"Account: {a.AccountNumber} {a.AccountName}\tCurrency: {a.Currency.CurrencyCode}")
                .ToList();
        }

        public static bool AddTransaction(BankenContext context, Account account, decimal amount, string transactionInfo)
        {

            // adds the transaction after the balance was updated

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
                Console.WriteLine($"Error adding transaction: {e}");
                return false;
            }
            return true;
        }

        public static bool UpdateBalance(BankenContext context, Account account, decimal amount)
        {

            // updates balance of an account

            account.Balance += amount;

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error updating balance: {e}");
                return false;
            }

            return true;

        }
        public static bool MakeTransaction(BankenContext context, Account account, decimal amount, string transactionInfo)
        {
            // validate if user has enough money for the transfer, update the balance, add a transaction

            if (account.Balance + amount < 0)
            {
                Console.WriteLine($"Not enough money :( your balance: {account.Balance} {account.Currency.CurrencyCode}, you try to transfer {-amount}");
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

    }
}