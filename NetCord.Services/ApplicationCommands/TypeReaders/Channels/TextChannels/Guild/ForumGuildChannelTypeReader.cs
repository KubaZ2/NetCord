﻿namespace NetCord.Services.ApplicationCommands.TypeReaders;

public class ForumGuildChannelTypeReader<TContext> : ChannelTypeReader<TContext> where TContext : IApplicationCommandContext
{
    public override IEnumerable<ChannelType>? AllowedChannelTypes
    {
        get
        {
            yield return ChannelType.ForumGuildChannel;
            yield return ChannelType.MediaForumGuildChannel;
        }
    }
}
