﻿using System.Text.Json.Serialization;

namespace NetCord;

public class AutoModerationActionMetadataProperties
{
    [JsonPropertyName("channel_id")]
    public Snowflake? ChannelId { get; set; }

    [JsonPropertyName("duration_seconds")]
    public int? DurationSeconds { get; set; }
}
