using MandelsBankenConsole.API;
using MandelsBankenConsole.InputValidator;
using MandelsBankenConsole.RootJsonObject;
using System.Text.Json;

namespace MandelsBankenConsole.CurrencyConverter
{
    public class CurrencyHandler : ICurrencyHandler
    {

        private readonly IValidateUserInput _userInputValidator;
        private readonly IAPIDataReaderCurrency _apiDataReader;

        public CurrencyHandler(IValidateUserInput userInputValidator,
            IAPIDataReaderCurrency apiDataReader)
        {
            _userInputValidator = userInputValidator;
            _apiDataReader = apiDataReader;
        }

        public async Task<(decimal ConvertResult, string Information)> ConvertBetweenCurrencies(string baseCurrency, string targetCurrency, decimal amount)
        {

            try
            {

                var json = await _apiDataReader.Read(baseCurrency, targetCurrency, amount);
                var ConvertResult = Result(json);
                var information = ConvertInfo(baseCurrency, targetCurrency, amount, ConvertResult);

                return (ConvertResult, information);
            }
            catch (Exception ex)
            {
                // add more spec error message to console
                Console.WriteLine($"Error: {ex.Message}");
                throw;

            }


        }

        public async Task<(decimal ConvertResult, string Information)> RunExchange()
        {
            try
            {
                var (baseCurrency, targetCurrency, amount) = GetUserInput();

                var json = await _apiDataReader.Read(baseCurrency, targetCurrency, amount);
                var ConvertResult = Result(json);
                var information = ConvertInfo(baseCurrency, targetCurrency, amount, ConvertResult);



                return (ConvertResult, information);
            }
            catch (Exception ex)
            {
                // add more spec error message to console
                Console.WriteLine($"Error: {ex.Message}");
                throw;

            }

        }

        private string ConvertInfo(string baseCurrency, string targetCurrency, decimal amount, decimal ConvertResult)
        {

            return $"Convertering from {baseCurrency} to {targetCurrency}: {amount} {baseCurrency} = {ConvertResult} {targetCurrency}";
        }


        private (string baseCurrency, string targetCurrency, decimal amount) GetUserInput()
        {
            var baseCurrency = _userInputValidator.BaseCurrency();
            var targetCurrency = _userInputValidator.TargetCurrency();
            var amount = _userInputValidator.Amount();

            return (baseCurrency, targetCurrency, amount);
        }

        private decimal Result(string json)
        {
            var convertResult = JsonSerializer.Deserialize<Root>(json);
            return (decimal)convertResult.conversion_result;
        }

    }
}
