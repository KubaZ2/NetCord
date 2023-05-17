﻿using System.Text.Json.Serialization;

namespace NetCord.Rest.JsonModels;

public partial class JsonRestGuildInviteMetadata
{
    [JsonPropertyName("uses")]
    public int Uses { get; set; }

    [JsonPropertyName("max_uses")]
    public int MaxUses { get; set; }

    [JsonPropertyName("max_age")]
    public int MaxAge { get; set; }

    [JsonPropertyName("temporary")]
    public bool Temporary { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonSerializable(typeof(JsonRestGuildInviteMetadata))]
    public partial class JsonRestGuildInviteMetadataSerializerContext : JsonSerializerContext
    {
        public static JsonRestGuildInviteMetadataSerializerContext WithOptions { get; } = new(Serialization.Options);
    }
}