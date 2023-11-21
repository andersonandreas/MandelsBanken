using MandelsBankenConsole.Data;
using MandelsBankenConsole.MandelBankApp;

namespace MandelsBankenConsole
{
    public class Program
    {
        static async Task Main(string[] args)
        {

            try
            {

                var firstConnectionBehindScenes = Task.Run(() => FastenUp());

                AppBank appBank = new AppBank();
                appBank.Run();
                await firstConnectionBehindScenes;

            }
            catch (Exception)
            {
                await Console.Out.WriteLineAsync("The appliaction closing..");
            }

        }


        // just a linq queryy to start up a connetion behind the scenes.
        // so when the user is provding succeful login details, the user are directly loged in so the user not waiting for the database connection to appeare.
        public static void FastenUp()
        {
            using (BankenContext context = new BankenContext())
            {
                context.Users
                   .Any();
            }



        }
    }



}












