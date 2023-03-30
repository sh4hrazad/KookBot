using System.Text.Json.Serialization;
using KookBot.Enums;

// ReSharper disable NotAccessedPositionalProperty.Global

namespace KookBot.Structs;

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
