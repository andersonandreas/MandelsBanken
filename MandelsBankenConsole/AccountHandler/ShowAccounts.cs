using MandelsBankenConsole.Data;
using MandelsBankenConsole.Models;
using MandelsBankenConsole.Utilities;

namespace MandelsBankenConsole.AccountHandler
{
    public class ShowAccount
    {

        private readonly BankenContext _bankenContext;

        public ShowAccount(BankenContext bankenContext)
        {
            _bankenContext = bankenContext;
        }


        public void ShowAccounts(User loggedInUser)
        {

            // User has chosen "See your accounts and balance"
            // Method shows a list of all accounts of the active user

            List<Account> userAccounts = DbHelper.GetAllAccounts(_bankenContext, loggedInUser);
            List<string> userAccountsDescription = DbHelper.GetAccountInformation(userAccounts);

            if (userAccounts.Count == 0)
            {
                Console.WriteLine("You don't have any accounts yet :(");
                return;
            }

            Console.WriteLine("Dina konton: ");
            foreach (string accountDesc in userAccountsDescription)
            {
                Console.WriteLine($"\t{accountDesc}");
            }

        }
    }
}
