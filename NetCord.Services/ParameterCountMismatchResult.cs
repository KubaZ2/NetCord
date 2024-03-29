﻿using System.ComponentModel;

namespace NetCord.Services;

public class ParameterCountMismatchResult(ParameterCountMismatchType type) : IFailResult
{
    public ParameterCountMismatchType Type { get; } = type;

    public string Message => Type switch
    {
        ParameterCountMismatchType.TooFew => "Too few parameters.",
        ParameterCountMismatchType.TooMany => "Too many parameters.",
        _ => throw new InvalidEnumArgumentException(nameof(Type), (int)Type, typeof(ParameterCountMismatchType)),
    };
}
