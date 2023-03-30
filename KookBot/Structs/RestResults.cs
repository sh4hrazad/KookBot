using System.Text.Json.Serialization;

// ReSharper disable NotAccessedPositionalProperty.Global

namespace KookBot.Structs;

public record RestResult<TData>(
        [property: JsonPropertyName("code")]
        int Code,
        [property: JsonPropertyName("message")]
        string Message,
        [property: JsonPropertyName("data")]
        TData Data
);

public record GatewayData(
        [property: JsonPropertyName("url")]
        string Url
);

public record CreateMessageData(
        [property: JsonPropertyName("msg_id")]
        string MessageId,
        [property: JsonPropertyName("msg_timestamp")]
        long MessageTimestamp,
        [property: JsonPropertyName("nonce")]
        string Nonce
);
