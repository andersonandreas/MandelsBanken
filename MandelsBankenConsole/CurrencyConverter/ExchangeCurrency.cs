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


            return (convertResult, InfoForDesc);
        }
    }
}


