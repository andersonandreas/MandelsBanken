using MandelsBankenConsole.API;
using MandelsBankenConsole.CurrencyConverter;
using MandelsBankenConsole.InputValidator;

namespace MandelsBankenConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            IAPIDataReaderCurrency apiDataReader = new APIDataReaderCurrency();
            IValidateUserInput userInputValidator = new ValidateUserInput(
                new CharValidator(),
                new NumberValidator());

            CurrencyHandler exchangeHandler = new CurrencyHandler(
                userInputValidator,
                apiDataReader);

            ExchangeCurrency transaction = new ExchangeCurrency(exchangeHandler);


            // user log in method 

            MenuFunctions.LogIn();



            // method for converting 
            var (resultIndecimal, infoDescription) = await transaction.ConvertCurrency("usd", "sek", 500000);

            await Console.Out.WriteLineAsync(resultIndecimal.ToString());
            await Console.Out.WriteLineAsync(infoDescription);





        }
    }

}
