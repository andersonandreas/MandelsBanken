namespace MandelsBankenConsole.Models
{
    internal class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CurrencyId { get; set; }
        public int TransactionId { get; set; }
        public int AccountNumber { get; set; }
        public string Name { get; set; }
        public string AcountType { get; set; }
        public double Balance { get; set; }



        public virtual Currency Currency { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }



    }
}



