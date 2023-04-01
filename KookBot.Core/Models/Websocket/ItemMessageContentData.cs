using System.Text.Json.Serialization;

namespace KookBot.Core;

public record ItemMessageContentData(
        [property: JsonPropertyName("user_id")]
        string UserId,
        [property: JsonPropertyName("target_id")]
        string TargetId,
        [property: JsonPropertyName("item_id")]
        int ItemId
);
