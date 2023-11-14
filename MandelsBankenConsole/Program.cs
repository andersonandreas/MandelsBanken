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


            // This tuple method (ConvertResult, Information) return both a decimal value (the result from the exchange),
            // and a string with the information for the transaction description. 

            // !!!!!!!!!!!!!!!!!!!! comment this code out when you not working with the API. !!!!!!!!!!!!!!!!!!!!
            var (ConvertResult, Information) = await exchangeHandler.RunExchange();


            // The new value after the exchange in decimal
            Console.WriteLine(ConvertResult);

            // The information from the convert. save to the decription in the transaction
            Console.WriteLine(Information);



            // logging in menu
            //MenuFunctions.LogIn();

        }
    }

}