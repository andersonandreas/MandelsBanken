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

            List<Account> accounts = context.Users
                .Where(u => u.Id == user.Id)
                .Include(u => u.Accounts)
                .Single()
                .Accounts
                .ToList();
            return accounts;
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
