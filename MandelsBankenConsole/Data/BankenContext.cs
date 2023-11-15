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



        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Account>().HasKey(x => x.id);

        //    // Other configurations...

        //    base.OnModelCreating(modelBuilder);
        //}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\.;Initial Catalog=BankSchool;Integrated Security=True;Pooling=False"); //Add your connection string here :)
        }

    }
}
