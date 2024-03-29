﻿using System.Diagnostics.CodeAnalysis;

using NetCord.Rest;
using NetCord.Services.EnumTypeReaders;

namespace NetCord.Services.ApplicationCommands.TypeReaders;

public class EnumTypeReader<TContext> : SlashCommandTypeReader<TContext> where TContext : IApplicationCommandContext
{
    private readonly EnumTypeReaderManager<SlashCommandEnumTypeReader<TContext>, Type, SlashCommandParameter<TContext>, ApplicationCommandServiceConfiguration<TContext>?> _enumTypeReaderManager;

    public unsafe EnumTypeReader()
    {
        _enumTypeReaderManager = new(&GetKey, (type, parameter, configuration) => new SlashCommandEnumTypeReader<TContext>(type));

        static Type GetKey(SlashCommandParameter<TContext> parameter) => parameter.NonNullableType;
    }

    public override ApplicationCommandOptionType Type => ApplicationCommandOptionType.Integer;

    public override ValueTask<TypeReaderResult> ReadAsync(string value, TContext context, SlashCommandParameter<TContext> parameter, ApplicationCommandServiceConfiguration<TContext> configuration, IServiceProvider? serviceProvider)
    {
        if (_enumTypeReaderManager.GetTypeReader(parameter, null).TryRead(value.AsMemory(), out var result))
            return new(TypeReaderResult.Success(result));

        return new(TypeReaderResult.ParseFail(parameter.Name));
    }

    public override IChoicesProvider<TContext>? ChoicesProvider => new EnumChoicesProvider(_enumTypeReaderManager);

    private class EnumChoicesProvider(EnumTypeReaderManager<SlashCommandEnumTypeReader<TContext>, Type, SlashCommandParameter<TContext>, ApplicationCommandServiceConfiguration<TContext>?> enumInfoManager) : IChoicesProvider<TContext>
    {
        [UnconditionalSuppressMessage("Trimming", "IL2075:'this' argument does not satisfy 'DynamicallyAccessedMembersAttribute' in call to target method. The return value of the source method does not have matching annotations.", Justification = "Literal fields on enums can never be trimmed")]
        public ValueTask<IEnumerable<ApplicationCommandOptionChoiceProperties>?> GetChoicesAsync(SlashCommandParameter<TContext> parameter)
        {
            return enumInfoManager.GetTypeReader(parameter, null).GetChoicesAsync(parameter);
        }
    }
}
