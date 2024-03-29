﻿using System.Collections.Immutable;

using NetCord.Rest;

namespace NetCord.Gateway;

public class GuildEmojisUpdateEventArgs : IJsonModel<JsonModels.EventArgs.JsonGuildEmojisUpdateEventArgs>
{
    JsonModels.EventArgs.JsonGuildEmojisUpdateEventArgs IJsonModel<JsonModels.EventArgs.JsonGuildEmojisUpdateEventArgs>.JsonModel => _jsonModel;
    private readonly JsonModels.EventArgs.JsonGuildEmojisUpdateEventArgs _jsonModel;

    public GuildEmojisUpdateEventArgs(JsonModels.EventArgs.JsonGuildEmojisUpdateEventArgs jsonModel, RestClient client)
    {
        _jsonModel = jsonModel;
        Emojis = jsonModel.Emojis.ToImmutableDictionary(e => e.Id.GetValueOrDefault(), e => new GuildEmoji(e, GuildId, client));
    }

    public ulong GuildId => _jsonModel.GuildId;

    public ImmutableDictionary<ulong, GuildEmoji> Emojis { get; }
}
