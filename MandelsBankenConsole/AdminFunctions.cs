using MandelsBankenConsole.Data;
using MandelsBankenConsole.Models;
using MandelsBankenConsole.UserHandler;
using MandelsBankenConsole.Utilities;

namespace MandelsBankenConsole
{
    public class AdminFunctions
    {

        private readonly AccountManager _accountManager;
        private readonly BankenContext _bankenContext;

        public AdminFunctions(AccountManager accountManager, BankenContext bankenContext)
        {
            _accountManager = accountManager;
            _bankenContext = bankenContext;
        }


        // removed static. we need to acced the field and other methods in this class. cant do it with static
        public void DoAdminTasks()
        {

            Console.WriteLine("Current users in system:");
            List<User> users = DbHelper.GetAllUsers(_bankenContext);

            foreach (User user in users)
            {
                Console.WriteLine($"{user.CustomerName}");
            }

            Console.WriteLine($"Total number of users = {users.Count()}");
            Console.WriteLine("c to create new user");
            Console.WriteLine("x to exit");

            while (true)
            {
                Console.Write("Enter command:");
                string command = Console.ReadLine();

                switch (command.ToLower())
                {
                    case "c":
                        CreateUser();
                        break;
                    case "x":
                        return;
                    default:
                        Console.WriteLine($"Unknown command: {command}");
                        break;
                }
            }
        }


        private void CreateUser()
        {
            Console.WriteLine("Create user");
            Console.Write("Enter user name:");
            string username = Console.ReadLine();
            Console.Write("Enter user social number:");
            string socialNumber = Console.ReadLine();

            Random random = new Random();
            string pin = random.Next(1000, 10000).ToString();

            User newUser = new User()
            {
                CustomerName = username,
                SocialSecurityNumber = socialNumber,
                Pin = pin,
            };
            bool success = DbHelper.AddUser(_bankenContext, newUser);


            if (success)
            {
                Console.WriteLine($"Created user {username} with pin {pin}");
                _accountManager.StartUpNewUserAccount(newUser);

            }
            else
            {
                Console.WriteLine($"Failed to create user with username {username}");
            }
        }
    }
}
