namespace MandelsBankenConsole.CurrencyConverter
{

    public class ExchangeCurrency
    {

        private readonly CurrencyHandler _exchangeHandler;

        public ExchangeCurrency(CurrencyHandler exchangeHandler)
        {
            _exchangeHandler = exchangeHandler;
        }


        public async Task<(decimal, string)> ConvertCurrency(string baseCurrency, string targetCurrency, decimal amount)
        {
            var (convertResult, InfoForDesc) = await _exchangeHandler.ConvertBetweenCurrencies(
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


