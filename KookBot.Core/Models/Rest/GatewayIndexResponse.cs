using System.Text.Json.Serialization;

namespace KookBot.Core;

public record GatewayIndexResponse(
        [property: JsonPropertyName("url")]
        string Url
);
