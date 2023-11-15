namespace MandelsBankenConsole.Models
{
    public class User
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Pin { get; set; }

        // Navigation property
        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}

// A user can have multiple accounts, each account has refernce to a currency,
// all have a collection of transactions thats unique to that specifec account and all the transactions has a reference to the account it belongs to.



