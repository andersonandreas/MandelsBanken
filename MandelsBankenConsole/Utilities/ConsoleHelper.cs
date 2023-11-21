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


        // not workin yet..
        public static void TimeReaming()
        {

            int froozenSystemCount = 3000;
            DateTime started = DateTime.Now;

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(froozenSystemCount / 10);
                TimeSpan timeFromFroozen = DateTime.Now - started;

                TimeSpan timeLeft = TimeSpan.FromMilliseconds(froozenSystemCount) - timeFromFroozen;

                string froozenTimeLeft = timeLeft.TotalSeconds >= 0
                ? $"Remaining time: {timeLeft.TotalSeconds:F2} seconds"
                : "unlock try log in again now";

                Console.WriteLine(froozenTimeLeft);
            }
        }



    }
}

