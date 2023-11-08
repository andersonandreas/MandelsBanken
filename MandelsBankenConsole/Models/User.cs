namespace MandelsBankenConsole.Models
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SocialNumber { get; set; }
        public string Pin { get; set; }


        public virtual ICollection<Account> Accounts { get; set; }
    }
}
