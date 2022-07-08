﻿namespace NetCord.Rest;

public class WebhookMessage : ClientEntity, IJsonModel<JsonModels.JsonMessage>
{
    JsonModels.JsonMessage IJsonModel<JsonModels.JsonMessage>.JsonModel => _jsonModel;
    private protected readonly JsonModels.JsonMessage _jsonModel;

    public WebhookMessage(JsonModels.JsonMessage jsonModel, RestClient client) : base(client)
    {
        _jsonModel = jsonModel;
        Components = jsonModel.Components.SelectOrEmpty(IComponent.CreateFromJson);
        GuildId = jsonModel.GuildId ?? jsonModel.MessageReference?.GuildId;
    }

    public override Snowflake Id => _jsonModel.Id;
    public Snowflake ChannelId => _jsonModel.ChannelId;
    public Snowflake? GuildId { get; }
    public Snowflake? ApplicationId => _jsonModel.ApplicationId;
    public IEnumerable<IComponent> Components { get; }

    public string GetJumpUrl() => $"https://discord.com/channels/{(GuildId != null ? GuildId : "@me")}/{ChannelId}/{Id}";
}