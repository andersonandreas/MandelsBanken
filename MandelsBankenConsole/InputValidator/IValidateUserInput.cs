namespace MandelsBankenConsole.InputValidator
{
    public interface IValidateUserInput
    {
        string BaseCurrency();
        string TargetCurrency();
        decimal Amount();
        decimal Amount(string message);
        string AccountName();
        string CodeCurrency();
        string FullName();
        string SocialNumber();
        string Pin();
        string CurrencyCodeUserInput();
    }
}
