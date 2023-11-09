namespace MandelsBankenConsole
{
    internal class Program
    {
        static void Main(string[] args)
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
                MenuFunctions menu = new MenuFunctions();
                MenuFunctions.ShowMenu();
            }
        }
    }
}