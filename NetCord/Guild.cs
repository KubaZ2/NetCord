﻿using System.Diagnostics.CodeAnalysis;

using NetCord.JsonModels;

namespace NetCord;

public class Guild : ClientEntity
{
    internal readonly JsonGuild _jsonEntity;

    internal Dictionary<DiscordId, VoiceState> _voiceStates;
    internal Dictionary<DiscordId, GuildUser> _users;
    internal Dictionary<DiscordId, IGuildChannel> _channels;
    internal Dictionary<DiscordId, Thread> _activeThreads;
    internal Dictionary<DiscordId, Role> _roles;
    internal Dictionary<DiscordId, Emoji> _emojis;
    internal Dictionary<DiscordId, StageInstance> _stageInstances;

    public override DiscordId Id => _jsonEntity.Id;
    public string Name => _jsonEntity.Name;
    public string? Icon => _jsonEntity.Icon;
    public string? IconHash => _jsonEntity.IconHash;
    public string? Splash => _jsonEntity.Splash;
    public string? DiscoverySplash => _jsonEntity.DiscoverySplash;
    public bool? IsOwner => _jsonEntity.IsOwner;
    public DiscordId OwnerId => _jsonEntity.OwnerId;
    public string? Permissions => _jsonEntity.Permissions;
    public string? Region => _jsonEntity.Region;
    public DiscordId? AfkChannelId => _jsonEntity.AfkChannelId;
    public int AfkTimeout => _jsonEntity.AfkTimeout;
    public bool? WidgetEnabled => _jsonEntity.WidgetEnabled;
    public DiscordId? WidgetChannelId => _jsonEntity.WidgetChannelId;
    public VerificationLevel VerificationLevel => _jsonEntity.VerificationLevel;
    public DefaultMessageNotificationLevel DefaultMessageNotificationLevel => _jsonEntity.DefaultMessageNotificationLevel;
    public ContentFilter ContentFilter => _jsonEntity.ContentFilter;
    public IEnumerable<Role> Roles
    {
        get
        {
            lock (_roles)
                return _roles.Values.AsEnumerable();
        }
    }
    public IEnumerable<Emoji> Emojis
    {
        get
        {
            lock (_emojis)
                return _emojis.Values.AsEnumerable();
        }
    }
    public SocketGuildFeatures Features { get; }
    public MFALevel MFALevel => _jsonEntity.MFALevel;
    public DiscordId? ApplicationId => _jsonEntity.ApplicationId;
    public DiscordId? SystemChannelId => _jsonEntity.SystemChannelId;
    public int SystemChannelFlags => _jsonEntity.SystemChannelFlags;
    public DiscordId? RulesChannelId => _jsonEntity.RulesChannelId;
    public DateTimeOffset? CreatedAt => _jsonEntity.CreatedAt;
    public bool? IsLarge => _jsonEntity.IsLarge;
    public bool? IsUnavaible => _jsonEntity.IsUnavaible;
    public int? MemberCount { get; internal set; }
    public IEnumerable<GuildUser> Users
    {
        get
        {
            lock (_users)
                return _users.Values.AsEnumerable();
        }
    }
    public IEnumerable<IGuildChannel> Channels
    {
        get
        {
            lock (_channels)
                return _channels.Values.AsEnumerable();
        }
    }
    public IEnumerable<Thread> ActiveThreads
    {
        get
        {
            lock (_activeThreads)
                return _activeThreads.Values.AsEnumerable();
        }
    }
    public IEnumerable<Presence> Presences { get; }
    public int? MaxPresences => _jsonEntity.MaxPresences;
    public int? MaxMembers => _jsonEntity.MaxMembers;
    public string? VanityUrlCode => _jsonEntity.VanityUrlCode;
    public string? Description => _jsonEntity.Description;
    public string? BannerHash => _jsonEntity.BannerHash;
    public int PremiumTier => _jsonEntity.PremiumTier;
    public int? PremiumSubscriptionCount => _jsonEntity.PremiumSubscriptionCount;
    public System.Globalization.CultureInfo PreferredLocale => _jsonEntity.PreferredLocale;
    public DiscordId? PublicUpdatesChannelId => _jsonEntity.PublicUpdatesChannelId;
    public int? MaxVideoChannelUsers => _jsonEntity.MaxVideoChannelUsers;
    public int? ApproximateMemberCount { get; internal set; }
    public int? ApproximatePresenceCount => _jsonEntity.ApproximatePresenceCount;
    public WelcomeScreen? WelcomeScreen { get; }
    public NSFWLevel NSFWLevel => _jsonEntity.NSFWLevel;
    public IEnumerable<StageInstance> StageInstances
    {
        get
        {
            lock (_stageInstances)
                return _stageInstances.Values.AsEnumerable();
        }
    }
    public IEnumerable<GuildSticker> Stickers { get; }

