using System.Text.Json.Serialization;

namespace KookBot.Core;

public record WebsocketMessage<TData>(
        [property: JsonPropertyName("s")]
        WebsocketSignal Signal,
        [property: JsonPropertyName("d")]
        TData Data,
        [property: JsonPropertyName("sn")]
        int Sn
);
