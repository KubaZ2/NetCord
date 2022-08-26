﻿using NetCord.JsonModels.EventArgs;

namespace NetCord.Gateway;

public class GuildInviteDeleteEventArgs : IJsonModel<JsonGuildInviteDeleteEventArgs>
{
    JsonGuildInviteDeleteEventArgs IJsonModel<JsonGuildInviteDeleteEventArgs>.JsonModel => _jsonModel;
    private readonly JsonGuildInviteDeleteEventArgs _jsonModel;

    public GuildInviteDeleteEventArgs(JsonGuildInviteDeleteEventArgs jsonModel)
    {
        _jsonModel = jsonModel;
    }

    public Snowflake InviteChannelId => _jsonModel.InviteChannelId;

    public Snowflake? GuildId => _jsonModel.GuildId;

    public string InviteCode => _jsonModel.InviteCode;
}
