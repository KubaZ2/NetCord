﻿using System.Text.Json.Serialization;

namespace NetCord.Rest;

public partial class VoiceStateOptions
{
    internal VoiceStateOptions(ulong channelId)
    {
        ChannelId = channelId;
    }

    [JsonPropertyName("channel_id")]
    public ulong ChannelId { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("suppress")]
    public bool? Suppress { get; set; }
}
