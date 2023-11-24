namespace MandelsBankenConsole.API
{

    public class APIDataReaderCurrency : IAPIDataReaderCurrency
    {

        const string BaseUrl = "https://v6.exchangerate-api.com/v6/";
        const string APIKey = "0fda422e2537ee416d3bc40e";

        public async Task<string> Read(string baseCurrency,
            string targetCurrency, decimal amount)
        {

            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                HttpResponseMessage response = await client.GetAsync(
                    $"{APIKey}/pair/{baseCurrency}/{targetCurrency}/{amount}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                await Console.Out.WriteLineAsync($"HTTP  Error: {e.Message}");
                throw;
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync($"Error: {e.Message}");
                throw;
            }
        }

    }





}
