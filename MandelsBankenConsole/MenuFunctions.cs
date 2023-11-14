namespace MandelsBankenConsole
{
    internal static class MenuFunctions
    {
        public static void ShowMenu()
        {
            string[] menuOptions = {
                "See your accounts and balance",
                "Transfer between accounts",
                "Withdraw money",
                "Deposit money",
                "Open a new account",
                "Log out"
            };

            int selectedIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu");

                for (int i = 0; i < menuOptions.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.Write("-> ");
                    }
                    else
                    {
                        Console.Write("   ");
                    }

                    Console.WriteLine(menuOptions[i]);
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedIndex = (selectedIndex - 1 + menuOptions.Length) % menuOptions.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedIndex = (selectedIndex + 1) % menuOptions.Length;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        if (!ExecuteMenuOption(selectedIndex))
                        {
                            break;
                        }
                        break;

                }
            }
        }

        private static bool ExecuteMenuOption(int optionIndex)
        {
            switch (optionIndex)
            {
                case 0:
                    Console.WriteLine("Does first option...");
                    Console.ReadLine();
                    break;
                case 1:
                    Console.WriteLine("Does second option...");
                    Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("Does third option...");
                    Console.ReadLine();
                    break;
                case 3:
                    Console.WriteLine("Does fourth option...");
                    Console.ReadLine();
                    break;
                case 4:
                    Console.WriteLine("Does fifth option...");
                    Console.ReadLine();
                    break;
                case 5:
                    Console.WriteLine("Logging out...");
                    Thread.Sleep(1000);
                    return false;
            }
            return true;
        }


        public static void LogIn()
        {
            Console.WriteLine("Welcome to Mandelsbank!");
            Thread.Sleep(1000);

            Console.WriteLine("Making banking smooth as almond milk");
            Thread.Sleep(1000);

            Console.WriteLine("Please log in");

            Console.Write("Enter user name:");
            string userName = Console.ReadLine();

            Console.Write("Enter pin code:");
            string pin = Console.ReadLine();

            if (userName == "admin")
            {
                if (pin != "1234")
                {
                    Console.WriteLine("Wrong password!");
                    return;
                }

                AdminFunctions.DoAdminTasks();
                return;
            }
            else //We need to add user-log in before letting them into this user-menu. Just made it kinda functional for now :)
            {

                ShowMenu();
            }
        }


    }
}
