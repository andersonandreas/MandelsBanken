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
        public static void BankTransfer(BankenContext context, User user)
        {
            // user has chosen "Transfer between accounts"
            // we give choice to either transfer between user's own accounts or to another person

            // Magda. idea
            // first: menu/list of accounts to transfer FROM (all of the user's accounts if more than one)
            // title "transfer from account NUMBER + NAME.." (either the chosen one or user's only one)
            // choose account to transfer to (list of the other accounts + "transfer to another person"
            // questions: validation for another person: do we need personnummer or do we need to know account number?


            List<Account> accounts = DbHelper.GetAllAccounts(context, user);
            int amountOfAccounts = accounts.Count;
            string[] accountsDesc = new string[amountOfAccounts]; // placeholder for transfer to another user

            accountsDesc=accounts
                .Select(a=>new {xx=a.AccountNumber + ":" +  a.AccountName }.ToString())
                .ToArray();

            accountsDesc[amountOfAccounts] = "Transfer to another person";

            MenuFunctions.ShowMenu(accountsDesc);

            // list of accounts to transfer TO + option to transfer to somebody else

        }

    }
}
