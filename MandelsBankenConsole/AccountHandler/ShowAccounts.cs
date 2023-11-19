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

            // Show the list with all accounts of the active user

            List<Account> userAccounts = DbHelper.GetAllAccounts(_bankenContext, loggedInUser);
            List<string> userAccountsDesc = DbHelper.GetAccountInformation(userAccounts);

            if (userAccounts.Count == 0)
            {
                Console.WriteLine("You dont have any accounts yet :(");
            }

            Console.WriteLine("Dina konton: ");
            foreach (string accountDesc in userAccountsDesc)
            {
                Console.WriteLine(accountDesc);
            }

        }
    }
}
