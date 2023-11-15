namespace MandelsBankenConsole.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal Balance { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        // FK
        public int AccountId { get; set; }

        // Navigation property
        public virtual Account Account { get; set; }
    }
}


