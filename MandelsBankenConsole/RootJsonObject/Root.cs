using System.Text.Json.Serialization;

namespace MandelsBankenConsole.RootJsonObject
{

    // structure for the json object that the api is returning

    public record Root(
    [property: JsonPropertyName("result")] string result,
    [property: JsonPropertyName("documentation")] string documentation,
    [property: JsonPropertyName("terms_of_use")] string terms_of_use,
    [property: JsonPropertyName("time_last_update_unix")] int time_last_update_unix,
    [property: JsonPropertyName("time_last_update_utc")] string time_last_update_utc,
    [property: JsonPropertyName("time_next_update_unix")] int time_next_update_unix,
    [property: JsonPropertyName("time_next_update_utc")] string time_next_update_utc,
    [property: JsonPropertyName("base_code")] string base_code,
    [property: JsonPropertyName("target_code")] string target_code,
    [property: JsonPropertyName("conversion_rate")] double conversion_rate,
    [property: JsonPropertyName("conversion_result")] double conversion_result);


}

