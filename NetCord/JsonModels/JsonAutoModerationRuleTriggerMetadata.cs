﻿using System.Text.Json.Serialization;

using NetCord.Rest;

namespace NetCord.JsonModels;

public record JsonAutoModerationRuleTriggerMetadata
{
    [JsonPropertyName("keyword_filter")]
    public string[]? KeywordFilter { get; init; }

    [JsonPropertyName("presets")]
    public AutoModerationRuleKeywordPresetType[]? Presets { get; init; }
}