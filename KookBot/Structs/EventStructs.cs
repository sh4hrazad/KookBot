using System.Text.Json.Serialization;
using KookBot.Enums;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable NotAccessedPositionalProperty.Global

namespace KookBot.Structs;

public record WebSocketResult<TData>(
        [property: JsonPropertyName("s")]
        WebsocketSignal Signal,
        [property: JsonPropertyName("d")]
        TData Data,
        [property: JsonPropertyName("sn")]
        int Sn
);

public record WebsocketEventData(
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

public record MessageExtra(
        [property: JsonPropertyName("type")]
        MessageType Type,
        [property: JsonPropertyName("guild_id")]
        string GuildId,
        [property: JsonPropertyName("channel_name")]
        string ChannelName,
        [property: JsonPropertyName("mention")]
        string[] Mention,
        [property: JsonPropertyName("mention_all")]
        bool MentionAll,
        [property: JsonPropertyName("mention_roles")]
        int[] MentionRoles,
        [property: JsonPropertyName("mention_here")]
        bool MentionHere,
        [property: JsonPropertyName("author")]
        User Author,
        [property: JsonPropertyName("nav_channels")]
        object[] NavChannels,
        [property: JsonPropertyName("code")]
        string Code,
        [property: JsonPropertyName("kmarkdown")]
        object KMarkdown,
        [property: JsonPropertyName("attachments")]
        object Attachments
);

public record ItemMessageContent(
        [property: JsonPropertyName("type")]
        string Type,
        [property: JsonPropertyName("data")]
        ItemMessageContentData Data
);

public record ItemMessageContentData(
        [property: JsonPropertyName("user_id")]
        string UserId,
        [property: JsonPropertyName("target_id")]
        string TargetId,
        [property: JsonPropertyName("item_id")]
        int ItemId
);

public record SystemMessageExtra<T>(
        [property: JsonPropertyName("type")]
        string Type,
        [property: JsonPropertyName("body")]
        T Body
);
