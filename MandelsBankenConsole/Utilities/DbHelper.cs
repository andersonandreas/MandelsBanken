using MandelsBankenConsole.Data;
using MandelsBankenConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
