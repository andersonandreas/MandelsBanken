using MandelsBankenConsole.CurrencyConverter;

namespace MandelsBankenConsole
{
    internal partial class Program
    {
        public class TrancactionsHandler
        {
            private readonly CurrencyHandler _exchangeHandler;
            public TrancactionsHandler(CurrencyHandler exchangeHandler)
            {
                _exchangeHandler = exchangeHandler;
            }
            // Method takes baseCurrency, targetCurrency and the amount as arguements
            //
            public async Task MakeTranscation(string baseCurrency, string targetCurrency, decimal amount)
            {
                var (convertResult, InfoForDesc) = await _exchangeHandler.ConvertBetweenUserAccount(
                    baseCurrency.ToUpper(),
                    targetCurrency.ToUpper(),
                    amount);



                // put your other methods here and input this values into it.
                // convertResult = decimal value after conversion
                // InfoForDesc = string with the description
                // just logging for show. can remove it, if you dont need it.
                Console.WriteLine(convertResult);
                Console.WriteLine(InfoForDesc);
            }
        }
    }

}