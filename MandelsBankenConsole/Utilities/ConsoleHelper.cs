namespace MandelsBankenConsole.Utilities
{
    public static class ConsoleHelper
    {

        public static void PrintColorRed(string input)
        {

            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(input);
            Console.ForegroundColor = originalColor;
        }


        public static void PrintColorGreen(string input)
        {

            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(input);
            Console.ForegroundColor = originalColor;
        }
    }
}
