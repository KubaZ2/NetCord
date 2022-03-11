﻿namespace NetCord;

public class ApplicationCommandInteractionResolvedData
{
    public IReadOnlyDictionary<DiscordId, User>? Users { get; }

    public IReadOnlyDictionary<DiscordId, GuildRole>? Roles { get; }

    public IReadOnlyDictionary<DiscordId, Channel>? Channels { get; }

    public IReadOnlyDictionary<DiscordId, RestMessage>? Messages { get; }

    public IReadOnlyDictionary<DiscordId, Attachment>? Attachments { get; }

    internal ApplicationCommandInteractionResolvedData(JsonModels.JsonApplicationCommandResolvedData jsonEntity, DiscordId? guildId, RestClient client)
    {
        if (jsonEntity.Users != null)
        {
            if (jsonEntity.GuildUsers != null)
            {
                var enumerator = jsonEntity.Users.GetEnumerator();
                int max = jsonEntity.Users.Count - jsonEntity.GuildUsers.Count;
                Dictionary<DiscordId, User> users = new();
                for (int i = 0; i < max; i++)
                {
                    enumerator.MoveNext();
                    var current = enumerator.Current;
                    users.Add(current.Key, new(current.Value, client));
                }
                foreach (var guildUser in jsonEntity.GuildUsers)
                {
                    enumerator.MoveNext();
                    var current = enumerator.Current;
                    users.Add(current.Key, new GuildInteractionUser(guildUser.Value with { User = current.Value }, guildId.GetValueOrDefault(), client));
                }
                enumerator.Dispose();
                Users = users;
            }
            else
                Users = jsonEntity.Users.ToDictionary(u => u.Key, u => new User(u.Value, client));
        }

        if (jsonEntity.Roles != null)
            Roles = jsonEntity.Roles.ToDictionary(r => r.Key, r => new GuildRole(r.Value, client));
        if (jsonEntity.Channels != null)
            Channels = jsonEntity.Channels.ToDictionary(c => c.Key, c => Channel.CreateFromJson(c.Value, client));
        if (jsonEntity.Messages != null)
            Messages = jsonEntity.Messages.ToDictionary(c => c.Key, c => new RestMessage(c.Value, client));
        if (jsonEntity.Attachments != null)
            Attachments = jsonEntity.Attachments.ToDictionary(c => c.Key, c => Attachment.CreateFromJson(c.Value));
    }
}