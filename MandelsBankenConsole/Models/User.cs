namespace MandelsBankenConsole.Models
{
    internal class User
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Pin { get; set; }

        // Navigation property
        public virtual ICollection<Account> Accounts { get; set; }
    }
}

