﻿namespace NetCord;

public interface IEntity
{
    /// <summary>
    /// The unique identifier for this object.
    /// </summary>
    public ulong Id { get; }

    /// <summary>
    /// The time this object was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; }
}
