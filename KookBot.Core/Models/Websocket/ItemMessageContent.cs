using System.Text.Json.Serialization;

namespace KookBot.Core;

public record ItemMessageContent(
        [property: JsonPropertyName("type")]
        string Type,
        [property: JsonPropertyName("data")]
        ItemMessageContentData Data
);
