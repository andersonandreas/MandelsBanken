using MandelsBankenConsole.API;
using MandelsBankenConsole.CurrencyConverter;
using MandelsBankenConsole.InputValidator;

namespace MandelsBankenConsole
{
    public class CurrencyInitExchange
    {

        public static ExchangeCurrency InitCurrencyHandler()
        {
            IAPIDataReaderCurrency aPIDataReader = new APIDataReaderCurrency();
            IValidateUserInput validateUserInput = new ValidateUserInput(
                new CharValidator(),
                new NumberValidator());


            return new ExchangeCurrency(new CurrencyHandler(validateUserInput, aPIDataReader));

        }
    }
}



