namespace MandelsBankenConsole.Models
{
    internal class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public double CurrentBalance { get; set; }


        // navaiton property = foreign key 
        public virtual Account Account { get; set; }

    }
}
