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

            //CurrencyHandler exchangeHandler = new CurrencyHandler(
            //    userInputValidator,
            //    apiDataReader);

            //ExchangeCurrency transaction = new ExchangeCurrency(exchangeHandler);




            // Mine account class. Yours gonna be here so that we can inject them into MenuFunctions class.
            AccountManager accountManager = new AccountManager(userInputValidator, context, new Random());

            MenuFunctions menuFunctions = new MenuFunctions(context, accountManager);

            menuFunctions.LogIn();






        }
    }

}
