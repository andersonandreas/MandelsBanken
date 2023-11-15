namespace MandelsBankenConsole.CurrencyConverter
{
    public interface ICurrencyHandler
    {
        Task<(decimal ConvertResult, string Information)> ConvertBetweenCurrencies(string baseCurrency, string targetCurrency, decimal amount);
        Task<(decimal ConvertResult, string Information)> RunExchange();
    }
}
