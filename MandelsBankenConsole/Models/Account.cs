namespace MandelsBankenConsole.Models
{
    internal class Account
    {
        public int ID { get; set; }
        public int AccountNumber { get; set; }
        public string AccountName { get; set; }
        public AccountType Type { get; set; }
        public decimal Balance { get; set; }

        // FK
        public int UserId { get; set; }
        public int CurrencyId { get; set; }

        // Navigation properties
        public virtual Currency Currency { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }


    public enum AccountType
    {
        Checking = 1,
        Savings = 2,

    }



}




