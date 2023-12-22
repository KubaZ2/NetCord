﻿using System.Text.Json.Serialization;

namespace NetCord.Gateway.JsonModels.EventArgs;

public class JsonMessageDeleteEventArgs
{
    [JsonPropertyName("id")]
    public ulong MessageId { get; set; }

    [JsonPropertyName("channel_id")]
    public ulong ChannelId { get; set; }

    [JsonPropertyName("guild_id")]
    public ulong? GuildId { get; set; }
}
