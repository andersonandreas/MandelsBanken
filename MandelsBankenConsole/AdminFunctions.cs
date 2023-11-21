using MandelsBankenConsole.Data;
using MandelsBankenConsole.InputValidator;
using MandelsBankenConsole.Models;
using MandelsBankenConsole.UserHandler;
using MandelsBankenConsole.Utilities;

namespace MandelsBankenConsole
{
    public class AdminFunctions
    {

        private readonly AccountManager _accountManager;
        private readonly BankenContext _bankenContext;
        private readonly IValidateUserInput _validateUserInput;
        private readonly MenuFunctions _menuFunctions = new MenuFunctions();

        public AdminFunctions(AccountManager accountManager, BankenContext bankenContext,
            IValidateUserInput validateUserInput)
        {
            _accountManager = accountManager;
            _bankenContext = bankenContext;
            _validateUserInput = validateUserInput;
        }



        public void DoAdminTasks()
        {
            List<string> adminMenu = new List<string> { "Create new user", "Show all users", "Exit" };
           
            while (true)
            {
                var adminChoice = _menuFunctions.ShowMenu(adminMenu);
                switch (adminChoice)
                {
                    case 0:
                        CreateUser();
                        break;
                    case 1:
                        ShowAllUsers();
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("Exiting app");
                        return;
                }
            }
        }

        private void ShowAllUsers()
        {
            Console.Clear();
            Console.WriteLine("Current users in system:");
            List<User> users = DbHelper.GetAllUsers(_bankenContext);
            Console.WriteLine("-------------------------------------------");
            foreach (User user in users)
            {
                Console.WriteLine($"{user.CustomerName}");
            }
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"Total users: {users.Count()}");
            Console.WriteLine("-------------------------------------------");
        }

        private void CreateUser()
        {

            Random random = new Random();

            const int MinNumber = 0;
            const int MaxNumber = 10000;


            string pin = random.Next(MinNumber, MaxNumber).ToString().PadLeft(4, '0');

            User newUser = new User()
            {
                CustomerName = _validateUserInput.FullName(),
                SocialSecurityNumber = _validateUserInput.SocialNumber(),
                Pin = pin,
            };
            bool success = DbHelper.AddUser(_bankenContext, newUser);


            if (success)
            {
                Console.WriteLine($"Created user {newUser.CustomerName} with pin {pin}");
                _accountManager.StartUpNewUserAccount(newUser);

            }
            else
            {
                Console.WriteLine($"Failed to create user with username {newUser.CustomerName}");
            }
        }
    }
}
