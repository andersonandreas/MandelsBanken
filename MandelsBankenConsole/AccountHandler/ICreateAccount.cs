using MandelsBankenConsole.Models;

namespace MandelsBankenConsole.UserHandler

{
    public interface ICreateAccount
    {

        public void CreateAccount(User user, int accountNumber,
             string accountName, AccountType type, decimal initialDepo, int currencyId);
        public void RunAccountCreation();
        public void RunAccountCreation(User currentUser);
        public int GenerateAccountNum();
        public string NameAccount();
        public AccountType AccountTypeChoice();
        public int CurrencyForAccount();

    }

}
