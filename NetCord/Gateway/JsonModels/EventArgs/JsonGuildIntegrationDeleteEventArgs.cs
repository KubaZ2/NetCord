﻿using System.Text.Json.Serialization;

namespace NetCord.Gateway.JsonModels.EventArgs;

public class JsonGuildIntegrationDeleteEventArgs
{
    [JsonPropertyName("id")]
    public ulong IntegrationId { get; set; }

    [JsonPropertyName("guild_id")]
    public ulong GuildId { get; set; }

    [JsonPropertyName("application_id")]
    public ulong? ApplicationId { get; set; }
}
