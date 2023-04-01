using System.Text.Json.Serialization;

namespace KookBot.Core;

public record WebsocketEvent(
        [property: JsonPropertyName("channel_type")]
        string ChannelType,
        [property: JsonPropertyName("type")]
        MessageType Type,
        [property: JsonPropertyName("target_id")]
        string TargetId,
        [property: JsonPropertyName("author_id")]
        string AuthorId,
        [property: JsonPropertyName("content")]
        string Content,
        [property: JsonPropertyName("extra")]
        MessageExtra Extra,
        [property: JsonPropertyName("msg_id")]
        string MessageId,
        [property: JsonPropertyName("msg_timestamp")]
        long MessageTimestamp,
        [property: JsonPropertyName("nonce")]
        string Nonce,
        [property: JsonPropertyName("from_type")]
        MessageType FromType
);
