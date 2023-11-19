namespace MandelsBankenConsole.InputValidator
{
    public interface IValidateUserInput
    {
        string BaseCurrency();
        string TargetCurrency();
        decimal Amount();
        string AccountName();
        string CodeCurrency();

    }
}
