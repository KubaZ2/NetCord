﻿using System.Text.Json.Serialization;

namespace NetCord.Rest;

public partial class GuildWidgetSettingsOptions
{
    internal GuildWidgetSettingsOptions()
    {
    }

    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }

    [JsonPropertyName("channel_id")]
    public ulong? ChannelId { get; set; }
}
