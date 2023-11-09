namespace MandelsBankenConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] menuOptions = {
                "Se dina konton och saldo",
                "Överföring mellan konton",
                "Ta ut pengar",
                "Sätt in pengar",
                "Öppna nytt konto",
                "Logga ut"
            };

            int selectedIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Meny");

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
                        ExecuteMenuOption(selectedIndex);
                        break;

                }
            }
        }
        static void ExecuteMenuOption(int optionIndex)
        {
            switch (optionIndex)
            {
                case 0:
                    Console.WriteLine("Gör menyval 1");
                    Console.ReadLine();
                    break;
                case 1:
                    Console.WriteLine("Gör menyval 2");
                    Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("Gör menyval 3");
                    Console.ReadLine();
                    break;
                case 3:
                    Console.WriteLine("Gör menyval 4");
                    Console.ReadLine();
                    break;
                case 4:
                    Console.WriteLine("Gör menyval 5");
                    Console.ReadLine();
                    break;
                case 5:
                    Console.WriteLine("Gör menyval 6");
                    Console.ReadLine();
                    break;
            }
        }
    }
}