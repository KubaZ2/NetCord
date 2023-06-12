﻿using System.Globalization;

using NetCord.Rest;

namespace NetCord;

public class EntityMenuInteractionData : ButtonInteractionData
{
    public EntityMenuInteractionData(JsonModels.JsonInteractionData jsonModel, ulong? guildId, RestClient client) : base(jsonModel)
    {
        int length = jsonModel.SelectedValues!.Length;
        var selectedValues = new ulong[length];
        for (var i = 0; i < length; i++)
            selectedValues[i] = ulong.Parse(jsonModel.SelectedValues[i], NumberStyles.None, CultureInfo.InvariantCulture);
        SelectedValues = selectedValues;
        if (jsonModel.ResolvedData != null)
            ResolvedData = new(jsonModel.ResolvedData, guildId, client);
    }

    public IReadOnlyList<ulong> SelectedValues { get; }

    public InteractionResolvedData? ResolvedData { get; }
}