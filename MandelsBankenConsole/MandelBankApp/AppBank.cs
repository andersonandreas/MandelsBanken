using MandelsBankenConsole.AccountHandler;
using MandelsBankenConsole.Data;
using MandelsBankenConsole.InputValidator;
using MandelsBankenConsole.UserHandler;

namespace MandelsBankenConsole.MandelBankApp
{
    public class AppBank
    {
        public void Run()
        {

            // Our dependies for the banking app 
            // better to use interface for the type but using class name insteeasd

            var context = new BankenContext();
            IValidateUserInput userInputValidator = new ValidateUserInput(new CharValidator(),
                new NumberValidator(), new SocialNumberValidor(), new ValidateLoginSocial(), context);

            var exchange = CurrencyInitExchange.InitCurrencyHandler();

            var accountManager = new AccountManager(userInputValidator, context, new Random());

            var adminFunctions = new AdminFunctions(accountManager, context, userInputValidator);

            var depositMoneyFunctions = new DepositMoneyFunctions(context, exchange, userInputValidator);

            var banking = new BankTransfer(context, exchange, userInputValidator);

            var showAccount = new ShowAccount(context);

            var withdrawMoneyFunctions = new WithdrawMoneyFunctions(context, exchange, userInputValidator);
            var menuFunctions = new MenuFunctions(
                context, accountManager, depositMoneyFunctions, adminFunctions,
                banking, withdrawMoneyFunctions, showAccount, userInputValidator);

            menuFunctions.LogIn();


        }



    }



}
