using System.Text.Json.Serialization;

namespace KookBot.Core;

public record SystemMessageExtra<T>(
        [property: JsonPropertyName("type")]
        string Type,
        [property: JsonPropertyName("body")]
        T Body
);
