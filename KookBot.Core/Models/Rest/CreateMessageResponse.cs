using System.Text.Json.Serialization;

namespace KookBot.Core;

public record CreateMessageResponse(
        [property: JsonPropertyName("msg_id")]
        string MessageId,
        [property: JsonPropertyName("msg_timestamp")]
        long MessageTimestamp,
        [property: JsonPropertyName("nonce")]
        string Nonce
);
