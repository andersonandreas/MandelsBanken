using MandelsBankenConsole.Models;
using Microsoft.EntityFrameworkCore;


namespace MandelsBankenConsole.Data
{
    internal class BankenContext : DbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<Transaction> Transaction { get; set; }
        DbSet<Currency> Currency { get; set; }





        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\.;Initial Catalog=MandelBanken;Integrated Security=True;Pooling=False");
        }

    }
}
