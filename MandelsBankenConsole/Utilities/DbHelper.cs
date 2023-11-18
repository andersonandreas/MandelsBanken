using MandelsBankenConsole.Data;
using MandelsBankenConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if (balance) { 
            return accounts
                .Select(a => $"Account: {a.AccountNumber} {a.AccountName}\tBalance: {a.Balance} {a.Currency.CurrencyCode}")
                .ToList();
            }

            return accounts
                .Select(a => $"Account: {a.AccountNumber} {a.AccountName}\tCurrency: {a.Currency.CurrencyCode}")
                .ToList();


        }


        public static int ReturnCurrencyIdFromCode(BankenContext context, string currencyCode)
        {
            return context.Currencies
                .Where(c => c.CurrencyCode == currencyCode)
                .Select(c => c.Id)
                .Single();

        }

      

    }
}