    public IEnumerable<VoiceState> VoiceStates
    {
        get
        {
            lock (_voiceStates)
                return _voiceStates.Values.AsEnumerable();
        }
    }

    internal Guild(JsonGuild jsonEntity, BotClient client) : base(client)
    {
        _jsonEntity = jsonEntity;

        _voiceStates = _jsonEntity.VoiceStates.ToDictionaryOrEmpty(s => s.UserId, s => new VoiceState(s));
        _users = _jsonEntity.Users.ToDictionaryOrEmpty(u => u.User.Id,
            u => new GuildUser(u, this, client));

        _channels = _jsonEntity.Channels.ToDictionaryOrEmpty(c => c.Id, c => (IGuildChannel)Channel.CreateFromJson(c, client));
        _activeThreads = _jsonEntity.ActiveThreads.ToDictionaryOrEmpty(t => t.Id, t => (Thread)Channel.CreateFromJson(t, client));
        _roles = _jsonEntity.Roles.ToDictionaryOrEmpty(r => r.Id, r => new Role(r, client));
        _emojis = _jsonEntity.Emojis.ToDictionaryOrEmpty(e => e.Id, e => new Emoji(e, client));
        _stageInstances = _jsonEntity.StageInstances.ToDictionaryOrEmpty(i => i.Id, i => new StageInstance(i, client));
        Stickers = _jsonEntity.Stickers.SelectOrEmpty(s => new GuildSticker(s, client));
        MemberCount = _jsonEntity.MemberCount;
        ApproximateMemberCount = _jsonEntity.ApproximateMemberCount;
        Presences = _jsonEntity.Presences.SelectOrEmpty(p => new Presence(p, client));
    }

    public bool TryGetUser(DiscordId id, [NotNullWhen(true)] out GuildUser? user)
    {
        lock (_users)
        {
            return _users.TryGetValue(id, out user);
        }
    }

    public GuildUser GetUser(DiscordId id)
    {
        if (TryGetUser(id, out var user))
            return user;
        else
            throw new EntityNotFoundException("The user was not found");
    }

    public bool TryGetChannel(DiscordId id, out IGuildChannel channel)
    {
        lock (_channels)
            return _channels.TryGetValue(id, out channel);
    }

    public IGuildChannel GetChannel(DiscordId id)
    {
        if (TryGetChannel(id, out var channel))
            return channel;
        else
            throw new EntityNotFoundException("The channel was not found");
    }

    public Task KickUserAsync(DiscordId userId) => GuildHelper.KickUserAsync(_client, userId, Id);
    public Task KickUserAsync(DiscordId userId, string reason) => GuildHelper.KickUserAsync(_client, userId, Id, reason);

    public Task AddBanAsync(DiscordId userId) => GuildHelper.AddBanAsync(_client, userId, Id);
    public Task AddBanAsync(DiscordId userId, string reason) => GuildHelper.AddBanAsync(_client, userId, Id, reason);
    public Task AddBanAsync(DiscordId userId, int deleteMessageDays, string reason) => GuildHelper.AddBanAsync(_client, userId, Id, deleteMessageDays, reason);

    public Task RemoveBanAsync(DiscordId userId) => GuildHelper.RemoveBanAsync(_client, userId, Id);
    public Task RemoveBanAsync(DiscordId userId, string reason) => GuildHelper.RemoveBanAsync(_client, userId, Id, reason);
}
