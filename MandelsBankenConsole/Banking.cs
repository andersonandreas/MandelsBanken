using MandelsBankenConsole.Data;
using MandelsBankenConsole.Models;
using MandelsBankenConsole.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandelsBankenConsole
{
    internal class Banking
    {
        public static void BankTransfer(/*BankenContext context, User user*/)
        {
            // user has chosen "Transfer between accounts"
            // we give choice to either transfer between user's own accounts or to another person

            // Magda. idea
            // first: menu/list of accounts to transfer FROM (all of the user's accounts if more than one)
            // title "transfer from account NUMBER + NAME.." (either the chosen one or user's only one)
            // choose account to transfer to (list of the other accounts + "transfer to another person"
            // questions: validation for another person: do we need personnummer or do we need to know account number?

            var context = new BankenContext();
            User user1 = context.Users.Where(i => i.Id == 3).Single();
            List<Account> accounts = DbHelper.GetAllAccounts(context, user1);
            List<string> accountsDesc = DbHelper.GetAccountInformation(accounts);

            // if no accounts, go back to menu
            if (accounts.Count == 0)
            {
                Console.WriteLine("You dont have any accounts yet :(");
            }
            // if more than 1 account, choose from the list
            else if (accounts.Count > 1)
            {
                MenuFunctions.ShowMenu(accountsDesc.ToArray(), "Which account do you want to tranfer from?", ChooseAccount);
            }
            
            // if user only has 1 account, this is the "from-account"


            // list of accounts to transfer TO + option to transfer to somebody else
            List<string> accountsTo = DbHelper.GetAccountInformation(accounts);
            accountsDesc.Add("Transfer to another person");
            MenuFunctions.ShowMenu(accountsDesc.ToArray(), "Which account do you want to tranfer to?");

        }

        public static bool ChooseAccount(int optionIndex)
        {
            Console.WriteLine("Chosen account: " + optionIndex);
            return true;
        }

    }
}
