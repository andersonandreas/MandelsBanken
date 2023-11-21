using MandelsBankenConsole.API;
using MandelsBankenConsole.CurrencyConverter;
using MandelsBankenConsole.InputValidator;

namespace MandelsBankenConsole.AccountHandler
{
    public class CurrencyInitExchange
    {

        public static ExchangeCurrency InitCurrencyHandler()
        {
            IAPIDataReaderCurrency aPIDataReader = new APIDataReaderCurrency();
            IValidateUserInput validateUserInput = new ValidateUserInput(
                new CharValidator(),
                new NumberValidator(),
                 new SocialNumberValidor(),
                 new ValidateLoginSocial());


            return new ExchangeCurrency(new CurrencyHandler(validateUserInput, aPIDataReader));

        }
    }
}



