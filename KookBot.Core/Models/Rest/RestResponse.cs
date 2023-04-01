using System.Text.Json.Serialization;

namespace KookBot.Core;

public record RestResponse<TData>(
        [property: JsonPropertyName("code")]
        int Code,
        [property: JsonPropertyName("message")]
        string Message,
        [property: JsonPropertyName("data")]
        TData Data
);
