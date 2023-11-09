using MandelsBankenConsole.Models;
using Microsoft.EntityFrameworkCore;


namespace MandelsBankenConsole.Data
{
    internal class BankenContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Currency> Currencies { get; set; }





        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\.;Initial Catalog=BankenTest;Integrated Security=True;Pooling=False");
        }

    }
}
