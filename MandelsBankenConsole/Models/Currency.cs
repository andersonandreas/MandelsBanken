namespace MandelsBankenConsole.Models
{
    internal class Currency
    {
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; } //  store and manage the exchange rate information for each currency. = (sourceAccount.Currency.ExchangeRate, targetAccount.Currency.ExchangeRate);


    }
}



