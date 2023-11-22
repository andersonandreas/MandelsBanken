using MandelsBankenConsole.MandelBankApp;
using MandelsBankenConsole.Utilities;

namespace MandelsBankenConsole
{
    public class Program
    {
        static async Task Main(string[] args)
        {

            try
            {

                var firstConnectionBehindScenes = Task.Run(() => DbHelper.FastenUp());

                AppBank appBank = new AppBank();
                appBank.Run();
                await firstConnectionBehindScenes;

            }
            catch (Exception)
            {
                Console.WriteLine("The application closing..");
            }

        }


    }



}












