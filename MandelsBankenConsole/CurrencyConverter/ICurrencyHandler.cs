namespace MandelsBankenConsole.CurrencyConverter
{
    public interface ICurrencyHandler
    {
        Task<(decimal ConvertResult, string Information)> ConvertBetweenUserAccount(string baseCurrency, string targetCurrency, decimal amount);
        Task<(decimal ConvertResult, string Information)> RunExchange();
    }
}
