using System.Text.Json.Serialization;

namespace KookBot.Core;

public record User(
        [property: JsonPropertyName("id")]
        string Id,
        [property: JsonPropertyName("username")]
        string Username,
        [property: JsonPropertyName("identify_num")]
        string IdentifyNum,
        [property: JsonPropertyName("online")]
        bool Online,
        [property: JsonPropertyName("status")]
        int Status,
        [property: JsonPropertyName("avatar")]
        string Avatar,
        [property: JsonPropertyName("vip_avatar")]
        string VipAvatar,
        [property: JsonPropertyName("banner")]
        string Banner,
        [property: JsonPropertyName("nickname")]
        string Nickname,
        [property: JsonPropertyName("roles")]
        int[] Roles,
        [property: JsonPropertyName("is_vip")]
        bool IsVip,
        [property: JsonPropertyName("is_ai_reduce_noise")]
        bool IsAiReduceNoise,
        [property: JsonPropertyName("is_personal_card_bg")]
        bool IsPersonalCardBg,
        [property: JsonPropertyName("bot")]
        bool Bot,
        [property: JsonPropertyName("decorations_id_map")]
        object DecorationsIdMap
);
