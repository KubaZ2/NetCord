﻿using System.Text.Json.Serialization;

using NetCord.JsonModels;

namespace NetCord.Gateway.JsonModels;

public class JsonPresence
{
    [JsonPropertyName("user")]
    public JsonUser User { get; set; }

    [JsonPropertyName("guild_id")]
    public ulong? GuildId { get; set; }

    [JsonPropertyName("status")]
    public UserStatusType Status { get; set; }

    [JsonPropertyName("activities")]
    public JsonUserActivity[]? Activities { get; set; }

    [JsonPropertyName("client_status")]
    public IReadOnlyDictionary<Platform, UserStatusType> Platform { get; set; }
}
