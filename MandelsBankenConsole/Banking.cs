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
    internal static class Banking
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
            int amountOfAccounts = accounts.Count;
            List<string> accountsDesc = DbHelper.GetAccountInformation(accounts);
           
            foreach (string acc in accountsDesc)
            {
                Console.WriteLine(acc);
            }
            Console.WriteLine(accountsDesc.Count);
            accountsDesc.Add("Transfer to another person");
            Console.WriteLine(accountsDesc.Count);


            foreach ( string acc  in accountsDesc) {
                Console.WriteLine(acc);
            }

            MenuFunctions.ShowMenu(accountsDesc.ToArray(),"Which account do you want to tranfer from?");

            // list of accounts to transfer TO + option to transfer to somebody else

        }

    }
}
