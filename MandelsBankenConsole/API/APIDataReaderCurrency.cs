namespace MandelsBankenConsole.API
{

    public class APIDataReaderCurrency : IAPIDataReaderCurrency
    {

        const string BaseUrl = "https://v6.exchangerate-api.com/v6/";
        const string APIKey = "0fda422e2537ee416d3bc40e";   // anvand era nycklar fron erat konto istllet do man enbart har 1500 calls per manad.

        public async Task<string> Read(string baseCurrency, string targetCurrency, decimal amount)
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
            catch (Exception)
            {
                throw;
            }
        }

    }





}
