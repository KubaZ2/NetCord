﻿using System.Text.Json.Serialization;

namespace NetCord.Gateway;

public partial class UserActivitySecretsProperties
{
    [JsonPropertyName("join")]
    public string? Join { get; set; }

    [JsonPropertyName("spectate")]
    public string? Spectate { get; set; }

    [JsonPropertyName("match")]
    public string? Match { get; set; }
}
