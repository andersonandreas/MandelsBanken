using MandelsBankenConsole.API;
using MandelsBankenConsole.CurrencyConverter;
using MandelsBankenConsole.InputValidator;

namespace MandelsBankenConsole
{
    internal partial class Program
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


            var (resultdecimal, info) = await transaction.ConvertCurrency("usd", "sek", 500000);
            Console.WriteLine(resultdecimal.ToString());
            Console.WriteLine(info);


            MenuFunctions.LogIn();





        }

        public class ExchangeCurrency
        {

            private readonly CurrencyHandler _exchangeHandler;

            public ExchangeCurrency(CurrencyHandler exchangeHandler)
            {
                _exchangeHandler = exchangeHandler;
            }


            public async Task<(decimal, string)> ConvertCurrency(string baseCurrency, string targetCurrency, decimal amount)
            {
                var (convertResult, InfoForDesc) = await _exchangeHandler.ConvertBetweenUserAccount(
                    baseCurrency.ToUpper(),
                    targetCurrency.ToUpper(),
                    amount);


                //put your other methods here and input this values into it.
                //convertResult = decimal value after conversion
                //InfoForDesc = string with the description
                //just logging for show.can remove it, if you dont need it.



                return (convertResult, InfoForDesc);

            }
        }
    }

}
