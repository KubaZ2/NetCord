﻿using System.Text.Json.Serialization;

using NetCord.JsonModels;

namespace NetCord.Gateway.JsonModels.EventArgs;

public class JsonGuildUserChunkEventArgs
{
    [JsonPropertyName("guild_id")]
    public ulong GuildId { get; set; }

    [JsonPropertyName("members")]
    public JsonGuildUser[] Users { get; set; }

    [JsonPropertyName("chunk_index")]
    public int ChunkIndex { get; set; }

    [JsonPropertyName("chunk_count")]
    public int ChunkCount { get; set; }

    [JsonPropertyName("not_found")]
    public ulong[]? NotFound { get; set; }

    [JsonPropertyName("presences")]
    public JsonPresence[]? Presences { get; set; }

    [JsonPropertyName("nonce")]
    public string? Nonce { get; set; }
}
