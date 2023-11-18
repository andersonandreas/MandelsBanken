namespace MandelsBankenConsole.API
{

    public interface IAPIDataReaderCurrency
    {
        Task<string> Read(string baseCurrency, string targetCurrency, decimal amount);
    }


}

