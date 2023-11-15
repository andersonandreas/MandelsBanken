namespace MandelsBankenConsole.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }

        // Navigation property
        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}



