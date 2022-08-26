﻿namespace NetCord.Services.Interactions.TypeReaders;

public class IntPtrTypeReader<TContext> : InteractionTypeReader<TContext> where TContext : InteractionContext
{
    public override Task<object?> ReadAsync(ReadOnlyMemory<char> input, TContext context, InteractionParameter<TContext> parameter, InteractionServiceOptions<TContext> options) => Task.FromResult((object?)nint.Parse(input.Span, provider: options.CultureInfo));
}
