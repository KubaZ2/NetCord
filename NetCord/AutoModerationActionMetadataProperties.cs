﻿using System.Text.Json.Serialization;

namespace NetCord;

public partial class AutoModerationActionMetadataProperties
{
    [JsonPropertyName("channel_id")]
    public ulong? ChannelId { get; set; }

    [JsonPropertyName("duration_seconds")]
    public int? DurationSeconds { get; set; }

    [JsonPropertyName("custom_message")]
    public string? CustomMessage { get; set; }
}
