using System.Text.Json.Serialization;

// ReSharper disable NotAccessedPositionalProperty.Global

namespace KookBot.Core;

public record CreateMessageOptions(
        [property: JsonPropertyName("type")]
        MessageType Type,
        [property: JsonPropertyName("target_id")]
        string TargetId,
        [property: JsonPropertyName("content")]
        string Content,
        [property: JsonPropertyName("quote")]
        string Quote,
        [property: JsonPropertyName("nonce")]
        string Nonce,
        [property: JsonPropertyName("temp_target_id")]
        string TempTargetId
);
