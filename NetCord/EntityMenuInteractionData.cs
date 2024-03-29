﻿using NetCord.Rest;

namespace NetCord;

public class EntityMenuInteractionData : MessageComponentInteractionData
{
    public EntityMenuInteractionData(JsonModels.JsonInteractionData jsonModel, ulong? guildId, RestClient client) : base(jsonModel)
    {
        var selectedValues = jsonModel.SelectedValues!;
        int length = selectedValues.Length;
        var result = new ulong[length];
        for (var i = 0; i < length; i++)
            result[i] = Snowflake.Parse(selectedValues[i]);
        SelectedValues = result;

        var resolvedData = jsonModel.ResolvedData;
        if (resolvedData is not null)
            ResolvedData = new(resolvedData, guildId, client);
    }

    public IReadOnlyList<ulong> SelectedValues { get; }

    public InteractionResolvedData? ResolvedData { get; }
}
