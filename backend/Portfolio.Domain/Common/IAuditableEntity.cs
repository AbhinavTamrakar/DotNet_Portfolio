namespace Portfolio.Domain.Common;

/// <summary>Entity that records when it was created (UTC).</summary>
public interface ICreatedAtEntity
{
    DateTime CreatedAt { get; set; }
}

/// <summary>Entity that records when it was last updated (UTC).</summary>
public interface IUpdatedAtEntity
{
    DateTime UpdatedAt { get; set; }
}

/// <summary>Entity that records both creation and update timestamps (UTC).</summary>
public interface IAuditableEntity : ICreatedAtEntity, IUpdatedAtEntity;
