using MandelsBankenConsole.Data;
using MandelsBankenConsole.InputValidator;
using MandelsBankenConsole.UserHandler;

namespace MandelsBankenConsole
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var context = new BankenContext();
            //IAPIDataReaderCurrency apiDataReader = new APIDataReaderCurrency();
            IValidateUserInput userInputValidator = new ValidateUserInput(
                new CharValidator(),
                new NumberValidator());



            // istallet for alla rader i era klasser 
            var exchange = CurrencyInitExchange.InitCurrencyHandler();

            // allt detta kommer flyttas till en klass dar vi kor voran applicaton och sedan startar vi den klassen har 
            // i program.
            AccountManager accountManager = new AccountManager(userInputValidator, context, new Random());

            DepositMoneyFunctions depositMoneyFunctions = new DepositMoneyFunctions(exchange);
            MenuFunctions menuFunctions = new MenuFunctions(context, accountManager, depositMoneyFunctions);



            menuFunctions.LogIn();





        }




    }
}












