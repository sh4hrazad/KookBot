using System.Text.Json.Serialization;

namespace KookBot.Core;

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
