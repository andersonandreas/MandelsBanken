using MandelsBankenConsole.AccountHandler;
using MandelsBankenConsole.Data;
using MandelsBankenConsole.InputValidator;
using MandelsBankenConsole.UserHandler;

namespace MandelsBankenConsole
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var context = new BankenContext();
            //IAPIDataReaderCurrency apiDataReader = new APIDataReaderCurrency();
            IValidateUserInput userInputValidator = new ValidateUserInput(
                new CharValidator(),
                new NumberValidator());



            // istallet for alla rader i era klasser 
            var exchange = CurrencyInitExchange.InitCurrencyHandler();

            // allt detta kommer flyttas till en klass dar vi har en mehtod som lagger allt detta i en method och so kor vi methoden i program main. 
            var accountManager = new AccountManager(userInputValidator, context, new Random());

            var adminFunctions = new AdminFunctions(accountManager, context);
            var depositMoneyFunctions = new DepositMoneyFunctions(context, exchange);
            var banking = new BankTransfer(context, exchange);
            var showAccount = new ShowAccount(context);
            var withdrawMoneyFunctions = new WithdrawMoneyFunctions(context, exchange);
            var menuFunctions = new MenuFunctions(context, accountManager, depositMoneyFunctions, adminFunctions, banking, withdrawMoneyFunctions, showAccount);



            menuFunctions.LogIn();





        }




    }
}












