﻿using NetCord.Gateway;
using NetCord.Rest;

namespace NetCord;

public class ButtonInteraction(JsonModels.JsonInteraction jsonModel, Guild? guild, Func<IInteraction, InteractionCallback, RestRequestProperties?, Task> sendResponseAsync, RestClient client) : MessageComponentInteraction(jsonModel, guild, sendResponseAsync, client)
{
    public override ButtonInteractionData Data { get; } = new(jsonModel.Data!);
}
